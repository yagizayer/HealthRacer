using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

[Serializable]
public class SlideObject : UnityEvent<bool> { }

public class SlideList : MonoBehaviour, IPointerDownHandler
{
    public SlideObject slidingEvent;
    private void Start() {
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        slidingEvent.Invoke(true);
    }
}
