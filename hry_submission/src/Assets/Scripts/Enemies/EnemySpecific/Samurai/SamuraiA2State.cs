using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SamuraiA2State : State
{
	/*
	 * Just simple waiting for animation to end
	 */
	private Samurai samurai;
	private D_SamuraiA2 stateData;

	public SamuraiA2State(FiniteStateMachine stateMachine, Samurai entity, string animBoolName, D_SamuraiA2 stateData) : base(stateMachine, entity, animBoolName) {
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
		samurai.stateMachine.ChangeState(samurai.idleState);
	}
}
