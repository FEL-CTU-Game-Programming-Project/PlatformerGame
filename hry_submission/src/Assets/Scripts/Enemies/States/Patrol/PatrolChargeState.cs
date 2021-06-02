using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolChargeState : State
{
	/*
	 * Skelet of Charge state, performs basic checks and doesn't affect behaviour of the AI
	 */
	protected D_PatrolChargeState stateData;
	private PatrolEntity patrolEntity;

	protected bool isDetectingLedge;
	protected bool isDetectingWall;
	protected bool isChargeOver;
	// A bit of time delay before AI can perform another action, just to give time to breath
	protected bool isEndLagOver;

	public PatrolChargeState(FiniteStateMachine stateMachine, PatrolEntity entity, string animBoolName, D_PatrolChargeState stateData) : base(stateMachine, entity, animBoolName) {
		patrolEntity = entity;
		this.stateData = stateData;
	}

	public override void Enter() {
		base.Enter();
		patrolEntity.StartCoroutine("StartChargeCD");
		patrolEntity.SetVelocity(Vector2.right * patrolEntity.facing, stateData.chargeSpeed);
		isChargeOver = false;
		isEndLagOver = false;
	}

	public override void Exit() {
		base.Exit();
	}

	public override void LogicUpdate() {
		base.LogicUpdate();
		if(Time.time >= startTime + stateData.chargeTime + stateData.endLag) {
			isEndLagOver = true;
		}
		if (Time.time >= startTime + stateData.chargeTime) {
			isChargeOver = true;
		}
	}

	public override void PhysicsUpdate() {
		base.PhysicsUpdate();
	}

	public override void DoChecks() {
		base.DoChecks();
		isDetectingLedge = patrolEntity.CheckLedge();
		isDetectingWall = patrolEntity.CheckWall();
	}
}
