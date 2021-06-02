using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newEntityData", menuName = "Data/State Data/Fleye Data/Charge Data")]
public class D_FleyeCharge : ScriptableObject {
	public float sampleSize = .5f;
	public float padding = .2f;
	public float diveSpeed = 2f;
	public float acceleration = 5f;
	public float interruptDist = 2f;
}
