using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Hub : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI dayCount;
    private void Start()
    {
        dayCount.text = $"DAY {DayManager.instance.DayCounter} / 5";
    }

    public enum Minigame { 
        Guts,
        Strength,
        Charisma,
        Perception,
        Intelligence,
        Wealthiness
    }

    public void SelectMinigame(Minigame m)
    {

    }

    public void GoToPier()
    {

    }
}
