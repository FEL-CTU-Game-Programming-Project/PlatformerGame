using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SamuraiDashState : State {

	/*
	 * Dash towards nearest player with wall detection
	 * (prolly pointless, but boss room is kinda finnicky)
	 */

	private Samurai samurai;
	private D_SamuraiDash stateData;

	public float distance = 0f;
	private float startingPosition;
	public State exitState;

	public SamuraiDashState(FiniteStateMachine stateMachine, Samurai entity, string animBoolName, D_SamuraiDash stateData) : base(stateMachine, entity, animBoolName) {
		samurai = entity;
		this.stateData = stateData;
	}

	public override void DoChecks() {
		base.DoChecks();
	}

	/*
	 * If player is in range for next action, go to exit state
	 * Otherwise face player and send out raycast to check for walls
	 */
	public override void Enter() {
		startTime = Time.time;
		if(Mathf.Abs(distance) < stateData.inRange) {
			samurai.stateMachine.ChangeState(exitState);
			return;
		}
		samurai.ghost.generateGhost = true;
		startingPosition = samurai.aliveGO.transform.position.x;
		if (distance < 0 && samurai.facing > 0) {
			samurai.Flip();
		} else if (distance > 0 && samurai.facing < 0) {
			samurai.Flip();
		}
		RaycastHit2D hit = Physics2D.Raycast(samurai.aliveGO.transform.position, Vector2.right * samurai.facing, 
												Mathf.Abs(distance), samurai.baseData.whatIsGround);
		if(hit.collider != null) {
			distance = hit.distance * samurai.facing;
		}
		samurai.SetVelocity(samurai.facing * Vector2.right, samurai.samuraiBaseData.dashSpeed);
	}

	public override void Exit() {
		samurai.ghost.generateGhost = false;
		samurai.SetVelocity(Vector2.zero, 0);
	}

	/*
	 * Checks if samurai has traveled given distance, added padding so samurai is in sword range
	 * Without padding samurai might hit player with his body, thus lowering his damage
	 */
	public override void LogicUpdate() {
		base.LogicUpdate();
		if (Mathf.Abs(startingPosition - samurai.aliveGO.transform.position.x) >= Mathf.Abs(distance) - stateData.inRange) {
			samurai.stateMachine.ChangeState(exitState);
		}
	}

	public override void PhysicsUpdate() {
		base.PhysicsUpdate();
	}
}
