using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkellyHit : State
{
	/*
	 * Applies knockback, exits to playedDetectedState 
	 */
	private Skeleton skelly;
	private D_SkellyHit stateData;

	public SkellyHit(FiniteStateMachine stateMachine, Skeleton entity, string animBoolName, D_SkellyHit stateData) : base(stateMachine, entity, animBoolName) {
		this.stateData = stateData;
		skelly = entity;
	}
	public override void DoChecks() {
		base.DoChecks();
	}

	public override void Enter() {
		base.Enter();
		entity.SetVelocity(Vector2.right * skelly.knockbackDirection, stateData.staggerSpeed);
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
}
