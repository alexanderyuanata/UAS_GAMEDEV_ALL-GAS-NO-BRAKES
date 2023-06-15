using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Keypad : MonoBehaviour
{
    public static Keypad instance;

    [SerializeField] private short[] key_code = new short[4];
    [SerializeField] private AudioClip button_press;
    [SerializeField] private AudioClip wrong_code;
    [SerializeField] private KeypadAlarm alarm;
    [SerializeField] private Queue<short> keys = new Queue<short>();

    private void Awake()
    {
        instance = this;
    }

    public void addKeypress(short key)
    {
        SFXManager1.instance.playClip(button_press);
        keys.Enqueue(key);
        
        if (keys.Count == 4)
        {
            for (int i = 0; i < 4; i++)
            {
                if (keys.Dequeue() != key_code[i])
                {
                    //code is wrong
                    SFXManager2.instance.playClip(wrong_code);
                    alarm.alarm();

                    keys.Clear();
                    return;
                }
            }
            //code is right by this point
            PrologueSequence.instance.StartCoroutine(PrologueSequence.instance.endSequence());

            keys.Clear();
        }
    }
}
