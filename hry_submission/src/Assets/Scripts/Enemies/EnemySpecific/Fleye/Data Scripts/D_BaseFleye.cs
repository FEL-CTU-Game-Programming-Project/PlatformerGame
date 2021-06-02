using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newEntityData", menuName = "Data/Entity Data/Fleye Data/Base Data")]
public class D_BaseFleye : ScriptableObject {
	public float flightSpeed = 1.5f;
	public float acceleration = 2f;

	public float projectileDamage = 20f;
	public float attackCD = 2f;

	public float detectionDist = 10f;
	public float maxDist = 20f;

	public float offsetX = 2f;
	public float offsetY = 1f;
}
