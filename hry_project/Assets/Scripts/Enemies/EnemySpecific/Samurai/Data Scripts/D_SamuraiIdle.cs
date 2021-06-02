using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newSamuraiIdleData", menuName = "Data/State Data/Samurai Data/Idle Data")]
public class D_SamuraiIdle : ScriptableObject {
	public float minWaitTime = 1f;
	public float maxWaitTime = 3f;
}
