using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EyelidController : MonoBehaviour
{
    [SerializeField] private Image top;
    [SerializeField] private Image bot;
    
    private float initial_top;
    private float initial_bot;

    // Start is called before the first frame update
    void Start()
    {
        initial_top = top.rectTransform.anchoredPosition.y;
        initial_bot = bot.rectTransform.anchoredPosition.y;
    }

    IEnumerator openLid(float duration)
    {
        float target_top = top.rectTransform.sizeDelta.y + (top.rectTransform.sizeDelta.y / 2);
        float target_bot = -1 * (bot.rectTransform.sizeDelta.y + (bot.rectTransform.sizeDelta.y / 2));

        float startTime = Time.time;
        float endTime = startTime + duration;

        float top_pos = top.rectTransform.anchoredPosition.y;
        float bot_pos = bot.rectTransform.anchoredPosition.y;

        while (Time.time < endTime)
        {
            float normalizedTime = (Time.time - startTime) / duration;
            float current_top = Mathf.Lerp(top_pos, target_top, normalizedTime);
            float current_bot = Mathf.Lerp(bot_pos, target_bot, normalizedTime);

            Vector2 new_top = top.rectTransform.anchoredPosition;
            new_top.y = current_top;
            top.rectTransform.anchoredPosition = new_top;

            Vector2 new_bot = top.rectTransform.anchoredPosition;
            new_bot.y = current_bot;
            bot.rectTransform.anchoredPosition = new_bot;

            yield return null;
        }

    }

    IEnumerator slightlyClose(float duration)
    {
        float target_top = top.rectTransform.anchoredPosition.y - (top.rectTransform.sizeDelta.y / 4);
        float target_bot = bot.rectTransform.anchoredPosition.y + (bot.rectTransform.sizeDelta.y / 4);

        float startTime = Time.time;
        float endTime = startTime + duration;

        float top_pos = top.rectTransform.anchoredPosition.y;
        float bot_pos = bot.rectTransform.anchoredPosition.y;

        while (Time.time < endTime)
        {
            float normalizedTime = (Time.time - startTime) / duration;
            float current_top = Mathf.Lerp(top_pos, target_top, normalizedTime);
            float current_bot = Mathf.Lerp(bot_pos, target_bot, normalizedTime);

            Vector2 new_top = top.rectTransform.anchoredPosition;
            new_top.y = current_top;
            top.rectTransform.anchoredPosition = new_top;

            Vector2 new_bot = top.rectTransform.anchoredPosition;
            new_bot.y = current_bot;
            bot.rectTransform.anchoredPosition = new_bot;

            yield return null;
        }
    }

    public void startOpen(float duration)
    {
        StartCoroutine(openLid(duration));
    }

    public void startBar(float duration)
    {
        StartCoroutine(slightlyClose(duration));
    }
}
