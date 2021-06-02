using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slime_ChargeState : PatrolChargeState
{
	/*
	 * This states makes Slime charge in direction of the player
	 * Movement is stopped upon detecting wall or ledge
	 */
    Slime slime;

	private bool isMovementStopped;
	public Slime_ChargeState(FiniteStateMachine stateMachine, Slime entity, string animBoolName, D_PatrolChargeState stateData) : base(stateMachine, entity, animBoolName, stateData) {
        slime = entity;
	}

	public override void DoChecks() {
		base.DoChecks();
	}

	public override void Enter() {
		base.Enter();
		isMovementStopped = false;
	}

	public override void Exit() {
		base.Exit();
	}

	public override void LogicUpdate() {
		base.LogicUpdate();
		if (isEndLagOver) {
			stateMachine.ChangeState(slime.returnState);
		} else if(isChargeOver && !isMovementStopped) {
			isMovementStopped = true;
			slime.SetVelocity(Vector2.zero, 0);
		}
		if( (!isDetectingLedge || isDetectingWall) && !isMovementStopped ) {
			slime.SetVelocity(Vector2.zero, 0);
		}
	}

	public override void PhysicsUpdate() {
		base.PhysicsUpdate();
	}
}
