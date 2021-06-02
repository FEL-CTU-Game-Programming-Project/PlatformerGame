using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FleyeChargeState : State {
	/*
	 * Flies towards target on parabolic curve
	 * Didn't know better way to do so than by using power of basic math 
	 */
	private Fleye fleye;
	private D_FleyeCharge stateData;

	private float param;
	private Vector3 peak;

	public FleyeChargeState(FiniteStateMachine stateMachine, Fleye entity, string animBoolName, D_FleyeCharge stateData) : base(stateMachine, entity, animBoolName) {
		fleye = entity;
		this.stateData = stateData;
	}

	public override void DoChecks() {
		base.DoChecks();
	}

	/*
	 * Initialize speed and acceleration to enable maximum zoom
	 * Calculates parameter "a" in (y = a * x^2) based on difference in position of target and Fleye
	 * Initial position of target acts as a peak of the curve
	 * Every point on curve is then calculated pretending that peak is in (0, 0)
	 */
	public override void Enter() {
		base.Enter();
		fleye.agent.speed = stateData.diveSpeed;
		fleye.agent.acceleration = stateData.acceleration;
		fleye.FaceTarget();

		peak = fleye.target.position;
		Vector3 pos = fleye.aliveGO.transform.position;
		
		float dx = Mathf.Abs(peak.x - pos.x);
		float dy = Mathf.Abs(peak.y - pos.y);
		param = dy / (dx * dx);
		fleye.agent.SetDestination(NextDest());
	}

	public override void Exit() {
		base.Exit();
		fleye.agent.ResetPath();
		fleye.agent.acceleration = fleye.fleyeBaseData.acceleration;
		fleye.agent.speed = fleye.fleyeBaseData.flightSpeed;
	}

	public override void LogicUpdate() {
		base.LogicUpdate();
		if ((fleye.aliveGO.transform.position - fleye.target.position).magnitude >= stateData.interruptDist) {
			fleye.stateMachine.ChangeState(fleye.aggroState);
		}
		if( Mathf.Abs(fleye.agent.destination.x - fleye.aliveGO.transform.position.x) < stateData.padding) {
			fleye.agent.SetDestination(NextDest());
		}
	}

	public override void PhysicsUpdate() {
		base.PhysicsUpdate();
	}

	/*
	 * Returns point based on current position of Fleye
	 * Points are calculated with position relative to peak
	 * Points are taken with a sampling given in stateData
	 */
	private Vector3 NextDest() {
		Vector3 alivePos = fleye.aliveGO.transform.position;
		float x = (alivePos.x - peak.x) + (fleye.facing * stateData.sampleSize);
		float y = param * x * x;
		return peak + new Vector3(x, y, 0);
	}

	public void AnimEnded() {
		fleye.stateMachine.ChangeState(fleye.aggroState);
	}
}
