using Ink.Parsed;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    public Vector3 forward;
    public float speed;

    private void Update()
    {
        Vector3 pos = transform.position;
        pos += speed * forward * Time.deltaTime;
        transform.position = pos;

        if (Vector3.Distance(pos, Vector3.zero) > 30f)
           Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<Agent>(out Agent a))
        {
            Destroy(a.gameObject);
            Destroy(gameObject);
        }

    }
}
