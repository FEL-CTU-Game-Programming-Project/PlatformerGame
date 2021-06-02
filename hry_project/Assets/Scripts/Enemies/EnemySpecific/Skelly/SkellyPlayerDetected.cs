using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkellyPlayerDetected : State {

	/*
	 * Enters upon interrupt from Skeleton script
	 * Follow player and if player is in range for attack, enters a1State
	 */

	private Skeleton skelly;
	private D_SkellyPD stateData;

	private bool obstacleDetected;

	public SkellyPlayerDetected(FiniteStateMachine stateMachine, Skeleton entity, string animBoolName, D_SkellyPD stateData) : base(stateMachine, entity, animBoolName) {
		this.stateData = stateData;
		skelly = entity;
	}

	public override void DoChecks() {
		base.DoChecks();
		obstacleDetected = !skelly.CheckLedge() || skelly.CheckWall();
	}

	public override void Enter() {
		base.Enter();
		Debug.Log("Entering PlayerDetected");
	}

	public override void Exit() {
		base.Exit();
	}

	public override void LogicUpdate() {
		base.LogicUpdate();
		skelly.FaceTarget();
		skelly.SetVelocity(Vector2.right * skelly.facing, skelly.skellyData.moveSpeed);
		float posDiff = Mathf.Abs(skelly.aliveGO.transform.position.x - skelly.target.position.x);
		if (posDiff <= stateData.attackOffset && skelly.isAttackReady) {
			stateMachine.ChangeState(skelly.a1State);
			skelly.StartCoroutine("StartAttackCD");
		}
		if (obstacleDetected) {
			skelly.ResetTarget();
			stateMachine.ChangeState(skelly.idleState);
			skelly.idleState.SetFlipAfterIdle(true);
		}
	}

	public override void PhysicsUpdate() {
		base.PhysicsUpdate();
	}
}
