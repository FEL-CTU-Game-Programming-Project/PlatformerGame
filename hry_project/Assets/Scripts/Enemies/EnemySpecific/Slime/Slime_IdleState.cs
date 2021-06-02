using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slime_IdleState : PatrolIdleState {
	/*
	 * Stops slime for random ammount of time
	 * If slime detects player it enters playerDetectedState, otherwise it enters moveState
	 */
	private Slime slime;
	public Slime_IdleState(FiniteStateMachine stateMachine, Slime entity, string animBoolName, D_PatrolIdleState stateData) : base(stateMachine, entity, animBoolName, stateData) {
		slime = entity;
	}

	public override void Enter() {
		base.Enter();
	}

	public override void Exit() {
		base.Exit();
	}

	public override void LogicUpdate() {
		base.LogicUpdate();
		if (isPlayerInMinAgroRange && slime.isChargeReady) {
			SetFlipAfterIdle(false);
			stateMachine.ChangeState(slime.playerDetectedState);
		} else if (isIdleTimeOver) {
			stateMachine.ChangeState(slime.moveState);
		}
	}

	public override void PhysicsUpdate() {
		base.PhysicsUpdate();
	}
}
