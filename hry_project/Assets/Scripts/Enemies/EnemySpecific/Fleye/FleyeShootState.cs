using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FleyeShootState : State {

	/*
	 * Flies back a bit and then shoots a projectile when AnimEvent calls for it.
	 */

	private Fleye fleye;
	private D_FleyeShoot stateData;

	public FleyeShootState(FiniteStateMachine stateMachine, Fleye entity, string animBoolName, D_FleyeShoot stateData) : base(stateMachine, entity, animBoolName) {
		fleye = entity;
		this.stateData = stateData;
	}
	public override void DoChecks() {
		base.DoChecks();
	}

	public override void Enter() {
		base.Enter();
		fleye.FaceTarget();
		Vector3 offset = new Vector3(stateData.shuffle.x * (-fleye.facing), stateData.shuffle.y, 0);
		fleye.agent.SetDestination(fleye.aliveGO.transform.position + offset);
	}

	public override void Exit() {
		base.Exit();
	}

	public override void LogicUpdate() {
		base.LogicUpdate();
		fleye.FaceTarget();
	}

	public override void PhysicsUpdate() {
		base.PhysicsUpdate();
	}

	/*
	 * Shoots and rotates projectile to face the target
	 */
	public void CreateProjectile() {
		float rotZ = Vector3.Angle(fleye.aliveGO.transform.position, fleye.target.position);
		Transform projectile = GameObject.Instantiate(fleye.projectile, fleye.aliveGO.transform.position, Quaternion.Euler(0, 0, rotZ));
		FleyeProjectile props = projectile.GetComponent<FleyeProjectile>();
		props.SetDirection(fleye.target);
	}

	public void AnimEnd() {
		stateMachine.ChangeState(fleye.aggroState);
	}
}
