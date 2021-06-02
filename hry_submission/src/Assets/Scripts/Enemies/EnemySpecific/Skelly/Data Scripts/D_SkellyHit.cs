using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newSkellyIdle", menuName = "Data/State Data/Skelly Data/Skelly Hit")]
public class D_SkellyHit : ScriptableObject {
    public float staggerTime = 2f;
    public float staggerSpeed = .3f;
}
