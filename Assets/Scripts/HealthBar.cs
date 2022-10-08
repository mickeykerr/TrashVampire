using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Slider healthbar;
    public float health = 10;

    private void Update()
    {
        healthbar.value = health;
    }
}
