using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombBehaviour : MonoBehaviour
{
    public float speed = 2.5f; // speed how fast is bomb

    private AttackProperties bombAttack;
    private float bombDamage = 20; // BOMB DAMAGE

    public GameObject Explosion;

    private float randomShakePower;

    public Vector3 LaunchOffset;
    public LayerMask whatIsDamageable;

    private void Start()
    {
        var direction = transform.right + Vector3.up;
        GetComponent<Rigidbody2D>().AddForce(direction * speed, ForceMode2D.Impulse);

        bombAttack = new AttackProperties(bombDamage, AttackProperties.DamageType.Neutral, 0);

        transform.Translate(LaunchOffset);

        Destroy(gameObject, 5); // bomb is destroyed after 5 seconds if there is not collision
    }

    // Sending damage to enemy (similar to default attack)
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (LayerMask.NameToLayer("layerName") != LayerMask.NameToLayer("whatIsDamageable"))
        {
            return;
        }

        if (collision.tag == "Enemy")
        {
            bombAttack.direction = transform.position.x;
            Destroy(gameObject); // destroys bomb
            Instantiate(Explosion, transform.position, Quaternion.identity); // creates explosion
            randomShakePower = Random.Range(-1f, 3f);
            ScreenShakeController.Instance.ShakeCamera(randomShakePower, 0.1f); // screenShake
            collision.transform.parent.SendMessage("Damage", bombAttack); // send information concerning bombAttack
        }
        else // if collision isnt enemy, explode anyways
        {
            Destroy(gameObject);
            Instantiate(Explosion, transform.position, Quaternion.identity);
        }
    }
}
