using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowBehaviour : MonoBehaviour
{
    private float randomShakePower;
    public float speed = 4.5f; // speed of arrow
    private float arrowDamage = 30; // damage of arrow

    private AttackProperties bowAttack;

    private PlayerController pc;

    // at the beginning bowAttack and its properties are set up
    private void Start()
    {
        bowAttack = new AttackProperties(arrowDamage, AttackProperties.DamageType.Neutral, 0);
        pc = GetComponent<PlayerController>();
    }

    private void Update()
    {
        transform.position += transform.right * Time.deltaTime * speed; // this is how arrow goes and how fast arrow goes
        Destroy(gameObject, 1); // destroy in 1 second if there is not collision
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (LayerMask.NameToLayer("layerName") != LayerMask.NameToLayer("whatIsDamageable"))
        {
            return;
        }

        if(collision.tag == "Enemy")
        {
            Debug.Log("Posilam DMG");
            bowAttack.direction = transform.position.x;
            Destroy(gameObject);
            randomShakePower = Random.Range(-1f, 3f); // random Shakepower
            ScreenShakeController.Instance.ShakeCamera(randomShakePower, 0.1f); // creating camera Shake
            collision.transform.parent.SendMessage("Damage", bowAttack); // sending information concerning bowAttack
        }
        else // Destroy with collision even if it is not an enemy
        {
            Destroy(gameObject);
        }
    }
}
