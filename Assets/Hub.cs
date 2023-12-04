using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Hub : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI dayCount;
    [SerializeField] TextMeshProUGUI pointCount;
    [SerializeField] Button[] minigameButtons;
    [SerializeField] int Points;

    private void Start()
    {
        
        dayCount.text = $"DAY {DayManager.instance.DayCounter} / 7";

        for (int i = 0; i < DayManager.instance.Minigames.Length; i++)
        {
            if (DayManager.instance.Minigames[i])
            {
                Points--;
            }
        }

        if (Points <= 0)
        {
            for (int i = 0; i < DayManager.instance.Minigames.Length; i++)
            {
                minigameButtons[i].interactable = false;
            }
        } else
        {
            for (int i = 0; i < DayManager.instance.Minigames.Length; i++)
            {
                minigameButtons[i].interactable = !DayManager.instance.Minigames[i];
            }
        }

        pointCount.text = $"POINTS LEFT: {Points}";
    }

    public void LoadScene(string scene)
    {
        SceneManager.LoadScene(scene);
    }

    public void GoToPier()
    {

    }
}
