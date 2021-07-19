using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UIClick : MonoBehaviour
{

    [Tooltip("Oyuncu nesnesi")]
    public PlayerControls player;
    [Tooltip("Arayüzde görütülenmek istenen Food")]
    public Food displayingFoodUI;

    (List<Transform>, List<FoodBaseIndicator>) _allFoods;

    void Start()
    {
        if (player == null) player = GameObject.FindWithTag("Player").GetComponent<PlayerControls>();

        transform.Find("UI").GetComponentInChildren<Button>().onClick.AddListener(RemoveFromList);
        _allFoods = GetAllFoods();
    }

    private (List<Transform>, List<FoodBaseIndicator>) GetAllFoods()
    {
        Transform _foodsWrap = GameObject.FindWithTag("FoodsWrap").transform;
        (List<Transform>, List<FoodBaseIndicator>) foods;
        foods.Item1 = new List<Transform>();
        foods.Item2 = new List<FoodBaseIndicator>();

        foreach (Transform item in _foodsWrap)
        {
            foods.Item1.Add(item);
            foods.Item2.Add(item.GetComponent<FoodBaseIndicator>());
        }
        return foods;
    }
    public void RemoveFromList()
    {
        if (player.canPlayerMove)
        {
            GameObject temp = null;
            foreach (GameObject go in Resources.FindObjectsOfTypeAll(typeof(GameObject)) as GameObject[])
            {
                if (_allFoods.Item1.Contains(go.transform))
                {
                    string foodName = _allFoods.Item2[_allFoods.Item1.IndexOf(go.transform)].FoodName;
                    if (foodName == displayingFoodUI._name)
                        temp = go;
                }
                
            }
            temp.SetActive(true);
            player.cartContent.Remove(displayingFoodUI);
        }
    }
}
