using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newEntityData", menuName = "Data/State Data/Fleye Data/Hit Data")]
public class D_FleyeHit : ScriptableObject{
    public float knockbackSpeed = .5f;
    public float staggerTime = 2f;
}
