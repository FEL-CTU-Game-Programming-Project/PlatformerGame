using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Soundtrack : MonoBehaviour
{//script that prevents engine from destroying gameObject sondtrack
  public static Soundtrack instance;
    void Awake()
    {

        if (instance == null)
            instance = this;
        else
        {
            Destroy(gameObject);
            return;

        }
        DontDestroyOnLoad(gameObject);
    }

}
