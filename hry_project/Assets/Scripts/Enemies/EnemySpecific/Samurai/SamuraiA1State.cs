using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SamuraiA1State : State {
	/*
	 * Just simple waiting for animation to end
	 */
	private Samurai samurai;
	private D_SamuraiA1 stateData;

	public SamuraiA1State(FiniteStateMachine stateMachine, Samurai entity, string animBoolName, D_SamuraiA1 stateData) : base(stateMachine, entity, animBoolName) {
		samurai = entity;
		this.stateData = stateData;
	}

	public override void DoChecks() {
		base.DoChecks();
	}

	public override void Enter() {
		base.Enter();
	}

	public override void Exit() {
		base.Exit();
	}

	public override void LogicUpdate() {
		base.LogicUpdate();
	}

	public override void PhysicsUpdate() {
		base.PhysicsUpdate();
	}

	public void AnimEndEvent() {
		if(Random.value < 0.7) {
			samurai.dashState.distance = samurai.NearestPlayerDist();
			samurai.dashState.exitState = samurai.a2State;
			samurai.stateMachine.ChangeState(samurai.dashState);
		} else {
			samurai.stateMachine.ChangeState(samurai.idleState);
		}
	}
}
