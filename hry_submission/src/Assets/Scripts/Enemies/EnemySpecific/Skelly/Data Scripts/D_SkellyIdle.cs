using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newSkellyIdle", menuName = "Data/State Data/Skelly Data/Skelly Idle")]
public class D_SkellyIdle : ScriptableObject {
    public float minWaitTime = 2f;
    public float maxWaitTime = 1f;
}
