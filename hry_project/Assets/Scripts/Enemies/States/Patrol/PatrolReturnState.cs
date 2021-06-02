using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolReturnState : State {
	/*
	 * Used to return enemy back inside bounds after charge
	 * This script only performs basic checks for decision making in inheriting scripts
	 */
	protected D_PatrolReturnState stateData;
	private PatrolEntity patrolEntity;

	protected bool isInsideBounds; // If bounds are not specified, it's set to true
	protected bool isPlayerDetected;
	protected int boundsDirection;

	public PatrolReturnState(FiniteStateMachine stateMachine, PatrolEntity entity, string animBoolName, D_PatrolReturnState stateData) : base(stateMachine, entity, animBoolName) {
		this.stateData = stateData;
		patrolEntity = entity;
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

	public override void DoChecks() {
		base.DoChecks();
		isInsideBounds = patrolEntity.IsInsideBounds();
		isPlayerDetected = patrolEntity.CheckPlayerMinRange();
		boundsDirection = patrolEntity.CheckBoundsDir();
	}
}
