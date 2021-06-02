using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slime : PatrolEntity {
	/*
	 * Basic enemy script. Only holds states and state machine
	 * Decision making is done inside of states themself
	 * Decision making is done inside of states themself
	 */
	public Slime_IdleState idleState { get; protected set; }
	public Slime_MoveState moveState { get; protected set; }
	public Slime_PlayerDetectedState playerDetectedState { get; protected set; }
	public Slime_ChargeState chargeState { get; protected set; }
	public Slime_ReturnState returnState { get; protected set; }
	public Slime_HurtState hurtState { get; protected set; }
	public Slime_DeathState deathState {get; protected set; }

	[SerializeField]
	private D_PatrolIdleState idleStateData;
	[SerializeField]
	private D_PatrolMoveState moveStateData;
	[SerializeField]
	private D_PatrolPlayerDetectedState playerDetectedStateData;
	[SerializeField]
	private D_PatrolChargeState chargeStateData;
	[SerializeField]
	private D_PatrolReturnState returnStateData;
	[SerializeField]
	private D_SlimeHurt hurtStateData;
	[SerializeField]
	private D_SlimeDeath deathData;


	public override void Start() {
		base.Start();
		moveState = new Slime_MoveState(stateMachine, this, "move", moveStateData);
		idleState = new Slime_IdleState(stateMachine, this, "idle", idleStateData);
		playerDetectedState = new Slime_PlayerDetectedState(stateMachine, this, "playerDetected", playerDetectedStateData);
		chargeState = new Slime_ChargeState(stateMachine, this, "charge", chargeStateData);
		returnState = new Slime_ReturnState(stateMachine, this, "return", returnStateData);
		hurtState = new Slime_HurtState(stateMachine, this, "hurt", hurtStateData);
		deathState = new Slime_DeathState(stateMachine, this, "death", deathData);
		stateMachine.Initialize(idleState);
	}

	protected override void Damage(AttackProperties attack) {
		base.Damage(attack);
		if(startStagger) {
			startStagger = false;
			stateMachine.ChangeState(hurtState);
			StartCoroutine("StaggerTimer");
		}
	}
	IEnumerator StaggerTimer() {
		yield return new WaitForSeconds(hurtStateData.staggerTime);
		stateMachine.ChangeState(idleState);
	}

	protected override void Death() {
		Debug.Log("Slime ded");
		stateMachine.ChangeState(deathState);
		Destroy(gameObject, 0.625f);
	}
}
