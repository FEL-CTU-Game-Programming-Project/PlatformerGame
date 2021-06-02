using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slime_DeathState : State {
	/*
	 * Death state is entered when slime gets deaded
	 * UNFUNNY REMARK: Captain Obvious had entered the room
	 */
	private Slime slime;
	private D_SlimeDeath stateData;
	public Slime_DeathState(FiniteStateMachine stateMachine, Slime entity, string animBoolName, D_SlimeDeath stateData) : base(stateMachine, entity, animBoolName) {
		slime = entity;
		this.stateData = stateData;
	}

	public override void Enter() {
		base.Enter();
		entity.SetVelocity(Vector2.zero, 0);
	}
}
