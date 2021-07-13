using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PositionContext : MonoBehaviour
{
    [Tooltip("ObjectList to be listed")]
    public List<RectTransform> context;
    [Tooltip("Object that all context is listed and masked")]
    public RectTransform parentObject;
    [Range(1, 10)]
    [Tooltip("Desired row number for single column")]
    public int desiredRowCount = 5;
    [Range(.1f, 10)]
    [Tooltip("Speed of sliding animation")]
    public float slidingSpeed = 2f;
    int columnCount = 1;
    int rightSlidingCount = 0;
    public Vector2 perObjectSize;
    bool sliding = false;
    private void Start()
    {
        if (parentObject == null) parentObject = (RectTransform)transform;
        if (context.Count == 0)
            foreach (RectTransform item in transform)
                context.Add(item);
        resizeAndPositionObjects(context);
    }

    public void resizeAndPositionObjects(List<RectTransform> objectList)
    {
        columnCount = Mathf.CeilToInt((float)objectList.Count / desiredRowCount);

        parentObject.anchorMax = new Vector2(columnCount, 1);

        perObjectSize = new Vector2(1f / columnCount, 1f / desiredRowCount);
        for (int columnNo = 0; columnNo < columnCount; columnNo++)
        {
            List<RectTransform> subList = objectList.GetRange(desiredRowCount * columnNo, (objectList.Count - desiredRowCount * columnNo) > desiredRowCount ? desiredRowCount : (objectList.Count - desiredRowCount * columnNo));

            for (int rowNo = 0; rowNo < subList.Count; rowNo++)
            {
                subList[rowNo].anchorMin = new Vector2(perObjectSize.x * columnNo, 1 - (perObjectSize.y * (rowNo + 1)));
                subList[rowNo].anchorMax = new Vector2(perObjectSize.x * (columnNo + 1), 1 - perObjectSize.y * rowNo);
            }
        }
    }
    public void SlideMe(bool slidingToRight)
    {
        if (slidingToRight)
        {
            if (rightSlidingCount == columnCount - 1) return;
            Vector3 currentPos = parentObject.localPosition;
            Vector3 targetPos = parentObject.localPosition + new Vector2(-parentObject.rect.width / columnCount, 0).toVector3();

            if (!sliding)
            {
                StartCoroutine(lerpPositions2D(parentObject, currentPos, targetPos, slidingSpeed));
                rightSlidingCount++;
            }
        }
        else
        {
            if (rightSlidingCount == 0) return;
            Vector3 currentPos = parentObject.localPosition;
            Vector3 targetPos = parentObject.localPosition + new Vector2(parentObject.rect.width / columnCount, 0).toVector3();

            if (!sliding)
            {
                StartCoroutine(lerpPositions2D(parentObject, currentPos, targetPos, slidingSpeed));
                rightSlidingCount--;
            }

        }
    }
    public IEnumerator lerpPositions2D(RectTransform objectToLerp, Vector3 startingPos, Vector3 targetPos, float speed)
    {
        float lerpVal = 0;
        while (lerpVal < 1)
        {
            sliding = true;
            objectToLerp.localPosition = Vector3.Lerp(startingPos, targetPos, lerpVal);

            yield return null;
            lerpVal += Time.deltaTime * speed;
        }
        if (lerpVal >= 1)
        {
            sliding = false;
        }

    }

}
