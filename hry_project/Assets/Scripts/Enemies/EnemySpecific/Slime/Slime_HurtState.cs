using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slime_HurtState : State {
	/*
	 * Hurt state is entered when slime gets staggered
	 * UNFUNNY REMARK: Yes, water is indeed made of water
	 */

	private Slime slime;
	private D_SlimeHurt stateData;
	public Slime_HurtState(FiniteStateMachine stateMachine, Slime entity, string animBoolName, D_SlimeHurt stateData) : base(stateMachine, entity, animBoolName) {
		slime = entity;
		this.stateData = stateData;
	}
	public override void DoChecks() {
		base.DoChecks();
	}

	public override void Enter() {
		base.Enter();
		slime.SetVelocity(Vector2.right * slime.knockbackDirection, stateData.staggerSpeed);
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
