using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


public class GetModel : MonoBehaviour, IPointerDownHandler
{
    public RigRotator rigRotator;
    public Model_SO currentModel;
    GameManager3DV GameManager;
    private void Start()
    {
        GameManager = GameObject.Find("GameManager").GetComponent<GameManager3DV>();
        rigRotator = Camera.main.GetComponent<RigRotator>();
        currentModel = gameObject.GetComponentInChildren<DisplayModel_UI>().model;
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        GameObject modelBase = GameObject.FindGameObjectWithTag("MainModelBase");
        Transform temp = null;
        foreach (Transform item in modelBase.transform)
        {
            temp = item;
        }
        if (temp)
            GameObject.Destroy(temp.gameObject);

        GameObject model_3D = new GameObject();

        MeshFilter meshFilter = model_3D.AddComponent<MeshFilter>();
        meshFilter.mesh = currentModel.mesh;

        MeshRenderer meshRenderer = model_3D.AddComponent<MeshRenderer>();
        meshRenderer.materials = currentModel.materials.ToArray();

        model_3D.transform.position = Vector3.zero;
        model_3D.transform.localScale = Vector3.one * currentModel.meshScale;
        model_3D.transform.localPosition = Vector3.zero + currentModel.localPositionOffset;
        model_3D.transform.eulerAngles = currentModel.eulerRotation;

        model_3D.transform.SetParent(modelBase.transform);
        GameObject.Find("ModelName").GetComponentInChildren<Text>().text = currentModel.name;

        rigRotator.recalculateDistance(2,Camera.main.gameObject);


        GameManager.activeModel = currentModel;
    }
}
