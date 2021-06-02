using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Slider slider;
    public Gradient gradient;
    public Image fill;

    public void SetMaxHealth(float health)
    {
        Debug.Log("Max Value" + health);
        slider.maxValue = health;
        slider.value = health;
    }

    public void SetHealth(float health)
    {
        Debug.Log("Value" + health);
        slider.value = health;
    }
}
