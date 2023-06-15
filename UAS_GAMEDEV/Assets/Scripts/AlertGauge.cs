using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AlertGauge : MonoBehaviour
{
    [SerializeField] private RectTransform mask_transform;
    [SerializeField] private Slider slider;

    private void Start()
    {
        if (mask_transform == null || slider == null)
        {
            Debug.LogError("Assign to alert gauge!");   
        }
        slider.maxValue = EnemyController.instance.getMaxAlert();
        slider.minValue = 0;
    }

    private void Update()
    {
        slider.value = EnemyController.instance.getAlertAmount();
    }
}
