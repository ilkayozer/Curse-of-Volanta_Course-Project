using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Slider slider;

    public void SetMaxHealth(int fullHealth)
    {
        slider.maxValue = fullHealth;
        slider.value = fullHealth;
    }

    public void SetHealth(int health)
    {
        slider.value = health;
    }

    /*
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    */
}
