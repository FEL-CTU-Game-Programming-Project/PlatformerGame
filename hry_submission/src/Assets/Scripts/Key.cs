using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour
{//this script is for key parts in the first level
    public bool triggered = false;

    //it uses collider to send information to key manager that key piece was found
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (triggered == false)
            {
                Debug.Log("Key triggered");
                FindObjectOfType<KeyManager>().keyCollected();
                triggered = true;
            }
        }
    }
}
