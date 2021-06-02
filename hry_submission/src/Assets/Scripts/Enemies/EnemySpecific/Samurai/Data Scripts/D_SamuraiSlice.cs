using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newSamuraiShuffleData", menuName = "Data/State Data/Samurai Data/Slice Data")]
public class D_SamuraiSlice : ScriptableObject
{
    public float warningDelay = 1.1f;
    public float attackDelay = 1.8f;
    public float exitDelay = 2f;
    public float diameter = .2f;
    public float distance = 20f;
}
