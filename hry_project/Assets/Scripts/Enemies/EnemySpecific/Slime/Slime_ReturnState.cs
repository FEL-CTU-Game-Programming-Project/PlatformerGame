using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slime_ReturnState : PatrolReturnState {
	// Explained in PatrolReturnState

	Slime slime;
	public Slime_ReturnState(FiniteStateMachine stateMachine, Slime entity, string animBoolName, D_PatrolReturnState stateData) : base(stateMachine, entity, animBoolName, stateData) {
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

	/*
	 * Makes slime go back inside of given boundaries.
	 */
	public override void LogicUpdate() {
		base.LogicUpdate();
		if(boundsDirection != slime.facing && boundsDirection != 0) {
			slime.Flip();
		}
		slime.SetVelocity(Vector2.right * boundsDirection, stateData.movementSpeed);
		if (isPlayerDetected && slime.isChargeReady) {
			stateMachine.ChangeState(slime.playerDetectedState);
		} else if (isInsideBounds) {
			stateMachine.ChangeState(slime.moveState);
		}
	}

	public override void PhysicsUpdate() {
		base.PhysicsUpdate();
	}
}
