using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Canvas))]
public class UIDisplay : MonoBehaviour
{
    /*
        Bu sınıf oyun alanı içerisindeki yiyeceklerin bilgilerini gösteren UI nesnesine ait
    */


    [Tooltip("Oyuncu nesnesi")]
    GameObject player;
    [Tooltip("Oyun alanında gösterilen Food nesnesi")]
    public Food food;
    [Tooltip("Oyunun Main Camera'sı")]
    Camera _cam;

    [Space]

    [Tooltip("Oyun alanında gösterilen Food nesnesi'nin adı")]
    [HideInInspector]public string foodName;
    [Tooltip("Oyun alanında gösterilen Food nesnesi'nin adını gösteren Text nesnesi")]
    Text foodNameText;
    [Tooltip("Oyun alanında gösterilen Food nesnesi'nin Besin Puanı")]
    int BP;
    [Tooltip("Oyun alanında gösterilen Food nesnesi'nin AburCuburPuanı")]
    int ACP;


    
    private void Start()
    {
        _cam = Camera.main;
        transform.rotation = Quaternion.LookRotation(transform.position - Camera.main.transform.position, Vector3.up);
        food = transform.parent.GetComponentInChildren<FoodDisplay>().food;
        foodName = food._name;
        BP = food.BP;
        ACP = food.ACP;
        foodNameText = transform.parent.GetComponentInChildren<Text>();
        foodNameText.text = foodName;

        Slider BDSlider = transform.Find("BD").GetComponent<Slider>();
        Slider ACDSlider = transform.Find("ACD").GetComponent<Slider>();
        
        BDSlider.value = BP;
        ACDSlider.value = ACP;

        player = GameObject.FindWithTag("Player");
    }

    private void FixedUpdate()
    {
        Ray r = _cam.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(r, out RaycastHit hit))
        {
            float scaleMultiplier = 7.5f / (transform.position - hit.point).sqrMagnitude;
            transform.localScale = Vector3.one * (scaleMultiplier > .1f ? .1f : scaleMultiplier);
        }
    }


}
