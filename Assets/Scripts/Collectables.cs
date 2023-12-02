using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

[System.Serializable]
public class Collectables : MonoBehaviour
{
    [System.Serializable]

    public class Rarity // Controls how valuable each collectable is.
{
    public string type; 
    
    public Rarity(string type) // la Constructor.
    {
        this.type = type;
      
    }
}


    public Rarity rarity; // Not a class, but rather a .. forgot the word but allows access to the Rarity class.
    public int basePointValue = 10;

    public void CollectItem() // Used by the InventoryManager, first multiplies basePoints value by whatever rarity it is.
    {                         // Then adds the points to GameManager's (addpoints.)
        float rarityMultiplier = GetRarityMultiplier(rarity.type);
        int pointsToAdd = Mathf.RoundToInt(basePointValue * rarityMultiplier);
        GameManager.instance.AddPoints(pointsToAdd); // Calls GameManager to add Points.

        gameObject.SetActive(false);
    }

    float GetRarityMultiplier(string type) // Switch statements, controlled by the type manually inputted into Unity's inspector.
    {
        switch (type) 
        {
            case "Common":
                return 1.0f;
            case "Rare":
                return 5.0f;
            case "Legendary":
                return 10.0f;
        default:
            return 1.0f;
        }
    }
}
