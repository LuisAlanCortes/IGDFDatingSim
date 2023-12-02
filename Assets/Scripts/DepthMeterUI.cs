using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class DepthMeterUI : MonoBehaviour
{
    public Transform playerTransform; // Player's curent position.
    public float metersPerUnit = 1f;
    public TextMeshProUGUI depthMeterText;

    public int offset; 
    
    // Start is called before the first frame update
    void Start()
    {
        //depthMeterText.text = "Depth: " + depth.ToString();
        depthMeterText = GetComponentInChildren<TextMeshProUGUI>(); // muuuuch better than above.
    

    
    }

    // Update is called once per frame
    void Update()
    {
    
        UpdateDepthMeter(); // Updates.
    }
    void UpdateDepthMeter()
    {
    
        if (playerTransform != null && depthMeterText != null)
        {
        float depth = Mathf.Abs(playerTransform.position.y); // Depth is based on the player's current position absoulte.

        float depthInMeters = depth * metersPerUnit - offset; // to meters!! and offset as well.

        depthMeterText.text = "Depth: " + depthInMeters.ToString("F2")  + " meters"; // Changes depthMeter's text in real time. 
        }
    }
}
