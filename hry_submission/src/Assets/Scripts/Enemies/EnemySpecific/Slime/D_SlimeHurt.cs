using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newSlimeChargeData", menuName = "Data/Slime State Data/Hurt State")]
public class D_SlimeHurt : ScriptableObject {
	public float staggerSpeed = .2f;
	public float staggerTime = 1f;
}
