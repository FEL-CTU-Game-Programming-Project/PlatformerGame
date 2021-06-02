using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class Dialogue
{//This is the basic structure of our Dialogue

    [Header("Sound")]
    public AudioClip sound;
   

    [Header("Character Image")]
    public Image imageHolder;
    
    
    public Sentence[] sentences;
}
