using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SamuraiHitState : State {
	/*
	 * Simple stagger
	 */

	private Samurai samurai;
	private D_SamuraiHit stateData;
	public int direction = -1;
	public State previousState;

	public SamuraiHitState(FiniteStateMachine stateMachine, Samurai entity, string animBoolName, D_SamuraiHit stateData) : base(stateMachine, entity, animBoolName) {
		samurai = entity;
		this.stateData = stateData;
	}

	public override void DoChecks() {
		base.DoChecks();
	}

	public override void Enter() {
		base.Enter();
		samurai.SetVelocity(Vector2.right * direction, stateData.staggerSpeed);
	}

	public override void Exit() {
		base.Exit();
		samurai.SetVelocity(Vector2.zero, 0);
	}

	public override void LogicUpdate() {
		base.LogicUpdate();
	}

	public override void PhysicsUpdate() {
		base.PhysicsUpdate();
	}
	public void AnimEndEvent() {
		samurai.stateMachine.ChangeState(samurai.idleState);
	}
}
