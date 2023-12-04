using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;

public class MenuManager : MonoBehaviour
{
    
   public void StartGame()
   {
        SceneManager.LoadScene("GutsMinigame");
   }

   public void QuitGame()
   {
        //Application.Quit();
        DayManager.instance.AddToPoints(DayManager.Skill.GUTS, GameManager.instance.Score / 50);
        SceneManager.LoadScene("Hub");
    }

   public void ReloadMenu()
   {
      SceneManager.LoadScene("MenuScene");
   }

}
