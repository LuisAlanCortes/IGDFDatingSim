using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sandbag : MonoBehaviour
{
    public float hitShakeTime;
    public float hitShakeMagnitude;
    public float torqueToForce = 30f;
    public float landShake = 30f;
    public Animator anim;
    public Rigidbody2D rb;
    public ParticleSystem impact;
    public ParticleSystem trail;

    public void SetShake(float shake)
    {
        this.shake = shake;
        anim.transform.localPosition = Vector3.zero;
    }
    public void Hit(float mag)
    {
        anim.Play("SandbagHurt");
        shake += (hitShakeTime * mag) / (shake + 1f);
        magModifier = mag;
    }

    public void Launch(float punchForce)
    {
        rb.AddForce(Vector2.Lerp(Vector2.right, Vector2.up, 0.5f) * punchForce);
        rb.AddTorque(punchForce / torqueToForce);
        impact.Play();
        trail.Play();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.relativeVelocity.magnitude > 15f)
            Hit(collision.relativeVelocity.magnitude / landShake);
    }

    private float shake;
    private float magModifier;
    private void Update()
    {
        if (shake > 0f)
        {
            shake -= Time.deltaTime;
            anim.transform.localPosition = Random.insideUnitCircle * (shake / hitShakeTime) * hitShakeMagnitude * magModifier;
        }
    }
}
