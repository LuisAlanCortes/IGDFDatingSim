using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public TextAsset IntroDialogue;

    public void LoadGame()
    {
        DialogueManager.LoadSceneWithDialogue(IntroDialogue);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
