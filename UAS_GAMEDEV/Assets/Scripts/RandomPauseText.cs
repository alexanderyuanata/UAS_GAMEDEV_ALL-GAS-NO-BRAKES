using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RandomPauseText : MonoBehaviour
{
    public static RandomPauseText instance;

    [SerializeField] private TextMeshProUGUI textmesh;
    [SerializeField] private string[] random_text;

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        if (textmesh == null)
        {
            textmesh = GetComponent<TextMeshProUGUI>();
        }
    }


    public void genRandText()
    {
        textmesh.text = random_text[Random.Range(0, random_text.Length)];
    }
    
}
