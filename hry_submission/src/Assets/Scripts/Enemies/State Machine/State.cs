using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class State
{
	/*
	 * Class used for every state of AI
	 * Stores start time since it's used often for exiting states
	 * Also switches animations using a bool
	 */
	protected FiniteStateMachine stateMachine;
	protected Entity entity;

	protected float startTime;
	protected string animBoolName;

	public State(FiniteStateMachine stateMachine, Entity entity, string animBoolName) {
		this.stateMachine = stateMachine;
		this.entity = entity;
		this.animBoolName = animBoolName;
	}

	public virtual void Enter() {
		startTime = Time.time;
		entity.animator.SetBool(animBoolName, true);
		DoChecks();
	}

	public virtual void Exit() {
		entity.animator.SetBool(animBoolName, false);
	}

	public virtual void LogicUpdate() {
	}

	public virtual void PhysicsUpdate() {
		DoChecks();
	}

	public virtual void DoChecks() {

	}
}
