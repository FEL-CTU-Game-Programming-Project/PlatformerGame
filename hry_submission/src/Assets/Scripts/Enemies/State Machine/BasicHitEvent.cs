using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicHitEvent : MonoBehaviour
{
	/*
	 * Simple class that is put on AliveGO to check for hitboxes of AI
	 * Doesn't differentiate between multiple HitBoxes
	 */
	private Entity entity;
	private void Start() {
		entity = transform.parent.GetComponent<Entity>();
	}
	private void OnTriggerEnter2D(Collider2D collision) {
		entity.HitBoxTrigger(collision);
	}
}
