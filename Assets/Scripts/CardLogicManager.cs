using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardLogicManager : MonoBehaviour
{
    public OnInteraction uiControl;
    public GameObject targetCard;
    public CardController cardController;
    public GameObject failurePanel;
    public GameObject successPanel;
    public GameObject endingButtons;
    
    // Start is called before the first frame update
    void Start()
    {
        failurePanel.SetActive(false);
        successPanel.SetActive(false);
        endingButtons.SetActive(false);
    }

    void Update()
    {
    }

    public bool returnTargetCard(GameObject clickedCard)
    {
        return clickedCard == targetCard;
    }

    public void swapSides()
    {
        if (uiControl.CheckIfWon())
        {
            
            successPanel.SetActive(true);
        } else
        {
           
            failurePanel.SetActive(true);
        }
        cardController.RevertToOriginalSprite();
        endingButtons.SetActive(true);
    
   }
}
