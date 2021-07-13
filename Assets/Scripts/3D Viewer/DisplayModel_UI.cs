using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayModel_UI : MonoBehaviour
{
    [Tooltip("Scriptable Object that you want to display")]
    public Model_SO model;

    _Helper helper;
    GameObject cameraModelPair;
    GameObject myPair;
    GameObject modelObject;
    GameObject cameraObject;
    GameObject textureObject;

    private void Start()
    {
        Vector3 myPosition = FindObjectOfType<GameManager3DV>().LastPlacedModelPosition + Vector3.right * 5;
        FindObjectOfType<GameManager3DV>().LastPlacedModelPosition = myPosition;

        cameraModelPair = GameObject.Find("GameManager").GetComponent<GameManager3DV>().cameraModelPair;
        helper = gameObject.AddComponent<_Helper>();

        // yeni pair oluştur
        myPair = Instantiate(cameraModelPair);
        foreach (Transform item in myPair.transform)
        {
            if (item.name == "ModelParent") modelObject = item.gameObject;
            if (item.name == "Camera") cameraObject = item.gameObject;
        }
        foreach (Transform item in transform)
            if (item.name == "Background") textureObject = item.GetComponentInChildren<RawImage>().gameObject;

        // konumunu ayarla tekrarEdilemez
        myPair.transform.position = myPosition;
        modelObject.transform.localScale = Vector3.one * model.meshPreviewScale;
        modelObject.transform.localPosition = model.localPositionOffset;
        modelObject.transform.eulerAngles = model.eulerRotation;

        // modelin bilgilerini gir
        MeshFilter meshFilter = modelObject.AddComponent<MeshFilter>();
        meshFilter.mesh = model.previewMesh;

        MeshRenderer meshRenderer = modelObject.AddComponent<MeshRenderer>();
        meshRenderer.materials = model.materials.ToArray();

        // kameradan render texture oluştur 
        RenderTexture renderTexture = new RenderTexture(256, 256, 16, RenderTextureFormat.ARGB32);
        cameraObject.GetComponent<Camera>().targetTexture = renderTexture;

        // kendi elementine bu render texture'ü ata
        textureObject.GetComponent<RawImage>().texture = renderTexture;

        // StartCoroutine(helper.rotateObject(modelObject.transform, model.rotationAxis, .5f, 300));
    }


}
