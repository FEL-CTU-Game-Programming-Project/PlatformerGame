using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public class Slime_PlayerDetectedState : PatrolPlayerDetectedState {
	/*
	 * After a startup of attack is done, slime will enter a chargeState
	 */

	Slime slime;

	public Slime_PlayerDetectedState (FiniteStateMachine stateMachine, Slime entity, string animBoolName, D_PatrolPlayerDetectedState stateData) : base(stateMachine, entity, animBoolName, stateData) {
		slime = entity;
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
		if(Time.time >= startTime + stateData.waitTime) {
			stateMachine.ChangeState(slime.chargeState);
		}
	}

	public override void PhysicsUpdate() {
		base.PhysicsUpdate();
	}
}
