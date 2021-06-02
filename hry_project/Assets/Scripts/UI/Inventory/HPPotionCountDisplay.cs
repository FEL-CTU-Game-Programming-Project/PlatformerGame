using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HPPotionCountDisplay : MonoBehaviour
{
    public int potionCount = 2;
    public Text potionCountText;

    private void Update()
    {
        potionCountText.text = potionCount.ToString();
    }
}
