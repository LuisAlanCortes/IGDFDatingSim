using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterAnimator : MonoBehaviour
{
    [SerializeField] CharacterDatabase database;
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

    private const string DEFAULT_SPRITE = "default";
    public void SetCharacter(string id)
    {
        if (database.SetCurrentChar(id))
        {
            ChangeSprite(DEFAULT_SPRITE);
        }
    }

    public void ChangeSprite(string id)
    {
        Sprite spr = database.GetSprite(id);
        if (spr != null)
        {
            charSprite.sprite = spr;
        }
    }

    bool bouncing;
    public void Bounce(float time = 1f, float frequency = 1f, float intensity = 1f)
    {
        if (bouncing)
            return;
        StartCoroutine(CoBounce(time, frequency, intensity));
    }

    private IEnumerator CoBounce(float time, float frequency, float intensity)
    {
        bouncing = true;
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
        bouncing = false;

        charRect.localPosition = Vector2.zero;
    }
}
