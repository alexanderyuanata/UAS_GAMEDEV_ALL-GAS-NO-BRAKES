using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TooltipScript : MonoBehaviour
{
    public static TooltipScript instance;

    [SerializeField] private TextMeshProUGUI textmesh;
    [SerializeField] private CanvasGroup canvasGroup;

    [SerializeField] private float appear_time;
    [SerializeField] private float fade_time;

    private bool isFadeFinished = false;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        if (textmesh == null || canvasGroup == null)
        {
            Debug.LogError("Assign a text mesh pro/canvas group on tooltip controller! Should be on textmeshpro object!");
        }
        textmesh.enabled = false;
    }

    public void startTooltip(string text)
    {
        StartCoroutine(displayText(text));
    }

    IEnumerator displayText(string text_todisplay)
    {
        textmesh.enabled = true;
        canvasGroup.alpha = 1f;
        textmesh.text = text_todisplay;

        yield return new WaitForSeconds(appear_time);

        isFadeFinished = false;
        StartCoroutine(fadeText());

        while (!isFadeFinished) yield return null;

        textmesh.enabled = false;
    }

    IEnumerator fadeText()
    {
        float timer = 0f;
        float startAlpha = canvasGroup.alpha;
        float targetAlpha = 0f;

        while (timer < fade_time)
        {
            timer += Time.deltaTime;
            float t = timer / fade_time;
            canvasGroup.alpha = Mathf.Lerp(startAlpha, targetAlpha, t);

            yield return null;
        }

        canvasGroup.alpha = targetAlpha;
        isFadeFinished = true;
    }       
}
