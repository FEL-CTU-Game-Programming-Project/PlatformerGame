using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SamuraiHitBox : MonoBehaviour
{
    /*
     * Simple script for calling HitBoxTrigger from specific hitbox
     * (name included for different damage values)
     */
    private Samurai samurai;
    void Start() {
        samurai = transform.parent.parent.GetComponent<Samurai>();
    }

	private void OnTriggerEnter2D(Collider2D collision) {
        samurai.HitBoxTrigger(gameObject.name, collision);
	}
}
