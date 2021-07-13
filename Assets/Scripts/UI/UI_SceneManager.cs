using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UI_SceneManager : MonoBehaviour
{


    public void DirectScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);

        // Add exceptions here
        if (GameObject.FindGameObjectWithTag("Music")) Destroy(GameObject.FindGameObjectWithTag("Music"));
    }
    public void CloseApp()
    {
        Debug.Log("cikis");
        Application.Quit();
    }
}
