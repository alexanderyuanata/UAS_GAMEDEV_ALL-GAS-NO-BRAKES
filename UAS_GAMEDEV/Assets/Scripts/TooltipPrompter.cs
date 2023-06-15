using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TooltipPrompter : MonoBehaviour
{
    [TextArea(5,3)]
    [SerializeField] private string tip;
    [SerializeField] private bool delete_after_trigger = true;

    [SerializeField] private float appear_time;
    [SerializeField] private float fade_time;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.transform.tag == "Player")
        {
            TooltipScript.instance.startTooltip(tip, appear_time, fade_time);
            if (delete_after_trigger)
            {
                Destroy(gameObject);
            }
        }
    }
}
