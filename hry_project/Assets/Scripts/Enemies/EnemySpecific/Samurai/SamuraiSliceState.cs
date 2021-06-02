using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SamuraiSliceState : State {

	/*
	 * Starts SlashCycle coroutine and waits till SlashCycle changes state
	 */
	private Samurai samurai;
	private D_SamuraiSlice stateData;

	public SamuraiSliceState(FiniteStateMachine stateMachine, Samurai entity, string animBoolName, D_SamuraiSlice stateData) : base(stateMachine, entity, animBoolName) {
		samurai = entity;
		this.stateData = stateData;
	}

	public override void DoChecks() {
		base.DoChecks();
	}

	public override void Enter() {
		base.Enter();
		samurai.FacePlayer();
		samurai.StartCoroutine("SlashCycle");
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
