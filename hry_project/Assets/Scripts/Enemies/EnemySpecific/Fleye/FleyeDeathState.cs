using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FleyeDeathState : State {
	/*
	 * Curls up into ball and gets flung far away
	 */

	private Fleye fleye;
	private D_FleyeDeath stateData;

	public FleyeDeathState(FiniteStateMachine stateMachine, Fleye entity, string animBoolName, D_FleyeDeath stateData) : base(stateMachine, entity, animBoolName) {
		fleye = entity;
		this.stateData = stateData;
	}

	public override void DoChecks() {
		base.DoChecks();
	}

	// Same as hit state, but gets flung much further
	public override void Enter() {
		base.Enter();
		fleye.rb.gravityScale = 1;
		fleye.rb.freezeRotation = false;
		fleye.agent.ResetPath();
		fleye.SetVelocity(Vector2.right * fleye.knockbackDirection, stateData.knockbackSpeed);
		fleye.aliveGO.GetComponent<SpriteRenderer>().color = stateData.deathColour;
	}

	public override void Exit() {
		fleye.rb.gravityScale = 0;
		base.Exit();
	}

	public override void LogicUpdate() {
		base.LogicUpdate();
	}

	public override void PhysicsUpdate() {
		base.PhysicsUpdate();
	}
}
