using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BombCountDisplay : MonoBehaviour
{
    public int bombCount = 5;
    public Text bombCountText;


    private void Update()
    {
        bombCountText.text = bombCount.ToString(); 
    }
}
