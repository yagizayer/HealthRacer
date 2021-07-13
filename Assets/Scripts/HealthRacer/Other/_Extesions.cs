using UnityEngine;


public static class _Extesions
{
    public static Vector3 toVector3(this Vector2 vector2)
    {
        return new Vector3(vector2.x, vector2.y, 0);
    }
    public static Vector3 toFloor(this Vector3 vector3)
    {
        return new Vector3(vector3.x, 0, vector3.z);
    }
    public static RectTransform resizeWidth(this RectTransform rectTransform)
    {
        rectTransform.sizeDelta = new Vector2(rectTransform.sizeDelta.x * ((float)Screen.height / 1000), rectTransform.sizeDelta.y);
        return rectTransform;
    }
    public static RectTransform scaleWidth(this RectTransform rectTransform)
    {
        Vector2 oldSize = rectTransform.sizeDelta;
        Vector2 newSize = new Vector2(rectTransform.sizeDelta.x * ((float)Screen.height / 1000), rectTransform.sizeDelta.y);
        Vector2 tempRatio = new Vector2(newSize.x / oldSize.x, newSize.y / oldSize.y);

        rectTransform.localScale = tempRatio;
        return rectTransform;
    }
    public static Transform Clear(this Transform transform)
    {
        foreach (Transform child in transform)
        {
            GameObject.Destroy(child.gameObject);
        }
        return transform;
    }
}
