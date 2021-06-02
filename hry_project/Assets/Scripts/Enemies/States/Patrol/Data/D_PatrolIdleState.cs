using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newPatrolIdleData", menuName = "Data/State Data/Idle State")]
public class D_PatrolIdleState : ScriptableObject {
	public float minIdleTime = 1f;
	public float maxIdleTime = 3f;
}
