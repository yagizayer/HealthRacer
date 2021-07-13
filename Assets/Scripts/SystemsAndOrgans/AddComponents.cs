using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddComponents : MonoBehaviour
{
    public GameObject textObj;
    private void Awake() {
        foreach (Transform child in transform)
        {
            child.gameObject.AddComponent<DisplayName>();
            child.gameObject.AddComponent<MouseDrag>();
        }
    }
}
