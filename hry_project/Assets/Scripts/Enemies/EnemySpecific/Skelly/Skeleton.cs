using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skeleton : Entity {
	/*
	 * Upon abandoning PatrolEntity, better AI can be created
	 * This class performs basic checks for player and has utility functions for states to use
	 * It can interrupt states in case of player detection
	 */

	[SerializeField]
	private Transform wallCheck, ledgeCheck, playerCheck;

	[SerializeField]
	public D_SkellyBase skellyData;

	[SerializeField]
	public Transform drop;

	[SerializeField]
	public D_SkellyA1 a1Data;
	[SerializeField]
	public D_SkellyIdle idleData;
	[SerializeField]
	public D_SkellyPD pdData;
	[SerializeField]
	public D_SkellyWalk walkData;
	[SerializeField]
	public D_SkellyDeath deathData;
	[SerializeField]
	public D_SkellyHit hitData;

	public SkellyA1 a1State { get; private set; }
	public SkellyIdle idleState { get; private set; }
	public SkellyPlayerDetected pdState { get; private set; }
	public SkellyWalk walkState { get; private set; }
	public SkellyDeath deathState { get; private set; }
	public SkellyHit hitState { get; private set; }


	public bool isAttackReady { get; private set; }

	public bool dropped = false;

	public Transform target { get; private set; }

	public override void AnimEvent(string name) {
		base.AnimEvent(name);
		if(name.Equals("a1 end")) {
			a1State.AnimEnd();
		}
	}

	public override void FixedUpdate() {
		base.FixedUpdate();
	}

	public override void Flip() {
		base.Flip();
	}

	public override void HitBoxTrigger(Collider2D other) {
		base.HitBoxTrigger(other);
	}

	public override void SetVelocity(Vector2 direction, float velocity) {
		base.SetVelocity(direction, velocity);
	}

	public override void Start() {
		base.Start();

		idleState = new SkellyIdle(stateMachine, this, "idle", idleData);
		walkState = new SkellyWalk(stateMachine, this, "move", walkData);
		deathState = new SkellyDeath(stateMachine, this, "death", deathData);
		a1State = new SkellyA1(stateMachine, this, "a1", a1Data);
		hitState = new SkellyHit(stateMachine, this, "hit", hitData);
		pdState = new SkellyPlayerDetected(stateMachine, this, "move", pdData);

		stateMachine.Initialize(idleState);
		isAttackReady = true;

		facing = 1;
	}

	public override void Update() {
		base.Update();
		if(currentHealth > 0) {
			if (target == null) {
				CheckForTarget();
			}
			else {
				TargetInRange();
			}
		}
	}

	protected override void Damage(AttackProperties attack) {
		base.Damage(attack);
		if (startStagger) {
			startStagger = false;
			stateMachine.ChangeState(hitState);
			StartCoroutine("StaggerTimer");
		}
	}

	/*
	 * Drops an arrow upon death
	 */
	protected override void Death() {
		if(!dropped) {
			stateMachine.ChangeState(deathState);
			Transform arrow = drop.Find("Arrow");
			GameObject.Instantiate(arrow, aliveGO.transform.position, Quaternion.identity);
			Destroy(gameObject, 1f);
			dropped = true;
		}
	}

	/*
	 * Checks for player using RayCast.
	 * Sets target if player was found
	 */
	public void CheckForTarget() {
		RaycastHit2D hit = Physics2D.Raycast(playerCheck.position, -aliveGO.transform.right, skellyData.detectionRange, baseData.whatIsPlayer);
		if( hit.collider!= null) {
			target = hit.collider.transform;
			stateMachine.ChangeState(pdState);
		}
	}

	// Deletes target if it's out of range
	public void TargetInRange() {
		if ( (target.transform.position - aliveGO.transform.position).magnitude > skellyData.maxAggroRange) {
			target = null;
			stateMachine.ChangeState(idleState);
		}
	}

	public void ResetTarget() {
		target = null;
	}
 
	public void FaceTarget() {
		if (target != null) {
			facing = aliveGO.transform.position.x < target.position.x ? 1 : -1;
			if (facing == 1) {
				aliveGO.transform.rotation = Quaternion.Euler(0, 0, 0);
			}
			else {
				aliveGO.transform.rotation = Quaternion.Euler(0, 180, 0);
			}
		}
		else {
			Debug.Log("No target found");
		}
	}

	public virtual bool CheckWall() {
		return Physics2D.Raycast(wallCheck.position, -aliveGO.transform.right, skellyData.wallCheckDist, baseData.whatIsGround);
	}

	public virtual bool CheckLedge() {
		return Physics2D.Raycast(ledgeCheck.position, Vector2.down, skellyData.ledgeCheckDist, baseData.whatIsGround);
	}

	IEnumerator StaggerTimer() {
		yield return new WaitForSeconds(hitData.staggerTime);
		stateMachine.ChangeState(idleState);
	}

	IEnumerator StartAttackCD() {
		isAttackReady = false;
		yield return new WaitForSeconds(skellyData.attackCD);
		isAttackReady = true;
	}
}
