using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FleyeAggroState : State {

	/*
	 * Follows target and keeps itself in certain distance 
	 * Way too well, might need a bit of noise.
	 * If player is in range for charge/projectile, transfers to that state
	 */

	private Fleye fleye;
	private D_FleyeAggro stateData;

	private bool endAggro;
	private bool chargeable;
	private bool projectileReady;

	public FleyeAggroState(FiniteStateMachine stateMachine, Fleye entity, string animBoolName, D_FleyeAggro stateData) : base(stateMachine, entity, animBoolName) {
		fleye = entity;
		this.stateData = stateData;
	}

	public override void DoChecks() {
		base.DoChecks();
		if (fleye.target != null) {
			endAggro = (fleye.target.position - fleye.aliveGO.transform.position).magnitude > fleye.fleyeBaseData.maxDist;
			projectileReady = (fleye.target.position - fleye.aliveGO.transform.position).magnitude < stateData.projectileRange;
			chargeable = (fleye.target.position - fleye.aliveGO.transform.position).magnitude < stateData.attackRange;
		}
	}

	public override void Enter() {
		base.Enter();
		fleye.agent.ResetPath();
		fleye.FaceTarget();
		fleye.TrackTarget();
	}

	public override void Exit() {
		base.Exit();
	}

	/*
	 * Lengthy LogicUpdate boild down to:
	 *	If you can shoot -> Shoot
	 *	If you can charge -> LEEEEROY JEENKINS
	 *	If you can do both -> Flip a coin
	 *	Otherwise just stalk the target
	 */
	public override void LogicUpdate() {
		fleye.FaceTarget();
		fleye.TrackTarget();
		if (endAggro) {
			stateMachine.ChangeState(fleye.idleState);
		}
		if(fleye.isAttackReady) {
			if(chargeable && projectileReady) {
				float random = Random.value;
				if ( random < .4f ) {
					fleye.StartCoroutine("StartAttackCD");
					stateMachine.ChangeState(fleye.shootState);
				} else {
					fleye.StartCoroutine("StartAttackCD");
					stateMachine.ChangeState(fleye.chargeState);
				}
			} else if (chargeable) {
				fleye.StartCoroutine("StartAttackCD");
				stateMachine.ChangeState(fleye.chargeState);
			} else if (projectileReady) {
				fleye.StartCoroutine("StartAttackCD");
				stateMachine.ChangeState(fleye.shootState);
			}
		}
		base.LogicUpdate();
	}

	public override void PhysicsUpdate() {
		base.PhysicsUpdate();
	}
}
