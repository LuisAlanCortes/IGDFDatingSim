
using System;
using UnityEngine;

[CreateAssetMenu(fileName = "Character Set.asset", menuName ="Character Set", order = 0)]
public class CharacterSet : ScriptableObject
{
    public string id;
    public CharacterSprite[] sprites;
    public CharacterVox[] voices;

    [Serializable]
    public struct CharacterVox
    {
        public string id;
        public AudioClip asset;
    }

    [Serializable]
    public struct CharacterSprite
    {
        public string id;
        public Sprite asset;
    }
}
