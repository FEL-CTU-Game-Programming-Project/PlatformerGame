using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newSamuraiShuffleData", menuName = "Data/State Data/Samurai Data/Shuffle Data")]
public class D_SamuraiShuffle : ScriptableObject {
    public float minDist = 1f;
    public float maxDist = 5f;
    public float bodyOffset = .25f;
}
