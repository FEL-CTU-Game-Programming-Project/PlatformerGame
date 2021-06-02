using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class Sentence
{   //this class is used by the Dialogue class to form dialogues
    public string name;

    public Sprite characterSprite;
    //For each sentence in your dialogues you can add pictures and names for the ones having them

    [TextArea(3, 10)]
    public string sentenceText;

}
