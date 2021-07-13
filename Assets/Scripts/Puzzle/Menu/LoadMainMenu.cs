using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.SceneManagement.SceneManager;

public class LoadMainMenu : MonoBehaviour
{
  
    public void GoMainMenu()
    {
        LoadScene("PuzzleMenu");
    }
}
