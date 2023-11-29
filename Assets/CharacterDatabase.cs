using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterDatabase : MonoBehaviour
{
    private CharacterSet curChar;
    [SerializeField] CharacterSet[] characters;
    private Dictionary<string, CharacterSet> idToChar;
    public void Awake()
    {
        ConstructDictionary();
    }

    public bool SetCurrentChar(string id)
    {
        CharacterSet set = GetCharacter(id);
        bool cond = set != null;

        if (cond)
            curChar = set;

        return cond;
    }

    public Sprite GetSprite(string id)
    {
        if (curChar == null)
            return null;

        foreach (CharacterSet.CharacterSprite chspr in curChar.sprites)
        {
            if (chspr.id == id)
                return chspr.asset;
        }

        return null;
    }

    public CharacterSet GetCharacter(string id)
    {
        if (idToChar.TryGetValue(id, out CharacterSet set))
        {
            return set;
        }
        return null;
    }

    private void ConstructDictionary()
    {
        idToChar = new Dictionary<string, CharacterSet>();
        foreach (CharacterSet set in characters)
        {
            idToChar.Add(set.id, set);
        }
    }
}
