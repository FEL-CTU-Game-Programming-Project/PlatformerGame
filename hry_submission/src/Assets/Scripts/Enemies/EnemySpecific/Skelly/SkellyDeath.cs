using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SkellyDeath : State {
	private Skeleton skelly;
	private D_SkellyDeath stateData;

	public SkellyDeath(FiniteStateMachine stateMachine, Skeleton entity, string animBoolName, D_SkellyDeath stateData) : base(stateMachine, entity, animBoolName) {
		this.stateData = stateData;
		skelly = entity;
	}
	public override void DoChecks() {
		base.DoChecks();
	}

	public override void Enter() {
		base.Enter();
		entity.SetVelocity(Vector2.zero, skelly.skellyData.moveSpeed);
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
