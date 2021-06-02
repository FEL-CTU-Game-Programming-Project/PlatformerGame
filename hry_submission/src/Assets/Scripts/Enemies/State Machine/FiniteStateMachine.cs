using System.Collections;
using System.Collections.Generic;
using System.Resources;
//using UnityEditorInternal;
using UnityEngine;

public class FiniteStateMachine {
	public State currentState { get; private set; }

	public void Initialize(State startingState) {
		currentState = startingState;
		currentState.Enter();
	}

	public void ChangeState(State newState) {
		if (newState.Equals(currentState)) {
			Debug.Log("Changed to state that it's currently in");
			return;
		}
		currentState.Exit();
		currentState = newState;
		currentState.Enter();
	}
}
