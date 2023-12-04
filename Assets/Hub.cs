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
    [SerializeField] Button PierButton;
    [SerializeField] int Points;
    [SerializeField] TextAsset OrcaText;
    [SerializeField] TextAsset JellyfishText;
    [SerializeField] TextAsset AnglerText;

    private void Start()
    {
        
        dayCount.text = $"DAY {DayManager.instance.DayCounter} / 7";

        if (DayManager.instance.DayCounter == 7)
        {
            SceneManager.LoadScene("Game Over");
        }

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
            PierButton.interactable = true;
        } else
        {
            for (int i = 0; i < DayManager.instance.Minigames.Length; i++)
            {
                minigameButtons[i].interactable = !DayManager.instance.Minigames[i];
            }
            PierButton.interactable = false;
        }

        pointCount.text = $"POINTS LEFT: {Points}";
    }

    public void LoadScene(string scene)
    {
        SceneManager.LoadScene(scene);
    }

    public void Jellyfish()
    {
        DialogueManager.LoadSceneWithDialogue(JellyfishText);
    }
    public void Orca()
    {
        DialogueManager.LoadSceneWithDialogue(OrcaText);
    }
    public void Angler()
    {
        DialogueManager.LoadSceneWithDialogue(AnglerText);
    }
}
