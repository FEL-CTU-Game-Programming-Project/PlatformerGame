using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkellyWalk : State {
	/*
	 * Skelly walks for a random distance forward
	 * If he detects ledge/wall, movement stops and he enters idle
	 */

	private Skeleton skelly;
	private D_SkellyWalk stateData;

	private float walkDist;
	private float initPos;

	private bool stopMoving;


	public SkellyWalk(FiniteStateMachine stateMachine, Skeleton entity, string animBoolName, D_SkellyWalk stateData) : base(stateMachine, entity, animBoolName) {
		this.stateData = stateData;
		skelly = entity;
	}
	public override void DoChecks() {
		base.DoChecks();
		stopMoving = !skelly.CheckLedge() || skelly.CheckWall();
	}

	public override void Enter() {
		base.Enter();
		Debug.Log("Entering walk");
		walkDist = Random.Range(stateData.minWalkDist, stateData.maxWalkDist);
		initPos = skelly.aliveGO.transform.position.x;
		entity.SetVelocity(Vector2.right * skelly.facing, skelly.skellyData.moveSpeed);
	}

	public override void Exit() {
		base.Exit();
	}

	public override void LogicUpdate() {
		base.LogicUpdate();
		float dist = Mathf.Abs(skelly.aliveGO.transform.position.x - initPos);
		if (walkDist < dist) {
			stateMachine.ChangeState(skelly.idleState);
			if (Random.value > .5f) {
				skelly.idleState.SetFlipAfterIdle(true);
			}
		}
		if(stopMoving) {
			stateMachine.ChangeState(skelly.idleState);
			skelly.idleState.SetFlipAfterIdle(true);
		}
	}

	public override void PhysicsUpdate() {
		base.PhysicsUpdate();
	}
}
