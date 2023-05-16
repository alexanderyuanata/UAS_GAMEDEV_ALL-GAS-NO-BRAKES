using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager instance;

    [SerializeField] private Queue<string> sentences;
    [SerializeField] private TextMeshProUGUI text_box;

    private CanvasGroup dialogue_box;
    private bool in_dialogue = false;

    private void Awake()
    {
        instance = this;
        sentences = new Queue<string>();
        dialogue_box = GetComponent<CanvasGroup>();
    }

    public void startDialogue(string[] dialogue)
    {
        if (in_dialogue) return;

        GameManager.instance.disableInputs();
        sentences.Clear();
        StartCoroutine(fadeInDialogue());
        in_dialogue = true;
        //lock player movement

        //load all dialogue into the queue
        foreach (string sentence in dialogue)
        {
            Debug.Log(sentence);
            sentences.Enqueue(sentence);
        }

        DisplayNextSentence();
    }

    private void DisplayNextSentence()
    {
        if (sentences.Count == 0)
        {
            StartCoroutine(fadeOutDialogue());
            in_dialogue = false;
            GameManager.instance.enableInputs();
            //unlock player movement
            return;
        }

        StartCoroutine(displaySentence(sentences.Dequeue()));
    }

    private IEnumerator displaySentence(string text)
    {
        text_box.text = "";
        foreach (char c in text.ToCharArray())
        {
            text_box.text += c;
            yield return null;
        }
    }

    private IEnumerator fadeInDialogue()
    {
        for (float i = 0; i <= 1; i += 0.2f)
        {
            dialogue_box.alpha = i;
            yield return null;
        }
    }

    private IEnumerator fadeOutDialogue()
    {
        for (float i = 1; i >= 0; i -= 0.2f)
        {
            dialogue_box.alpha = i;
            yield return null;
        }
    }

    private void Update()
    {
        //if in dialogue and player  left clicks
        if (in_dialogue && (/**Input.GetKeyDown(KeyCode.E) ||**/ Input.GetMouseButtonDown(0)))
        {
            DisplayNextSentence();
        }
    }
}
