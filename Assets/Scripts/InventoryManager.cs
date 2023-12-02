using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour // Inventory Manager! Manages inventory of the player. Up to 3 items.
{
    // Instance  of inventory.
    public static InventoryManager instance; // Used by Player Controller mainly.
    
    private List<Collectables> collectedItems = new List<Collectables>(); // Stores collected items in a List, so it can increase modually. typo
    private int maxInventorySize = 3; // Size of available inventory.

    void Awake()
    {
        if (instance == null){
            instance = this;
        } else{
            Debug.LogError("Duplicate UIManager!)");
            Destroy(gameObject);
        }
    }

    public bool canCollectItems() // Checks if an item can be collected. - Used by PlayerController.
    {
        return collectedItems.Count < maxInventorySize;
    }

    public void AddItem(Collectables collectables) //. . . Adds an item from Collectables 'collectables'. - Used by Player Controller.
    {
       
        if (canCollectItems()) // If an item CAN be collected in the first place.
        {
            collectedItems.Add(collectables); // collectedItems list adds the collectables. 

            collectables.CollectItem(); //  Calls collectables to Collect the item, now that it is added to inventory.
            
        }

        // Player Controller Test, may delete later. Testing too see if slowing players down based on how much they are holding is viable or not. or even fun for that matter.
        if (PlayerController.instance != null) // could be challenging???
        {
            PlayerController.instance.ModifyPlayerSpeed(-0.83f);
        } else{
            Debug.LogError("Player Controller Instance is null!");
        }
        
    }

    public void UnloadInventory() // Unloads collectedItems, cashes in points to score as well!! || Used by PlayerController.
    {
        collectedItems.Clear();
        GameManager.instance.ResetPoints(); // Resets Points.
        PlayerController.instance.ModifyPlayerSpeed(+0.2f); // May delete later, genuiely don't know if this is a good idea.
      
    }



}
