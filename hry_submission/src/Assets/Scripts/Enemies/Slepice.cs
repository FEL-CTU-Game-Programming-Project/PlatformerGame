using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slepice : MonoBehaviour
{//chicken isn't a real enemy, it has a collider and when player triggers it, the chicken will run away 
    public Animator animator;
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            animator.SetBool("Scared", true);
        }
    }
    }
