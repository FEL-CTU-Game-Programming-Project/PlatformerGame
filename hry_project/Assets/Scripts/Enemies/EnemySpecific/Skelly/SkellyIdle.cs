using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkellyIdle : State {

	/*
	 * Skeleton waits for a random amount of time
	 * After that it enters a walkState
	 * It will flip based on RNG or flipAfterIdle
	 */

	private Skeleton skelly;
	private D_SkellyIdle stateData;

	private float idleTime;

	private bool flipAfterIdle = false;

	public SkellyIdle(FiniteStateMachine stateMachine, Skeleton entity, string animBoolName, D_SkellyIdle stateData) : base(stateMachine, entity, animBoolName) {
		this.stateData = stateData;
		skelly = entity;
	}

	public override void DoChecks() {
		base.DoChecks();
	}

	public override void Enter() {
		base.Enter();
		Debug.Log("Entering idle");
		idleTime = Random.Range(stateData.minWaitTime, stateData.maxWaitTime);
		entity.SetVelocity(Vector2.zero, 0);
	}

	public override void Exit() {
		base.Exit();
		if (flipAfterIdle) {
			skelly.Flip();
		}
		flipAfterIdle = false;
	}

	public override void LogicUpdate() {
		base.LogicUpdate();
		if (Time.time >= idleTime + startTime) {
			stateMachine.ChangeState(skelly.walkState);
		}
	}

	public override void PhysicsUpdate() {
		base.PhysicsUpdate();
	}
	public void SetFlipAfterIdle(bool flip) {
		flipAfterIdle = flip;
	}
}
