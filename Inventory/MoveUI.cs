using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MoveUI : MonoBehaviour, IDragHandler
{
    RectTransform currentRect;
    private void OnEnable()
    {
        currentRect.anchoredPosition = new Vector3(0, 0, 0);
        this.currentRect.SetAsLastSibling();
    }

    public void OnDrag(PointerEventData eventData)
    {
        currentRect.anchoredPosition += eventData.delta;
    }

    private void Awake()
    {
        currentRect = GetComponent<RectTransform>();
    }
}
