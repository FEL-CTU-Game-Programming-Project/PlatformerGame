using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyManager : MonoBehaviour
{
    int piecesLeft = 3;

    public Key pieceOne;
    public Key pieceTwo;
    public Key pieceThree;
    public Animator animator;
    public Collider2D door;
    //This script manages key parts, after 3 key parts are collected it opens door to the nexl level 
    
    public void keyCollected()
    {
        piecesLeft--;
        Debug.Log("Door triggered");
        if (piecesLeft<1)
        {
            Debug.Log("Door openned");
            door.isTrigger = true;
            animator.SetBool("KeyCollected", true);
        }
    }

   
}
