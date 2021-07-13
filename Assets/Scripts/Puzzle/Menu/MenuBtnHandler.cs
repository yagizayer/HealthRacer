using System;
using System.Collections;
using System.Collections.Generic;
using TMPro.Examples;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.SceneManagement.SceneManager;

public class MenuBtnHandler : MonoBehaviour
{
    public Image image;
    private VertexColorCycler _vertexColorCycler;
    [SerializeField] GameObject _musicObject;

    void Start(){
        image.alphaHitTestMinimumThreshold = 0.5f;
        
        _vertexColorCycler = transform.GetChild(0).GetComponent<VertexColorCycler>();
        _vertexColorCycler.ChangeColorToDefault();
    }
    // Update is called once per frame
    public void mouseOn()
    {
        if (gameObject.tag == "flowerBtn")
        {
            _vertexColorCycler.animateColor = true;
            StartCoroutine(_vertexColorCycler.AnimateVertexColors());
            gameObject.transform.localScale = new Vector3(11f, 11f, 1);
        }
        else
        {
            
            _vertexColorCycler.animateColor = true;
            StartCoroutine(_vertexColorCycler.AnimateVertexColors());
            gameObject.transform.localScale = new Vector3(2.4f, 2.4f, 1);
            //Debug.Log(_vertexColorCycler.animateColor);
        }
    }

    public void mouseOFFF()
    {
        if (gameObject.tag == "flowerBtn")
        {
            _vertexColorCycler.animateColor = false;
            gameObject.transform.localScale = new Vector3(10.5f, 10.5f, 1);
            _vertexColorCycler.ChangeColorToDefault();
        }
        else
        {
            _vertexColorCycler.animateColor = false;
            gameObject.transform.localScale = new Vector3(2f, 2f, 1);
            _vertexColorCycler.ChangeColorToDefault();
        }
        
        //Debug.Log(_vertexColorCycler.animateColor );
    }

    public void LoadScenes()
    {
        LoadScene(gameObject.tag.ToString());
    }

    public void ExitScene()
    {
        LoadScene("GameList");
        Destroy(_musicObject);
    }
}
