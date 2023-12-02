using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour // Handles the player controller, and their state.
{
    // References to other scripts.
    public OxygenManager oxygenManager; // Manages Oxygen of player.
    public Image breathMeterImage; // UI Oxygen
     public GameObject oceanSurface; // Returns surface of the ocean.
     public static PlayerController instance; // Used by InventoryManager.
     public TimeController timer;
     public GameManager gameManager;

     // Player stats.
    public float jumpForce = 10f; // Initial Jump
    public float swimSpeed = 5f; 
    public float detectionDistant = 2f; // Adjust detection of interactions.
    // Oxygen Stats.
    public float maxBreathTime;
    private Vector2 spawnPoint;
    private float currentBreathTime;
    public float breathRegainRate;
    public float oceanLevel = -86f;
   // Booleans
    private bool isOnLand = true;
    private bool passed = false; // bug check
    private bool isInOcean = false;
    private bool isInsideDock = false;

    private float checker = -87f; // Used in checking if player passed -Y axis of -95.
    private Rigidbody2D rb;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        } else {
            Debug.LogError("Duplicate PlayerController Instance.");
            Destroy(gameObject);
        }

        timer = FindObjectOfType<TimeController>();
    }

   
    void Start()
    {
        rb = GetComponent<Rigidbody2D>(); // Rigidbody
        currentBreathTime = maxBreathTime;
        spawnPoint = transform.position;
    }


   
    void Update()
    {
        // Starts the game
        if (isOnLand && Input.GetKeyDown(KeyCode.F)){ // If on land and the player pressed the 'F' key, call JumpIntoOcean() Method.
            // Reason it's in Update is to be called once at start of game.
            JumpIntoOcean();

            timer.StartTimer(); // Starts the timer! note it's a different script that holds timer functionality
        }

        if (Input.GetKeyDown(KeyCode.E)) // If Input is E, try collecting an item.
        {
            TryCollectItem();
        }
        if (Input.GetKeyDown(KeyCode.F) && isInsideDock)
        {
            gameManager.ShowWinScreen();
            transform.position = spawnPoint;
        }
        updateBreath(); // Breath gradually decreases when in the ocean, therefore update it constantly. 
        UpdateBreathMeterUI(); // Correlates with breath.

    
    }
    
    

    void FixedUpdate()
    {
        if (!isOnLand && isInOcean) // Only move once actually in the ocean. Fixed!
        { 
            float horizontalInput = Input.GetAxis("Horizontal"); // basic horizontalInput/vertical Input.
            float verticalInput = Input.GetAxis("Vertical");
            //                                             x                        y           
            Vector2 movement = new Vector2(horizontalInput * swimSpeed, verticalInput * swimSpeed);
            rb.velocity = movement;

            
            // So the player stays at a certain water point. (Unironically, the most stressful part of this entire thing)
            Vector2 newPosition = transform.position;
            if(transform.position.y < checker) // If player's Y position pasts the check.
            {
                passed = true;
            }

            if (passed == true && transform.position.y > oceanLevel) //If it passed it, and the player is above the oceanLevel, THEN
            {
                newPosition.y = Mathf.Clamp(newPosition.y , float.MinValue, oceanLevel); // CLAMP THEM. WORKS!! WOOOO no longer flies above
                transform.position = newPosition;
            }
           
           
        
        }
    }



    private void JumpIntoOcean(){ // When called - 
        isOnLand = false; // No longer on land, therefore false.
        rb.gravityScale = 1f; //        x                y    (personal note).
        rb.velocity = new Vector2(rb.velocity.x + 5, jumpForce); // Moves the player forwards on the X position at jumpforce Y. a jump pretty much

    }

    private void OnTriggerEnter2D(Collider2D other) // For the ocean.
    {
    
        if (other.CompareTag("Ocean")) // When the player collides and enters an object with the tag 'Ocean'
        { 
           isInOcean = true; // Ocean now true.
        rb.gravityScale = 3f; // So you don't fall straight to the bottom of the ocean lol.jy 
        }

        if (other.CompareTag("Bubbles")) // When player combines with a tag named 'Bubbles'.
        {
            currentBreathTime = maxBreathTime; // Regains max breath.
            Destroy(other.gameObject);
        }
        if (other.CompareTag("UnloadSpot"))
        {
            isInsideDock = true;
        }
        
        
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("UnloadSpot"))
        {{
            isInsideDock = false;
        }}
    }

    private bool isUnderwater()
    {
        return transform.position.y < oceanLevel; // Checks too see if player is underwater. Different from the other bool, isInOcean. (trust)
    }

    private void RegainBreath() // When called, the player is no longer in the ocean. Regain their breath.
    {
        if (currentBreathTime < maxBreathTime)
        {
            currentBreathTime += breathRegainRate * Time.deltaTime;
        }
    }

    private void updateBreath() // Called in Update. Main script for breathing.
    {
         if (isUnderwater()) // Self explanatory.
        {
            currentBreathTime -= Time.deltaTime; // current Breath is decreased to Time.deltaTime.
        }
        else
        {
            RegainBreath(); // self explanatory
            
        }

        if (currentBreathTime < 0)
        {
            GameManager.instance.GameOver("Drowned!"); //Calls gameManager to initiate a Game Over : helloo, how r u,  im under the water, please help me. theres too much raining , huaaooo
        }
    }

    private void UpdateBreathMeterUI()
    {
        oxygenManager.UpdateBreathMeterUI(currentBreathTime, maxBreathTime); //OxygenManager, updates breathMeter UI.
    }



    private void TryCollectItem() // Try to collect an item when E is pressed.
    { 
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, detectionDistant); // Colliders...

        foreach(Collider2D collider in colliders) // like a for loop, but for each.
        {
            if (collider.CompareTag("Collectables")) // If tag on 'E', is a collectable
            {
                Collectables collectable = collider.GetComponent<Collectables>(); // the tie between player controller and collectable.

                if (collectable != null && InventoryManager.instance.canCollectItems()) // And the Inventory isn't filled.
                {
                     InventoryManager.instance.AddItem(collectable); // Calls the InventoryManager, using the AddItem function, with parameter of the item just collected.
                }
                 else
                 {
                    
                    print("Collected!");
                 }
            }
            else if (collider.CompareTag("UnloadSpot")) // If tag on 'E' is the spot to unload.
            {
                print("Testing unload moment.");
                InventoryManager.instance.UnloadInventory(); // Calls the Inventory Manager again, using the UnloadInventory function.

            }
        }
    }



    



            // Called by the InventoryManager. May delete.
    public void ModifyPlayerSpeed(float speedModifier) // hmm.. probably?? was gonna slow down players if they collect too much treasure for added difficulty. but....
    {
        rb.velocity = new Vector2(rb.velocity.x * (1 + speedModifier), rb.velocity.y);
    }


    
}
