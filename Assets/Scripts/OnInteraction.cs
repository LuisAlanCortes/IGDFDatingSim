using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.UI;

public class OnInteraction : MonoBehaviour
{
    public CardLogicManager cardLogicManager;

    private static bool playerWon = false;

 
   void OnMouseDown()
   {
    if (isTargetCard())
    {
        playerWon = true;
    } else
    {
        playerWon = false;
        
    }

    cardLogicManager.swapSides();
   }
   public bool CheckIfWon()
   {
    print(playerWon);
    return playerWon;
   }

   private bool isTargetCard()
   {
    return cardLogicManager.returnTargetCard(gameObject);
   }

}
