using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{   //This script uses 2D collider to trigger Dialogue
    public Dialogue dialogue;
    public bool triggered = false;

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (triggered == false)
            {
                Debug.Log("Dialogue triggered");
                FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
                triggered = true;
            }
        }
    }

}
