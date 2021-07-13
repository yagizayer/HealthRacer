//Dogukan Kaan Bozkurt
//      github.com/dkbozkurt
//GEFEASOFT

/* Displaying name of the object in a certain place of the scene.
 * 
 * Put this on the object (which need a collider for this to work) and drag a Text 
 * object onto the slot in the inspector.
 *
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;   //Import the library.
using TMPro;    //Import the library.

public class DisplayName : MonoBehaviour
{
    [SerializeField] private GameObject textObj;      //Give the tmp into inspector
    private TMP_Text textObject;

    //Accessing the TMP component.
    private void Awake()
    {
        textObj = transform.parent.GetComponent<AddComponents>().textObj;
        textObject = textObj.GetComponent<TMP_Text>();
    }

    public void Start()
    {
        textObj.SetActive(false);
    }
    public void OnMouseOver()
    {
        string organName = "";
        if (gameObject.name == "HearthHolder") organName = "Kalp";
        if (gameObject.name == "MouthHolder") organName = "Ağız";
        if (gameObject.name == "GallBladderHolder") organName = "Safra Kesesi";
        if (gameObject.name == "EsophagusHolder") organName = "Yemek Borusu";
        if (gameObject.name == "PancreasHolder") organName = "Pankreas";
        if (gameObject.name == "LiverHolder") organName = "Karaciğer";
        if (gameObject.name == "StomachHolder") organName = "Mide";
        if (gameObject.name == "SmallIntestineHolder") organName = "İnce Bağırsak";
        if (gameObject.name == "ColonHolder") organName = "Kalın Bağırsak";
        if (gameObject.name == "CerebrumHolder") organName = "Bayincik";
        if (gameObject.name == "BrainHolder") organName = "Beyin";
        if (gameObject.name == "Skull") organName = "Kafatası";
        if (gameObject.name == "Skeleton") organName = "İskelet";
        if (gameObject.name == "LungsHolder") organName = "Akciğerler";
        if (gameObject.name == "KidneysHolder") organName = "Böbrekler";
        if (gameObject.name == "BladderHolder") organName = "Mesane";



        textObj.SetActive(true);
        //Debug.Log("Name of the object is" + this.gameObject.name );
        textObject.text = organName;

    }
    public void OnMouseExit()
    {
        textObj.SetActive(false);
    }

}
