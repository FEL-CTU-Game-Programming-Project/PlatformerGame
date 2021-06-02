using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FleyeHitState : State {

	/*
	 * Finally, funnier hit state
	 * Gets flung in a knockback direction and falls onto ground
	 */
	private Fleye fleye;
	private D_FleyeHit stateData;

	private Quaternion rotation;

	public FleyeHitState(FiniteStateMachine stateMachine, Fleye entity, string animBoolName, D_FleyeHit stateData) : base(stateMachine, entity, animBoolName) {
		fleye = entity;
		this.stateData = stateData;
	}
	public override void DoChecks() {
		base.DoChecks();
	}

	/*
	 * Turns on gravity, unlocks rotation, deletes path and sets velocity
	 */
	public override void Enter() {
		base.Enter();
		rotation = fleye.aliveGO.transform.rotation;
		fleye.rb.gravityScale = 1;
		fleye.rb.freezeRotation = false;
		fleye.agent.ResetPath();
		fleye.SetVelocity(Vector2.right * fleye.knockbackDirection, stateData.knockbackSpeed);
	}

	/*
	 * Fixes up our cute lil' Fleye
	 */
	public override void Exit() {
		base.Exit();
		fleye.rb.gravityScale = 0;
		fleye.rb.freezeRotation = true;
		fleye.aliveGO.transform.rotation = rotation;
		fleye.FaceTarget();
	}

	public override void LogicUpdate() {
		base.LogicUpdate();
	}

	public override void PhysicsUpdate() {
		base.PhysicsUpdate();
	}
}
