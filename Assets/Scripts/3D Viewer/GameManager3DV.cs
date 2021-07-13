using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager3DV : MonoBehaviour
{
    public Vector3 LastPlacedModelPosition = new Vector3(50, 0, 0);
    public GameObject cameraModelPair;
    public GameObject UIElementPrefab;
    public Model_SO activeModel;
    List<Model_SO> allModels = new List<Model_SO>();
    GameObject listBase;
    void Awake()
    {
        Model_SO[] x = Resources.LoadAll<Model_SO>("Models");
        foreach (Model_SO item in x)
            allModels.Add(item);

        listBase = GameObject.FindWithTag("UIListBase");

        // UI elementlerini oluştur
        List<GameObject> UIElements = new List<GameObject>();

        foreach (Model_SO item in allModels)
        {
            GameObject temp = Instantiate(UIElementPrefab);
            UIElements.Add(temp);
            temp.transform.SetParent(listBase.transform);
            temp.GetComponentInChildren<DisplayModel_UI>().model = item;
            temp.GetComponent<RectTransform>().sizeDelta = Vector2.zero;
            temp.GetComponent<RectTransform>().localPosition = Vector2.zero;
            temp.GetComponent<RectTransform>().localScale = Vector2.one;
            temp.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
        }
    }


}
