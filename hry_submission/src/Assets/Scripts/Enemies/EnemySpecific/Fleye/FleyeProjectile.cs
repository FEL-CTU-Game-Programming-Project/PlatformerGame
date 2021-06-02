using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FleyeProjectile : MonoBehaviour
{
    /*
     * Simple projectile controller
     */
    public Rigidbody2D rb;
    private Animator ac;
    private Vector3 initialPos;
    private AttackProperties ap;

    public float damage = 10f;
    public float travelDist = 10f;
    public float travelSpeed = .5f;
    public LayerMask player;
    public LayerMask whatDestroys;


    void Start() {
        ap = new AttackProperties(damage);
        ac = transform.GetComponent<Animator>();
        rb = transform.GetComponent<Rigidbody2D>();
        initialPos = transform.position;
    }

    /*
     * Sets flying direction of the projectile.
     * Direction isn't normalized so the further away target is, the faster it will be
     * Intended?
     * Not at all.
     * Will I fix it?
     * No, I don't think I will.
     */
	public void SetDirection(Transform target) {
        initialPos = transform.position;
        Vector2 direction = (Vector2)(target.position - initialPos);
        rb.velocity = direction * travelSpeed;
	}

    // Makes projectile go boom if it travels too far
	void Update() {
        if((initialPos - transform.position).magnitude > travelDist) {
            ac.SetBool("hit", true);
            rb.velocity = new Vector2(0, 0);
        }
    }

    /*
     * Sends damage to player and changes animation to explode
     * Projectile is destroyed if it hits any layer in whatDestroys layer mask
     */
	private void OnTriggerEnter2D(Collider2D collision) {
		if( player == (player | (1 << collision.gameObject.layer) ) ) {
            collision.SendMessage("Damage", ap);
		}
        if (whatDestroys == (whatDestroys | (1 << collision.gameObject.layer))) {
            rb.velocity = new Vector2(0, 0);
            ac.SetBool("hit", true);
        }
	}

    // Finished animation, rip projectile
    public void HitEnd() {
        Destroy(gameObject);
	}
}
