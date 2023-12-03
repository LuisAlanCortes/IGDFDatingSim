using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
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
        StoredSkills = new int[Enum.GetNames(typeof(Skill)).Length];
        Minigames = new bool[StoredSkills.Length];
    }

    public void ConfirmValues()
    {
        for (int i = 0; i < StoredSkills.Length; i++)
        {
            AddToPoints((Skill)i, 0);
        }

        Ink.Runtime.Object obj = new Ink.Runtime.IntValue(DayCounter);
        // call the DialogueManager to set the variable in the globals dictionary
        DialogueManager.GetInstance()?.SetVariableState("dayCounter", obj);
    }

    public void AdvanceDay()
    {
        DayCounter++;
        Ink.Runtime.Object obj = new Ink.Runtime.IntValue(DayCounter);
        // call the DialogueManager to set the variable in the globals dictionary
        DialogueManager.GetInstance()?.SetVariableState("dayCounter", obj);
        Minigames = new bool[StoredSkills.Length];
    }

    public enum Skill : int
    {
        STRENGTH,
        CHARISMA,
        INTELLIGENCE,
        WEALTHINESS,
        GUTS,
        PERCEPTION
    }

    public int[] StoredSkills;
    public bool[] Minigames;
    public void AddToPoints(Skill skill, int pt)
    {
        int index = (int)skill;

        int total = StoredSkills[index] + pt;
        StoredSkills[index] = total;
        Minigames[index] = true;

        Ink.Runtime.Object obj = new Ink.Runtime.IntValue(total);
        DialogueManager.GetInstance()?.SetVariableState(skill.ToString(), obj);
    }
}
