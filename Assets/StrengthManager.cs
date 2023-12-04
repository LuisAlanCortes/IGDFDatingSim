using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.Experimental.TerrainAPI;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.SceneManagement;

public class StrengthManager : MonoBehaviour
{
    public float backgroundParallax;
    public float prepTime;
    public Animator player;
    public ParticleSystem punch;
    public Transform floor;
    public Transform parallax;
    public Transform bg;
    public float punchForce;
    private float totalForce;
    public GameObject tut;
    public TextMeshPro countdown;
    public TextMeshPro timerui;
    public float followSpeed;
    public Sandbag bag;

    private Vector3 bgSize;

    private Transform camera;
    private void Start()
    {
        camera = Camera.main.transform;
        bgSize = bg.GetComponent<BoxCollider2D>().size;
        StartCoroutine(CountDown());

        for (int x = 0; x < 15; x++)
        {
            for (int y = 0; y < 15; y++)
            {
                GenerateNewBG(new Vector3(x, y));
            }
        }
    }

    private bool canPunch;
    private bool followSandbag;
    private void Update()
    {
        Vector3 floorPos = floor.position;
        floorPos.x = camera.position.x;
        floor.position = floorPos;

        if (Input.GetMouseButtonDown(0))
        {
            Punch();
        }
        
        if (followSandbag)
        {
            timerui.text = bag.transform.position.x.ToString("F2") + " meters";
            Vector3 camPos = Vector3.Lerp(camera.position, bag.transform.position, followSpeed * Time.deltaTime);
            camPos.z = camera.position.z;
            Vector3 diff = camPos - camera.position;
            camera.position = camPos;

            parallax.position -= diff * backgroundParallax;

            if (vel == null && bag.rb.velocity.magnitude < 0.1f)
            {
                vel = StartCoroutine(CheckVel());
            }
        }
    }

    Coroutine vel;
    private IEnumerator CheckVel()
    {
        float t = 1f;
        bool cancel = false;
        while (t > 0f)
        {
            if (bag.rb.velocity.magnitude > 0.1f)
            {
                cancel = true;
                break;
            }
            t -= Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        vel = null;
        if (!cancel)
        {
            countdown.gameObject.SetActive(true);
            int pointCalc = Mathf.FloorToInt(bag.transform.position.x / 200);
            if (pointCalc < 0)
                pointCalc = 0;

            countdown.color = Color.white;
            countdown.text = "STRENGTH POINTS EARNED:\n" + pointCalc.ToString();
            DayManager.instance.AddToPoints(DayManager.Skill.STRENGTH, pointCalc);

            yield return new WaitForSecondsRealtime(2.5f);

            SceneManager.LoadScene("Hub");
        }
    }

    private void GenerateNewBG(Vector3 pos)
    {
        GameObject newBg = GameObject.Instantiate(bg.gameObject);

        Vector3 scale = Vector3.one;
        if (pos.x % 2 != 0)
            scale.x = -1f;
        if (pos.y % 2 != 0)
            scale.y = -1f;
        
        pos.x *= bgSize.x;
        pos.y *= bgSize.y;
        pos.z = 1f;
        newBg.transform.SetParent(bg.parent);
        newBg.transform.localPosition = pos;
        newBg.transform.localScale = scale;
    }

    private void Punch()
    {
        if (!canPunch)
            return;
        punch.Stop(true, ParticleSystemStopBehavior.StopEmitting);
        punch.Play();
        float mag = Random.Range(0.5f, 2.5f);
        bag.Hit(mag);
        player.Play("Punch", -1, 0);
        player.playbackTime = 0f;
        totalForce += punchForce * mag;
    }

    IEnumerator CountDown()
    {
        float timer = 3f;
        while (timer > 0f)
        {
            timer -= Time.deltaTime;
            countdown.text = Mathf.CeilToInt(timer).ToString();

            if (Input.GetKey(KeyCode.O))
            {
                timer = 0f;
            }

            yield return new WaitForEndOfFrame();
        }
        countdown.gameObject.SetActive(false);
        canPunch = true;
        timerui.gameObject.SetActive(true);
        timer = 6f;
        while (timer > 0f)
        {
            timer -= Time.deltaTime;
            timerui.text = timer.ToString("F2") + "s";

            if (Input.GetKey(KeyCode.O))
            {
                timer = 0f;
                totalForce = 5000;
            }
            yield return new WaitForEndOfFrame();
        }

        tut.SetActive(false);
        canPunch = false;

        bag.SetShake(0f);
        yield return new WaitForSecondsRealtime(0.3f);
        followSandbag = true;
        bag.Launch(totalForce);
    }
}
