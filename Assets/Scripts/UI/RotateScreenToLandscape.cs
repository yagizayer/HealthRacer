using System.Collections;
using System.Collections.Generic;
using ntw.CurvedTextMeshPro;
using UnityEngine;
using UnityEngine.SceneManagement;
public class RotateScreenToLandscape : MonoBehaviour
{
    [SerializeField] private ScreenOrientation screenOrientation = ScreenOrientation.Portrait;
    [SerializeField] private TextProOnACircle[] texts;  
    void Awake()
    {
        Screen.orientation = screenOrientation;

        if (SceneManager.GetActiveScene().name == "PuzzleMenu")
        {
            StartCoroutine(CircleTextRestart());
        }
    }

    private IEnumerator CircleTextRestart()
    {
        while (Screen.orientation != ScreenOrientation.LandscapeLeft)
        {
            yield return null;
        }

        foreach (var text in texts)
        {
            text.enabled = false;
        }

        yield return new WaitForSeconds(0.1f);
        foreach (var text in texts)
        {
            text.enabled = true;
        }
    }
}
