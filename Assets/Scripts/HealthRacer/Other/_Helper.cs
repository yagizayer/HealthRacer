using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class _Helper : MonoBehaviour
{

    public IEnumerator rotateObject(Transform item, Vector3 rotationAxis, float rotationSpeed, float waveHeight)
    {
        while (true)
        {
            item.Rotate(rotationAxis, 1 * rotationSpeed, Space.Self);
            item.position += Mathf.Sin(Time.time * Mathf.PI) / waveHeight * Vector3.up;
            yield return null;
        }
    }
    public IEnumerator lerpPositions2D(RectTransform objectToLerp, Vector3 startingPos, Vector3 targetPos, float speed, bool controlVal)
    {

        float lerpVal = 0;
        while (lerpVal < 1)
        {
            controlVal = true;
            objectToLerp.localPosition = Vector3.Lerp(startingPos, targetPos, lerpVal);

            yield return null;
            lerpVal += Time.deltaTime * speed;
        }
        if (lerpVal >= 1)
            controlVal = false;

    }
    public GameObject fetchDeactivatedGameObject(string name)
    {
        foreach (GameObject go in Resources.FindObjectsOfTypeAll(typeof(GameObject)) as GameObject[])
        {
            if (go.name == name)
            {
                return go;
            }
        }
        return null;
    }
}
