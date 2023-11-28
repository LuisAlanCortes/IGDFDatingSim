using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterAnimator : MonoBehaviour
{
    [SerializeField] Image charSprite;
    [SerializeField] float debugTime;
    [SerializeField] float debugIntensity;
    [SerializeField] float debugFrq;


    private RectTransform charRect;

    private void Awake()
    {
        charRect = charSprite.rectTransform;
    }

    private void OnGUI()
    {
        if (GUI.Button(new Rect(0, 0, 100, 100), "Boing"))
        {
            StartCoroutine(CoBounce(debugTime, debugIntensity, debugFrq));
        }
    }

    private IEnumerator CoBounce(float time, float frequency, float intensity)
    {
        float t = 0f;
        while (t < time)
        {
            float b = Mathf.Sin(t * frequency) * intensity;
            b = Mathf.Lerp(b, 0f, t / time);
            Vector2 vec = new Vector2(0f, b);
            charRect.localPosition = vec;
            t += Time.deltaTime;
            yield return null;
        }

        charRect.localPosition = Vector2.zero;
    }
}
