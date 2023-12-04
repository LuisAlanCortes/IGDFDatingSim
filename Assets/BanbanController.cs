using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BanbanController : MonoBehaviour
{
    public float speed;
    public Vector2 bounds;
    public float coinCooldown;
    public float detectRadius;
    public GameObject coinPrefab;
    public LayerMask detectMask;
    private void Start()
    {
        bounds /= 2f;
    }

    float cooldown;
    private void Update()
    {
        Vector3 move = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0f);

        Vector3 pos = transform.position; pos += move * speed * Time.deltaTime;
        if (pos.y > bounds.y)
            pos.y = bounds.y;
        if (pos.y < -bounds.y)
            pos.y = -bounds.y;
        if (pos.x > bounds.x)
            pos.x = bounds.x;
        if (pos.x < -bounds.x)
            pos.x = -bounds.x;
        transform.position = pos;

        Collider2D agent = Physics2D.OverlapCircle(transform.position, detectRadius, detectMask, -999999f, 999999f);
        if (cooldown <= 0f && agent != null)
        {
            cooldown = coinCooldown;
            Coin coin = GameObject.Instantiate(coinPrefab).GetComponent<Coin>();
            coin.forward = (agent.transform.position - transform.position).normalized;
            coin.transform.position = transform.position;
        }
        if (cooldown > 0f)
            cooldown -= Time.deltaTime;
    }

    private void OnDrawGizmos()
    {
        //Gizmos.DrawCube(Vector3.zero, bounds);
        Gizmos.DrawWireSphere(transform.position, detectRadius);
    }
}
