using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonShift : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private RectTransform buttonRectTransform;
    private Vector2 originalPosition;
    private bool isHovering;
    [SerializeField] private float shiftPercentage = 0.2f;

    private void Awake()
    {
        buttonRectTransform = GetComponent<RectTransform>();
        originalPosition = buttonRectTransform.anchoredPosition;
    }

    private void Update()
    {
        if (isHovering)
        {
            Vector2 targetPosition = originalPosition + new Vector2(buttonRectTransform.sizeDelta.x * shiftPercentage, 0f);
            buttonRectTransform.anchoredPosition = Vector2.Lerp(buttonRectTransform.anchoredPosition, targetPosition, Time.deltaTime * 10f);
        }
        else
        {
            buttonRectTransform.anchoredPosition = Vector2.Lerp(buttonRectTransform.anchoredPosition, originalPosition, Time.deltaTime * 10f);
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        isHovering = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        isHovering = false;
    }
}
