using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class WealthManager : MonoBehaviour
{
    public TextMeshPro TimeText;
    public TextMeshPro SuccessPoints;
    public GameObject Agents;
    public float agentFrequency;
    public float curveUp;
    public float powerUp;
    public float pointThreshold = 90f;
    public float clumpThreshold = 30f;

    public Vector3 spawnRing;

    public static WealthManager Instance;
    float totalTime;

    private void Awake()
    {
        Instance = this;
        StartCoroutine(EnemySpawner());
    }

    private void Update()
    {
        totalTime += Time.deltaTime;
        TimeText.text = $"Time: {Mathf.FloorToInt(totalTime)}";
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(Vector3.zero, spawnRing);
    }


    bool paused;
    IEnumerator EnemySpawner()
    {
        while (!paused)
        {
            GameObject agent = GameObject.Instantiate(Agents);

            Vector3 sphere = Vector3.Scale(Random.insideUnitCircle.normalized, spawnRing / 2f);
            sphere.z = 1f;
            agent.transform.position = sphere;

            if (totalTime > clumpThreshold && (Random.value > 0.8f))
            {
                for (int i = 0; i < Random.Range(2, 5); i++)
                {
                    agent = GameObject.Instantiate(Agents);
                    agent.transform.position = sphere + new Vector3(Random.insideUnitCircle.x, Random.insideUnitCircle.y, 0f);
                }
            }

            yield return new WaitForSeconds(agentFrequency + Random.Range(0.5f, agentFrequency) - (Mathf.Pow(totalTime, powerUp) / curveUp));
        }
    }

    public void EndGame()
    {
        if (paused)
            return;

        int pt = Mathf.FloorToInt(totalTime / pointThreshold);
        paused = true;
        SuccessPoints.transform.parent.gameObject.SetActive(true);
        SuccessPoints.text = $"WEALTHINESS POINTS EARNED: {pt}";
        DayManager.instance.AddToPoints(DayManager.Skill.WEALTHINESS, pt);

        Invoke(nameof(GoGoGo), 3f);
    }

    private void GoGoGo()
    {
        SceneManager.LoadScene("Hub");
    }
}
