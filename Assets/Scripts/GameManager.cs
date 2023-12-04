using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour // Handles mostly Points + game state.
{
    // References to other scripts.
    public static GameManager instance; // Used by Collectables, InventoryManager, Player Controller, and Time Controller.

    public PlayerController playerController;
    public TimeController timeController;

    // Text block
    public TextMeshProUGUI pointsText;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI carryLimitText;
    public TextMeshProUGUI pointsOnDeath;
    public GameObject gameOverScreen;
    public GameObject winScreen;
    
    // Integer
    private int currentlyHeld = 0;
    private int totalPoints = 0;
    private int score = 0;
    public int Score { get => score; }
    
    

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            
        }
        else{
            Debug.LogError("Duplicate GameManager instance!!! DESTROY IMMEDIANTLY RAAHH!!");
            Destroy(gameObject);
        }
    }

    void Start()
    {
        
        gameOverScreen.SetActive(false);
    }

    public void AddPoints(int pointsToAdd) // Used by Player Controller to add Points when an item is collected.
    {
        totalPoints += pointsToAdd;  
        currentlyHeld += 1;  
        UpdatePointUI(); 
        

    }


    void UpdatePointUI() // Casually updating Point and the Limit text.
    {
        if (pointsText != null)
        {
            pointsText.text = "Points: " + totalPoints;
        }
        carryLimitText.text = "Carrying: " + currentlyHeld +"/3";
        
    }


    public void ResetPoints() // Resets Points, and the number of items currently held.
    {
        score += totalPoints;
        totalPoints = 0;
        currentlyHeld = 0;

        if (scoreText != null && pointsText != null)
        {
            scoreText.text = "Score: " + score;
            pointsText.text = "Points: " + totalPoints.ToString();
            carryLimitText.text = "Carrying: " + 0 + "/3";
        }

    
    }

    private void Info()
    {
        pointsOnDeath.gameObject.SetActive(true);
        pointsOnDeath.text = $"GUTS POINTS EARNED: {Mathf.Clamp(GameManager.instance.Score / 30, 0, 3)}"; 
    }

    public void GameOver(string reason) // Used by Player Controller and Time Controller.
    {
        Info();
        Debug.Log("Game Over!");
        playerController.enabled = false; // Temporary disablement. 
        gameOverScreen.SetActive(true);
        timeController.StopTimer();

    }

    public void ShowWinScreen()
    {
        Info();
        Debug.Log("Docked!");
        playerController.enabled = false;
        winScreen.SetActive(true);
        timeController.StopTimer();
        print(score);
    }


}