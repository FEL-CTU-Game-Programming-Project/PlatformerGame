using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolPlayerDetectedState : State {
	/*
	 * Skelet of state for player detection.
	 * Performs only basic checks to help with decision making
	 */

	protected D_PatrolPlayerDetectedState stateData;

	protected bool isDetectingWall;
	protected bool isDetectingLedge;

	protected bool isPlayerInMinAgroRange;
	protected bool isPlayerInMaxAgroRange;

	private PatrolEntity patrolEntity;

	public PatrolPlayerDetectedState(FiniteStateMachine stateMachine, PatrolEntity entity, string animBoolName, D_PatrolPlayerDetectedState stateData) : base(stateMachine, entity, animBoolName) {
		this.stateData = stateData;
		patrolEntity = entity;
	}

	public override void Enter() {
		base.Enter();
		patrolEntity.SetVelocity(Vector2.zero, 0f);
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

	public override void DoChecks() {
		base.DoChecks();
		isDetectingLedge = patrolEntity.CheckLedge();
		isDetectingWall = patrolEntity.CheckWall();
		isPlayerInMinAgroRange = patrolEntity.CheckPlayerMinRange();
		isPlayerInMaxAgroRange = patrolEntity.CheckPlayerMaxRange();	
	}
}
