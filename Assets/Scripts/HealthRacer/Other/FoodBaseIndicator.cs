using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[SelectionBase]
public class FoodBaseIndicator : MonoBehaviour
{
    public string FoodName = "";
    private void Start() {
        FoodName = GetComponentInChildren<UIDisplay>().foodName;
    }
}
