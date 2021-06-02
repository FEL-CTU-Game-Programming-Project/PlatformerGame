using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SamuraiShuffleState : State {
	/*
	 * Moves in a random direction, when detecting a wall stops the movement
	 */

	private Samurai samurai;
	private D_SamuraiShuffle stateData;

	private float finalPosition;
	private float startingPosition;
	private float distance;

	public SamuraiShuffleState(FiniteStateMachine stateMachine, Samurai entity, string animBoolName, D_SamuraiShuffle stateData) : base(stateMachine, entity, animBoolName) {
		samurai = entity;
		this.stateData = stateData;
	}
	public override void DoChecks() {
		base.DoChecks();
	}

	/*
	 * Sets direction randomly. If wall is detected using RayCast, make the distance shorter 
	 * (Skeletons/Slime wall check would be smarter)
	 */
	public override void Enter() {
		base.Enter();
		if (Random.value < 0.3) {
			samurai.Flip();
		}
		distance = Random.Range(stateData.minDist, stateData.maxDist);
		startingPosition = samurai.aliveGO.transform.position.x;
		samurai.SetVelocity(samurai.facing * Vector2.right, samurai.samuraiBaseData.movementSpeed);

		RaycastHit2D hit = Physics2D.Raycast(samurai.aliveGO.transform.position, Vector2.right * samurai.facing,
												Mathf.Abs(distance), samurai.baseData.whatIsGround);
		if (hit.collider != null) {
			distance = (hit.distance - stateData.bodyOffset);
		}
	}

	public override void Exit() {
		base.Exit();
		samurai.SetVelocity(Vector2.zero, 0);
	}

	public override void LogicUpdate() {
		base.LogicUpdate();
		if ( Mathf.Abs(startingPosition - samurai.aliveGO.transform.position.x) >= distance) {
			samurai.stateMachine.ChangeState(samurai.idleState);
		}
	}

	public override void PhysicsUpdate() {
		base.PhysicsUpdate();
	}
}
