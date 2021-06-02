using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newBaseSkelly", menuName = "Data/Entity Data/Skelly Data/Base Skelly")]
public class D_SkellyBase : ScriptableObject{
    public float attackCD = 3f;
    public float moveSpeed = 1f;
    public float detectionRange = 3f;
    public float maxAggroRange = 5f;
    public float wallCheckDist = .1f;
    public float ledgeCheckDist = .05f;
}
