using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UIClick : MonoBehaviour
{

    [Tooltip("Oyuncu nesnesi")]
    public GameObject player;
    [Tooltip("Arayüzde görütülenmek istenen Food")]
    public Food displayingFoodUI;


    void Start()
    {
        if (player == null) player = GameObject.FindWithTag("Player");
        transform.Find("UI").GetComponentInChildren<Button>().onClick.AddListener(RemoveFromList);
    }

    public void RemoveFromList()
    {
        if (player.GetComponent<PlayerControls>().canPlayerMove)
        {
            GameObject temp = null;
            foreach (GameObject go in Resources.FindObjectsOfTypeAll(typeof(GameObject)) as GameObject[])
            {

                if (go.transform.root != go.transform
                    && (go.transform.parent.name == "FoodsWrap")
                    && (go.name == displayingFoodUI._name))
                {
                    temp = go;
                    break;
                }
            }
            temp.SetActive(true);
            player.GetComponent<PlayerControls>().cartContent.Remove(displayingFoodUI);
        }
    }
}
