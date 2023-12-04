using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Agent : MonoBehaviour
{
    public float speed;

    public void Update()
    {
        Vector3 pos = transform.position;
        pos = Vector3.MoveTowards(pos, Vector3.zero, speed * Time.deltaTime);
        transform.position = pos;
    }

    private void OnTriggerEnter(Collider other)
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "UnloadSpot")
            WealthManager.Instance.EndGame();
    }
}
