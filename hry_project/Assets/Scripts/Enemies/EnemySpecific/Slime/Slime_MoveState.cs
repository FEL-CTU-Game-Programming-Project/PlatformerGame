using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slime_MoveState : PatrolMoveState {

	/*
	 * This state makes slime move forward until a ledge, wall or player is detected
	 * If player is detected, it enters playerDetectedState, otherwise it goes into idle
	 */
	private Slime slime;
	public Slime_MoveState(FiniteStateMachine stateMachine, Slime entity, string animBoolName, D_PatrolMoveState stateData) : base(stateMachine, entity, animBoolName, stateData) {
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

		if(isPlayerInMinAgroRange && slime.isChargeReady) {
			stateMachine.ChangeState(slime.playerDetectedState);
		} else if (isDetectingWall || !isDetectingLedge || boundaryDetected) {
			slime.idleState.SetFlipAfterIdle(true);
			stateMachine.ChangeState(slime.idleState);
		}
	}

	public override void PhysicsUpdate() {
		base.PhysicsUpdate();
	}
}
