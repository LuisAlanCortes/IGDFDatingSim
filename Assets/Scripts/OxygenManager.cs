using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OxygenManager : MonoBehaviour // biggest struggle - Used by PlayerController.
{
    public Image breathMeterImage; // Image of the breath going down.

     public void UpdateBreathMeterUI(float currentBreath, float maxBreath) // Referenced by Player Controller to update breathMeterUi.
    {
        
        float fillAmount = Mathf.Clamp01(currentBreath/ maxBreath); // Fill Amount is the blue bar over the 'breathMeter'. It's clamped between 0 and 100.
        breathMeterImage.fillAmount = fillAmount; // Fill amount gradually decreases, therefore the bar decreases as well.
    }
}
