using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ArrowCountDisplay : MonoBehaviour
{
    public int arrowCount = 10;
    public Text arrowCountText;

    private void Update()
    {
        arrowCountText.text = arrowCount.ToString();
    }
}
