using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CartContentControl : MonoBehaviour
{
    [Header("Scripts")]
    [Tooltip("Oyuncunun kontrol Scripti")]
    public PlayerControls player;
    [Tooltip("Canvas'ta görülecek Food nesnesinin örneği")]
    public GameObject UIPrefab;
    [Tooltip("Canvas nesnesi")]
    public Transform canvas;
    [SerializeField]
    [Tooltip("Canvas'taki Food nesnelerinin UI larını tutan liste(GameObject)")]
    List<GameObject> UIObjectList = new List<GameObject>();
    [Tooltip("Toplam Besin Puanı ve toplam AburCubur Puanı")]
    public float totalBP = 0, totalACP = 0;
    [Tooltip("Canvas'taki sağ üstte bulunan barlar Süre ve butonların oluşturduğu alan")]
    public GameObject levelControls;
    public List<GameObject> scalingMenus = new List<GameObject>();

    // nesnelerin sıralı şekilde yerleşmesini sağlayan aralıklar
    // float xGap = 0, yGap = 0, xStart = 0, yStart = 0, scaleMultiplier = 1;
    int knownCount = 0;



    private void Start()
    {
        // scaleMultiplier = (float)Screen.height / 1000;
        // scaleMultiplier *= 1.5f;

        // xStart = -Screen.width / 2 + 150 * scaleMultiplier;
        // yStart = Screen.height / 2 - 100 * scaleMultiplier;
        // xGap = Screen.width / 8f;
        // yGap = -Screen.height / 6.5f;
        // levelControls.transform.localScale *= scaleMultiplier;
        // foreach (GameObject menu in scalingMenus)
        // {
        //     menu.transform.localScale *= scaleMultiplier;
        // }


        // Rect temp = levelControls.GetComponent<RectTransform>().rect;
        // temp.x *= scaleMultiplier;
        // temp.y *= scaleMultiplier;
        // levelControls.transform.localPosition = new Vector3(Screen.width / 2 + temp.x, Screen.height / 2 + temp.y, 0);
    }
    private void FixedUpdate()
    {
        if (player.cartContent.Count != knownCount)
        {
            totalBP = 0;
            totalACP = 0;
            UIObjectList = new List<GameObject>();
            foreach (Transform child in fetchDeactivatedGameObject("CanvasList").transform)
            {
                GameObject.Destroy(child.gameObject);
            }
            foreach (Food item in player.cartContent)
            {
                GameObject UIObject = instantiateObject();

                UIObject.GetComponentInChildren<Image>().sprite = item.sprite;
                UIObject.GetComponentInChildren<Text>().text = item._name;
                UIObject.name = item._name + "_UI";

                float BP = item.BP;
                float ACP = item.ACP;
                totalACP += ACP;
                totalBP += BP;
                Slider BDSlider = UIObject.transform.Find("UI").Find("BD").GetComponent<Slider>();
                Slider ACDSlider =  UIObject.transform.Find("UI").Find("ACD").GetComponent<Slider>();
                
                BDSlider.value = BP;
                ACDSlider.value = ACP;

                UIClick clickOperaion = UIObject.AddComponent<UIClick>();
                clickOperaion.displayingFoodUI = item;

                UIObjectList.Add(UIObject);
            }
            orderList(UIObjectList);

            updatePointCounters();


            knownCount = player.cartContent.Count;
        }
    }

    void orderList(List<GameObject> tempCart)
    {
        float requiredColumn = Mathf.Ceil((float)tempCart.Count / 5);

        for (int columnNo = 0; columnNo < requiredColumn; columnNo++)
        {
            List<GameObject> altList = tempCart.GetRange(5 * columnNo, tempCart.Count - 5 * columnNo);
            for (int rowNo = 0; rowNo < altList.Count; rowNo++)
            {
                GameObject test = altList[rowNo];
                // test.transform.localPosition = new Vector3(xStart, yStart, 0);
                // test.transform.localPosition += new Vector3(xGap * columnNo, yGap * rowNo, 0);
                // test.transform.localScale = Vector3.one * (scaleMultiplier * 0.8f);
            }
        }
    }

    GameObject instantiateObject()
    {
        GameObject test = Instantiate(UIPrefab, canvas.transform, false);
        // test.transform.localPosition = Vector3.zero;
        // test.transform.localEulerAngles = Vector3.zero;
        // test.transform.localScale = Vector3.one;
        return test;
    }

    void updatePointCounters()
    {
        // ekrandaki ACP ve BP değerlerini hesapla
        Slider BDSlider = levelControls.transform.Find("BD").GetComponent<Slider>();
        Slider ACDSlider = levelControls.transform.Find("ACD").GetComponent<Slider>();

        BDSlider.value = totalBP;
        totalACP *= 3;
        ACDSlider.value = totalACP;
        
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
