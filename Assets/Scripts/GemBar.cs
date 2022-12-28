using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GemBar : MonoBehaviour
{
    public Slider slider;

    public void SetMinGem(int minGem, int maxGem)
    {
        slider.maxValue = maxGem;
        slider.value = minGem;
    }

    public void SetGem(int gem)
    {
        slider.value = gem;
    }
    
}
