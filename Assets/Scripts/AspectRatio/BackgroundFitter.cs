using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundFitter : MonoBehaviour
{
    [SerializeField]
    private SpriteRenderer _background;

    private void Start()
    {
        float screenRatio = (float)Screen.width / (float)Screen.height;
        float targetRatio = _background.bounds.size.x / _background.bounds.size.y;

        if (screenRatio >= targetRatio)
        {
            Camera.main.orthographicSize = _background.bounds.size.y / 2;
        }
        else
        {
            float differenceInSize = targetRatio / screenRatio;
            Camera.main.orthographicSize = _background.bounds.size.y / 2 * differenceInSize;
        }
    }
}
