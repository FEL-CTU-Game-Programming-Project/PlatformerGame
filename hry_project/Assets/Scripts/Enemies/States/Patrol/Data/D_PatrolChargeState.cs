using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newPatrolChargeData", menuName = "Data/State Data/Charge State")]
public class D_PatrolChargeState : ScriptableObject {
	public float chargeSpeed = 1.5f;
	public float chargeTime = 1f; // Set to animation time of the attack
	public float endLag = .5f; // Time to wait after charging
}
