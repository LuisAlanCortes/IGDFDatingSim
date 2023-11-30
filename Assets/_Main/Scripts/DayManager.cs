using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        // convert the variable into a Ink.Runtime.Object value
        int dayCounter = 1;
        Ink.Runtime.Object obj = new Ink.Runtime.IntValue(dayCounter);
        // call the DialogueManager to set the variable in the globals dictionary
        DialogueManager.GetInstance().SetVariableState("dayCounter", obj);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
