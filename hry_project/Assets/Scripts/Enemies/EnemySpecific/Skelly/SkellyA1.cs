using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkellyA1 : State
{
	/*
	 * Attacks forward with small forward movement
	 */
	private Skeleton skelly;
	private D_SkellyA1 stateData;

	private bool stopMoving;

	public SkellyA1(FiniteStateMachine stateMachine, Skeleton entity, string animBoolName, D_SkellyA1 stateData) : base(stateMachine, entity, animBoolName) {
		this.stateData = stateData;
		skelly = entity;
	}

	public override void DoChecks() {
		base.DoChecks();
		stopMoving = !skelly.CheckLedge() || skelly.CheckWall();
	}

	public override void Enter() {
		base.Enter();
		Debug.Log("Entering attack");
		skelly.FaceTarget();
		entity.SetVelocity(Vector2.right * skelly.facing, stateData.attackMoveSpeed);
	}

	public override void Exit() {
		base.Exit();
	}

	public override void LogicUpdate() {
		base.LogicUpdate();
		if(stopMoving) {
			entity.SetVelocity(Vector2.zero, skelly.skellyData.moveSpeed);
		}
	}

	public override void PhysicsUpdate() {
		base.PhysicsUpdate();
	}

	public void AnimEnd() {
		stateMachine.ChangeState(skelly.pdState);
	}
}
