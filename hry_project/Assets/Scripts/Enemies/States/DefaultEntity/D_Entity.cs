using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newEntityData", menuName = "Data/Entity Data/Base Data")]
public class D_Entity : ScriptableObject {
	public float maxHealth = 100f;
	public float touchDamage = 10f;

	public float hitBoxWidth = 5f;
	public float hitBoxHeight = 3f;

	public float hitCD = .3f;
	public float staggerCD = 5f;

	public LayerMask whatIsGround;
	public LayerMask whatIsPlayer;
}
