using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FleyeIdleState : State {

	/*
	 * Fleye picks a random location withing home position bounds and fly towards it..
	 * And the cycle continues
	 */

	private Fleye fleye;
	private D_FleyeIdle stateData;

	private bool goAggro;

	public FleyeIdleState(FiniteStateMachine stateMachine, Fleye entity, string animBoolName, D_FleyeIdle stateData) : base(stateMachine, entity, animBoolName) {
		fleye = entity;
		this.stateData = stateData;
	}
	public override void DoChecks() {
		base.DoChecks();
		goAggro = fleye.NearestPlayerDist() <= fleye.fleyeBaseData.detectionDist;
	}

	public override void Enter() {
		base.Enter();
		fleye.agent.ResetPath();
	}

	public override void Exit() {
		base.Exit();
	}

	public override void LogicUpdate() {
		base.LogicUpdate();
		fleye.Flip();
		if (goAggro) {
			fleye.FindTarget();
			fleye.stateMachine.ChangeState(fleye.aggroState);
		}
		if (!fleye.agent.hasPath) {
			float randomDist = Random.Range(0, stateData.maxPatrolDist);
			Vector3 randomDir = Random.insideUnitSphere;
			Vector3 newPosition = fleye.homePosition + randomDir*randomDist;
			fleye.agent.SetDestination(newPosition);
		}
	}

	public override void PhysicsUpdate() {
		base.PhysicsUpdate();
	}
}
