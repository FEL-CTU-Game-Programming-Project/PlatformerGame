using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Samurai : Entity {
	/*
	 * Simple boss made hard
	 * Almost completely ignores Entity
	 * Holds all states and data + utility functions for states
	 */

	[SerializeField]
	private Transform slashWarningPF, slashAttackPF;

	[SerializeField]
	public Transform player1;
	[SerializeField]
	public Transform player2;

	public SamuraiIdleState idleState { get; protected set; }
	public SamuraiShuffleState shuffleState { get; protected set; }
	public SamuraiA1State a1State { get; protected set; }
	public SamuraiA2State a2State { get; protected set; }
	public SamuraiSliceState sliceState { get; protected set; }
	public SamuraiHitState hitState{ get; protected set; }
	public SamuraiDeathState deathState { get; protected set; }
	public SamuraiDashState dashState { get; protected set; }

	[SerializeField]
	public D_BaseSamurai samuraiBaseData;

	[SerializeField]
	private D_SamuraiIdle idleStateData;
	[SerializeField]
	private D_SamuraiShuffle shuffleStateData;
	[SerializeField]
	private D_SamuraiA1 a1StateData;
	[SerializeField]
	private D_SamuraiA2 a2StateData;
	[SerializeField]
	private D_SamuraiSlice sliceStateData;
	[SerializeField]
	private D_SamuraiDeath deathStateData;
	[SerializeField]
	private D_SamuraiHit hitStateData;
	[SerializeField]
	private D_SamuraiDash dashStateData;

	public GhostController ghost { get; private set; }

	private AttackProperties swordDamage;
	private AttackProperties sliceDamage;

	public bool attackReady { get; private set; } = true;


	public override void FixedUpdate() {
		stateMachine.currentState.PhysicsUpdate();
	}

	public override void SetVelocity(Vector2 direction, float velocity) {
		base.SetVelocity(direction, velocity);
	}

	public void Move(float velocity) {
		rb.MovePosition( rb.position + Vector2.right * velocity * facing * Time.fixedDeltaTime );
	}

	public override void Start() {
		base.Start();
		if(player1 == null) {
			Debug.LogError("Player 1 missing. Commiting seppuku");
			Destroy(gameObject);
		}
		idleState = new SamuraiIdleState(stateMachine, this, "idle", idleStateData);
		shuffleState = new SamuraiShuffleState(stateMachine, this, "shuffle", shuffleStateData);
		dashState = new SamuraiDashState(stateMachine, this, "dash", dashStateData);
		a1State = new SamuraiA1State(stateMachine, this, "a1", a1StateData);
		a2State = new SamuraiA2State(stateMachine, this, "a2", a2StateData);
		sliceState = new SamuraiSliceState(stateMachine, this, "slice", sliceStateData);
		hitState = new SamuraiHitState(stateMachine, this, "hit", hitStateData);
		deathState = new SamuraiDeathState(stateMachine, this, "death", deathStateData);

		swordDamage = new AttackProperties(samuraiBaseData.swordDamage);
		sliceDamage = new AttackProperties(samuraiBaseData.sliceDamage);

		facing = 1;

		ghost = transform.GetComponent<GhostController>();
		stateMachine.Initialize(idleState);
		FacePlayer();
	}

	public override void Update() {
		stateMachine.currentState.LogicUpdate();
	}

	protected override void Damage(AttackProperties attack) {
		base.Damage(attack);
		if(currentHealth <= 0) {
			stateMachine.ChangeState(deathState);
		}
		if(startStagger) {
			hitState.previousState = idleState;
			hitState.direction = knockbackDirection;
			stateMachine.ChangeState(hitState);
			startStagger = false;
		}

	}

	public virtual void FacePlayer() {
		float minDist = NearestPlayerDist();
		if (minDist > 0 && facing < 0) {
			Flip();
		} else if (minDist < 0 && facing > 0) {
			Flip();
		}
	}

	public void AnimationEvent(string message) {
		State currentState = stateMachine.currentState;
		if (message == "A1End" && currentState.Equals(a1State)) {
			a1State.AnimEndEvent();
		} else if (message == "A2End" && currentState.Equals(a2State)) {
			a2State.AnimEndEvent();
		} else if (message == "HitEnd" && currentState.Equals(hitState)) {
			hitState.AnimEndEvent();
		} else if (message == "DeathEnd" && currentState.Equals(deathState)) {
			Destroy(gameObject, 1f);
			SceneManager.LoadScene("VictoryScreen");

		}
		// Other animations
	}

	/*
	 * Handles damage sent to player based on hitbox name
	 */
	public void HitBoxTrigger(string hitbox, Collider2D other) {
		string layerName = LayerMask.LayerToName(other.gameObject.layer);
		if ( LayerMask.GetMask(layerName) != baseData.whatIsPlayer.value) {
			return;
		}
		AttackProperties attack;
		if (hitbox.Equals("HitBox")) {
			attack = swordDamage;
		} else {
			attack = touchDamage;
		}
		attack.direction = aliveGO.transform.position.x;
		other.gameObject.SendMessage("Damage", attack);
	}

	/*
	 * Returns distance of nearest player
	 *	Negative if player is on left
	 *	Positive if player is on right
	 */
	public float NearestPlayerDist() {
		if(player1 == null) {
			Debug.LogError("Missing player1!");
			return float.MaxValue;
		}
		float minDist;
		float alivePos = aliveGO.transform.position.x;
		if (player2 == null) {
			minDist = player1.position.x - alivePos;
		}
		else {
			float p1Dist = player1.position.x - alivePos;
			float p2Dist = player2.position.x - alivePos;
			minDist = Mathf.Abs(p1Dist) < Mathf.Abs(p2Dist) ? p1Dist : p2Dist;
		}
		return minDist;
	}

	/*
	 * Coroutine that handles Instantiating anime slice prefabs
	 */
	IEnumerator SlashCycle() {
		yield return new WaitForSeconds(sliceStateData.warningDelay);
		SlashWarning();
		yield return new WaitForSeconds(sliceStateData.attackDelay);
		SlashAttack();
		yield return new WaitForSeconds(sliceStateData.exitDelay);
		stateMachine.ChangeState(idleState);
	}

	// Sends a warning beam, still looks threatening and absolutely hideous
	public void SlashWarning() {
		Instantiate(slashWarningPF, aliveGO.transform.position + samuraiBaseData.castOffset, aliveGO.transform.rotation);
	}

	// Sends the actual attack and checks if it landed
	public void SlashAttack() {
		Instantiate(slashAttackPF, aliveGO.transform.position + samuraiBaseData.castOffset, aliveGO.transform.rotation);
		CheckSliceHB();
	}

	/*
	 * Checks if a hit occured in a rectangular area (size is changable in samuraiBaseData)
	 */
	private void CheckSliceHB() {
		Vector2 position = new Vector2(aliveGO.transform.position.x, aliveGO.transform.position.y);
		Vector2 topRight = position + new Vector2(samuraiBaseData.castOffset.x, samuraiBaseData.castOffset.y + sliceStateData.diameter);
		Vector2 downLeft = position + new Vector2(samuraiBaseData.castOffset.x + facing * sliceStateData.distance, samuraiBaseData.castOffset.y - sliceStateData.diameter);

		Collider2D hit = Physics2D.OverlapArea(downLeft, topRight, baseData.whatIsPlayer);

		if (hit != null) {
			sliceDamage.direction = aliveGO.transform.position.x;
			hit.SendMessage("Damage", sliceDamage);
		}
	}

	/*
	 * Showcases anime slice hitbox and dash + attack ranges
	 */
	void OnDrawGizmos() {
		Vector3 position = transform.Find("Alive").gameObject.transform.position;
		Gizmos.color = Color.blue;
		Gizmos.DrawLine(position, position + new Vector3(samuraiBaseData.maxDashDist, 0, 0));

		Gizmos.color = Color.red;
		Gizmos.DrawLine(position, position + new Vector3(dashStateData.inRange, 0, 0));
		Vector2 topRight = (Vector2)position + new Vector2(samuraiBaseData.castOffset.x, samuraiBaseData.castOffset.y + sliceStateData.diameter);
		Vector2 downLeft = (Vector2)position + new Vector2(samuraiBaseData.castOffset.x + facing * sliceStateData.distance, samuraiBaseData.castOffset.y - sliceStateData.diameter);
		Vector2 downRight = (Vector2)position + new Vector2(samuraiBaseData.castOffset.x, samuraiBaseData.castOffset.y - sliceStateData.diameter);
		Vector2 topLeft = (Vector2)position + new Vector2(samuraiBaseData.castOffset.x + facing * sliceStateData.distance, samuraiBaseData.castOffset.y + sliceStateData.diameter);
		Gizmos.color = Color.red;
		Gizmos.DrawLine((Vector3)topRight, (Vector3)topLeft);
		Gizmos.DrawLine((Vector3)topRight, (Vector3)downRight);
		Gizmos.DrawLine((Vector3)downLeft, (Vector3)downRight);
		Gizmos.DrawLine((Vector3)downLeft, (Vector3)topLeft);
	}
}
