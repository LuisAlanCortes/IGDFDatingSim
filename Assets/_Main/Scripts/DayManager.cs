using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayManager : MonoBehaviour
{
    public static DayManager instance;
    public int DayCounter;

    // Start is called before the first frame update
    void Awake()
    {
        if (instance != null)
        {
            Destroy(this);
            return;
        }
        DontDestroyOnLoad(this);
        instance = this;
        AdvanceDay();
    }

    public void AdvanceDay()
    {
        DayCounter++;
        Ink.Runtime.Object obj = new Ink.Runtime.IntValue(DayCounter);
        // call the DialogueManager to set the variable in the globals dictionary
        DialogueManager.GetInstance()?.SetVariableState("dayCounter", obj);
    }
}
