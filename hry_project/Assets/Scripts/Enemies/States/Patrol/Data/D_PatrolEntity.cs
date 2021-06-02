using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newPatrolEntityData", menuName = "Data/Entity Data/Base Patrol Data")]
public class D_PatrolEntity : ScriptableObject
{
	public float wallCheckDistance = .1f;
	public float ledgeCheckDistance = .3f;
	public float maxAgroDistance = 5f;
	public float minAgroDistance = 3f;
	public float chargeCD = 1.5f;
}
