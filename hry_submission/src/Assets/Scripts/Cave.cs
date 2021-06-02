using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cave : MonoBehaviour
{//second part of the puzzle in the second level
    public int cagesLeft = 3;
    public Collider2D door;
    public Animator animator;

    //this function is called by cages, when all cages are triggered it opens a secret door
    public void monsterCatched()
    {
        cagesLeft--;
        Debug.Log("Cage triggered");
        if (cagesLeft < 1)
        {
            Debug.Log("Door openned");
            door.isTrigger = true;
            animator.SetBool("MonstersCatched", true);
        }
    }
}
