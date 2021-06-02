using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newEntityData", menuName = "Data/State Data/Fleye Data/Death Data")]
public class D_FleyeDeath : ScriptableObject{
    public float knockbackSpeed = 5f;
    public Color deathColour = Color.gray;
}
