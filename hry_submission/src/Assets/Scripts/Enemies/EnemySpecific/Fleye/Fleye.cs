using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Fleye : Entity {

	/*
	 * Flying enemy with bad targetting design. Too late to fix now
	 * Holds all states, state data and helping functions for States
	 * Pathfinding is done using NavMeshPlus
	 * Link: https://github.com/h8man/NavMeshPlus/wiki/HOW-TO
	 */

	public Transform player1, player2;
	public bool isAttackReady { get; private set; } = true;
	public Vector3 homePosition { get; private set; }
	
	public Transform target { get; protected set; }
	public NavMeshAgent agent;

	public FleyeIdleState idleState { get; private set; }
	public FleyeAggroState aggroState { get; private set; }
	public FleyeChargeState chargeState { get; private set; }
	public FleyeDeathState deathState { get; private set; }
	public FleyeHitState hitState { get; private set; }
	public FleyeShootState shootState { get; private set; }

	[SerializeField]
	private D_FleyeAggro aggroData;
	[SerializeField]
	private D_FleyeCharge chargeData;
	[SerializeField]
	private D_FleyeDeath deathData;
	[SerializeField]
	private D_FleyeHit hitData;
	[SerializeField]
	private D_FleyeIdle idleData;
	[SerializeField]
	private D_FleyeShoot shootData;

	public Transform projectile;

	[SerializeField]
	public D_BaseFleye fleyeBaseData;

	public override void FixedUpdate() {
		base.FixedUpdate();
	}

	public override void SetVelocity(Vector2 direction, float velocity) {
		base.SetVelocity(direction, velocity);
	}

	public override void AnimEvent(string message) {
		base.AnimEvent(name);
		if(message.Equals("charge end") && stateMachine.currentState.Equals(chargeState)) {
			chargeState.AnimEnded();
		} else if (message.Equals("shoot end") && stateMachine.currentState.Equals(shootState)) {
			shootState.AnimEnd();
		} else if (message.Equals("shoot") && stateMachine.currentState.Equals(shootState)) {
			shootState.CreateProjectile();
		}
	}

	public override void Start() {
		base.Start();

		if (player1 == null) {
			Debug.Log("No player set, stoopid");
			Destroy(gameObject);
		}
		homePosition = aliveGO.transform.position;
		facing = 1;

		agent = aliveGO.GetComponent<NavMeshAgent>();
		agent.updateRotation = false;
		agent.updateUpAxis = false;
		agent.speed = fleyeBaseData.flightSpeed;
		agent.acceleration = fleyeBaseData.acceleration;

		idleState = new FleyeIdleState(stateMachine, this, "idle", idleData);
		hitState = new FleyeHitState(stateMachine, this, "hit", hitData);
		aggroState = new FleyeAggroState(stateMachine, this, "aggro", aggroData);
		chargeState = new FleyeChargeState(stateMachine, this, "charge", chargeData);
		deathState = new FleyeDeathState(stateMachine, this, "death", deathData);
		shootState = new FleyeShootState(stateMachine, this, "shoot", shootData);

		stateMachine.Initialize(idleState);
	}

	public override void Update() {
		base.Update();
	}

	protected override void Death() {
		stateMachine.ChangeState(deathState);
	}

	public void TrackTarget() {
		if(target != null) {
			Vector3 offset = new Vector3((-facing) * fleyeBaseData.offsetX, fleyeBaseData.offsetY);
			agent.SetDestination(target.position + offset);
		}
	}

	// Finds nearest player and returns it's distance
	public float NearestPlayerDist() {
		if(player1 == null) {
			Debug.LogError("P1 not set!");
			return float.MaxValue;
		}
		float minDist;
		Vector3 alivePos = aliveGO.transform.position;
		if (player2 == null) {
			minDist = (player1.position - alivePos).magnitude;
		}
		else {
			float p1Dist = (player1.position - alivePos).magnitude;
			float p2Dist = (player2.position - alivePos).magnitude;
			minDist = p1Dist < p2Dist ? p1Dist : p2Dist;
		}
		return minDist;
	}

	// Finds nearest player and if he is in aggro range, sets him as an target
	public void FindTarget() {
		if (player1 == null) {
			Debug.LogError("P1 not set!");
			return;
		}
		Vector3 alivePos = aliveGO.transform.position;
		if (player2 == null) {
			if ((player1.position - alivePos).magnitude < fleyeBaseData.detectionDist) {
				target = player1;
			}
		}
		else {
			float p1Dist = (player1.position - alivePos).magnitude;
			float p2Dist = (player2.position - alivePos).magnitude;
			if(p1Dist < fleyeBaseData.detectionDist || p2Dist < fleyeBaseData.detectionDist) {
				target = p1Dist < p2Dist ? player1 : player2;
			}
		}
	}

	public virtual void GetHome() {
		agent.SetDestination(homePosition);
	}

	protected override void Damage(AttackProperties attack) {
		base.Damage(attack);
		if (startStagger) {
			startStagger = false;
			StartCoroutine("StartStagger");
		}
	}

	public override void HitBoxTrigger(Collider2D other) {
		base.HitBoxTrigger(other);
	}

	public void FaceTarget() {
		if(target != null) {
			facing = aliveGO.transform.position.x < target.position.x ? 1 : -1;
			if (facing == 1) {
				aliveGO.transform.rotation = Quaternion.Euler(0, 0, 0);
			}
			else {
				aliveGO.transform.rotation = Quaternion.Euler(0, 180, 0);
			}
		} else {
			Debug.Log("No target found");
		}
	}

	public override void Flip() {
		facing = agent.destination.x > aliveGO.transform.position.x ? 1 : -1;
		if(facing == 1) {
			aliveGO.transform.rotation = Quaternion.Euler(0, 0, 0);
		} else {
			aliveGO.transform.rotation = Quaternion.Euler(0, 180, 0);
		}
	}

	IEnumerator StartAttackCD() {
		isAttackReady = false;
		yield return new WaitForSeconds(fleyeBaseData.attackCD);
		isAttackReady = true;
	}

	IEnumerator StartStagger() {
		stateMachine.ChangeState(hitState);
		yield return new WaitForSeconds(hitData.staggerTime);
		stateMachine.ChangeState(idleState);
	}

	private void OnDrawGizmos() {
		if(homePosition != null && aliveGO != null) {
			Gizmos.color = Color.green;
			Gizmos.DrawWireSphere(homePosition, idleData.maxPatrolDist);
			Gizmos.color = Color.yellow;
			Gizmos.DrawWireSphere(aliveGO.transform.position, fleyeBaseData.detectionDist);
			Gizmos.color = Color.red;
			Gizmos.DrawWireSphere(aliveGO.transform.position, aggroData.attackRange);
		} else {
			Vector3 pos = transform.position;
			Gizmos.color = Color.green;
			Gizmos.DrawWireSphere(pos, idleData.maxPatrolDist);
			Gizmos.color = Color.yellow;
			Gizmos.DrawWireSphere(pos, fleyeBaseData.detectionDist);
			Gizmos.color = Color.red;
			Gizmos.DrawWireSphere(pos, aggroData.attackRange);
		}
		
	}
}
