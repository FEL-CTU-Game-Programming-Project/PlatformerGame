using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SamuraiIdleState : State {
	/*
	 * Wait for a random ammount of time and then semi-randomly picks next action
	 */

	private Samurai samurai;
	private D_SamuraiIdle stateData;
	
	private float waitTime;

	public SamuraiIdleState(FiniteStateMachine stateMachine, Samurai entity, string animBoolName, D_SamuraiIdle stateData) : base(stateMachine, entity, animBoolName) {
		samurai = entity;
		this.stateData = stateData;
	}
	public override void DoChecks() {
		base.DoChecks();
	}

	public override void Enter() {
		base.Enter();
		waitTime = Random.Range(stateData.minWaitTime, stateData.maxWaitTime);
	}

	public override void Exit() {
		base.Exit();
	}

	public override void LogicUpdate() {
		samurai.FacePlayer();
		if (Time.time > startTime + waitTime) {
			samurai.stateMachine.ChangeState( PickMove() );
		}
		base.LogicUpdate();
	}

	public override void PhysicsUpdate() {
		base.PhysicsUpdate();
	}

	/*
	 * Summary:
	 *	If the player if withing dash distance
	 *		50% chance for dash attack
	 *		30% chance for reposition
	 *		20% chance for anime slice
	 *	Otherwise
	 *		30% chance for anime slice
	 *		70% chance for reposition
	 */
	private State PickMove() {
		float minDist = samurai.NearestPlayerDist();
		State state;
		if (Mathf.Abs(minDist) <= samurai.samuraiBaseData.maxDashDist) {
			float choice = Random.value;
			if (choice <= 0.5f) {
				state = samurai.dashState;
				samurai.dashState.distance = minDist;
				samurai.dashState.exitState = samurai.a1State;
			} else if (choice <= 0.80f) {
				state = samurai.shuffleState;
			} else {
				state = samurai.sliceState;
			}
		} else {
			float choice = Random.value;
			if (choice <= 0.3f) {
				state = samurai.sliceState;
			}
			else {
				state = samurai.shuffleState;
			}
		}
		return state;
	}
}
