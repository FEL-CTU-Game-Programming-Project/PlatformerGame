using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDummy : MonoBehaviour
{
    public void Damage(float[] attackDetails) {
		Debug.Log("OUCH MY ASS " + attackDetails[0]);
	}
}
