using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newSkellyIdle", menuName = "Data/State Data/Skelly Data/Skelly Walk")]
public class D_SkellyWalk : ScriptableObject {
	public float maxWalkDist = 5f;
	public float minWalkDist = .5f;
}
