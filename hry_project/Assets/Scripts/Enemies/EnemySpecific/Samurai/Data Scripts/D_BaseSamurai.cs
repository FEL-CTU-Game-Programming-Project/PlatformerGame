using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newBaseSamurai", menuName = "Data/Entity Data/Samurai Data/Base Samurai")]
public class D_BaseSamurai : ScriptableObject {
    public float swordDamage = 30f;
    public float sliceDamage = 20f;
    public float attackCD = 3f;
    public float maxDashDist = 15f;
    public float dashSpeed = 5f;
    public float movementSpeed = 1f;

    public Vector3 castOffset = new Vector3(-0.1f, -0.2f, 0);
}
