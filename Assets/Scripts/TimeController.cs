using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TimeController : MonoBehaviour // The clock! Main antagonist of the game, and me.
{
    // Reference to other scripts.
    public static GameManager instance;
    // Text fields.
    public TextMeshProUGUI countDownText;
    private bool isRunning = false;
    // Floats.
    public float totalTime = 180f;
    private float currentTime;
    // Update is called once per frame
    void Update()
    {
        if (isRunning && totalTime > 0)
        {
            totalTime -= Time.deltaTime; // Lets time casually tick down.
            
            UpdateTime();

        } else if (totalTime <= 0)
        {
            GameManager.instance.GameOver("Time is up!"); // However, could game over if ran out. -- Calls gameManager.
        }

    }

    private void UpdateTime() // i hate this.
    {
        int minutes = Mathf.FloorToInt(totalTime/ 60);
        int seconds = Mathf.FloorToInt(totalTime % 60);

        countDownText.text = string.Format("Time: {0:00}:{1:00}", minutes, seconds); 
        /* Okay. Sstring.Format is basically the 'Format' of the time you want, with the minutes and seconds being
        coming after 'Format'. Should be simple enough, 10 minutes bet.

        why did i say that
        */
    }

    public void StartTimer() // Used by Player Controller.
    {
        print("Starting.");
        currentTime = totalTime;
        isRunning = true;
    }
    public void StopTimer(){
        isRunning = false;
    }
    }

