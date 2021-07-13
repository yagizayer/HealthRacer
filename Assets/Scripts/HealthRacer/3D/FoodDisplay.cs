using System.Collections;
using System.Collections.Generic;
using UnityEngine;



[RequireComponent(typeof(BoxCollider))]
public class FoodDisplay : MonoBehaviour
{
    [Tooltip("Göserilecek Food Nesnesi")]
    public Food food;
    // Start is called before the first frame update
    private void Start()
    {
        GetComponent<MeshFilter>().mesh = food.shape;
        GetComponent<MeshRenderer>().material = food.material;
        this.name = food.name;
    }
}
