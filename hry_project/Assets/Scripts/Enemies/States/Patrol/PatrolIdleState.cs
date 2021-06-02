using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolIdleState : State
{
	/*
	 * Skelet of state that just waits in place for random ammount of time
	 * Performs only check for player
	 */
	private PatrolEntity patrolEntity;

	protected D_PatrolIdleState stateData;

	protected bool flipAfterIdle;
	protected bool isIdleTimeOver;
	protected bool isPlayerInMinAgroRange;
	protected float idleTime;

	public PatrolIdleState(FiniteStateMachine stateMachine, PatrolEntity entity, string animBoolName, D_PatrolIdleState stateData) : base(stateMachine, entity, animBoolName) {
		patrolEntity = entity;
		this.stateData = stateData;
	}

	public override void Enter() {
		base.Enter();
		isIdleTimeOver = false;
		SetRandomIdleTime();
		entity.SetVelocity(Vector2.zero, 0f);
	}

	public override void Exit() {
		if (flipAfterIdle) {
			patrolEntity.Flip();
		}
		base.Exit();
	}

	public override void LogicUpdate() {
		base.LogicUpdate();
		if (Time.time >= idleTime + startTime) {
			isIdleTimeOver = true;
		}
	}

	public override void PhysicsUpdate() {
		base.PhysicsUpdate();
	}

	public void SetFlipAfterIdle(bool flip) {
		flipAfterIdle = flip;
	}

	private void SetRandomIdleTime() {
		idleTime = Random.Range(stateData.minIdleTime, stateData.maxIdleTime);
	}

	public override void DoChecks() {
		base.DoChecks();
		isPlayerInMinAgroRange = patrolEntity.CheckPlayerMinRange();
	}
}
