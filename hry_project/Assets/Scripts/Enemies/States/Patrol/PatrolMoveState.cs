using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolMoveState : State {
	/*
	 * Skelet of state for movement
	 * This one makes the enemy move forward automatically (bad design)
	 */
	protected D_PatrolMoveState stateData;
	protected bool isDetectingWall;
	protected bool isDetectingLedge;
	protected bool boundaryDetected;
	protected bool isPlayerInMinAgroRange;

	private PatrolEntity patrolEntity;


	public PatrolMoveState(FiniteStateMachine stateMachine, PatrolEntity entity, string animBoolName, D_PatrolMoveState stateData) : base(stateMachine, entity, animBoolName) {
		this.stateData = stateData;
		patrolEntity = entity;
	}

	public override void Enter() {
		base.Enter();
		entity.SetVelocity(Vector2.right * patrolEntity.facing, stateData.movementSpeed);
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
		boundaryDetected = patrolEntity.IsOutOfBounds();
		isPlayerInMinAgroRange = patrolEntity.CheckPlayerMinRange();
	}
}
