using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cage : MonoBehaviour
{//This script manages cages in the second puzzle
    public Collider2D door;
    public string catcher;
    public bool triggered = false;
    public Animator animator;
    // Using a 2D collider and an animator, it "shuts" the cage door when chosen tag collides with collider inside the cage
    void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == catcher)
        {
            if (triggered == false)
            {
                Debug.Log("Cage triggered");
                door.isTrigger = false;
                triggered = true;
                animator.SetBool("Catched", true);
                FindObjectOfType<Cave>().monsterCatched();
            }
        }
    }
}
