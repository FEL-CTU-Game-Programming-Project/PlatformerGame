using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SamuraiDeathState : State {
	/*
	 * Simple death state
	 */
	private Samurai samurai;
	private D_SamuraiDeath stateData;
	public int direction = -1;
	public State previousState;

	public SamuraiDeathState(FiniteStateMachine stateMachine, Samurai entity, string animBoolName, D_SamuraiDeath stateData) : base(stateMachine, entity, animBoolName) {
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
}
