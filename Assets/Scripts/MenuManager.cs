using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    
   public void StartGame()
   {
    SceneManager.LoadScene("GameScene");
   }

   public void QuitGame()
   {
    Application.Quit();
   }

   public void ReloadMenu()
   {
      SceneManager.LoadScene("MenuScene");
   }

}
