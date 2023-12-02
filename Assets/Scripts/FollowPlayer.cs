using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour // Camera follows the player. 
{
    public Transform playerTransform;
    public Vector3 offset = new Vector3(0f, 0f, -10f);
   

    // Update is called once per frame
    void Update()
    {
        if (playerTransform != null)
        {
            transform.position = playerTransform.position + offset;
        }
    }
}
