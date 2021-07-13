// Dogukan Kaan Bozkurt
//      github.com/dkbozkurt
// BeyazDisler

/*
 * Button Controller
 */


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ButtonController : MonoBehaviour
{
    #region Buttons
    [SerializeField] private GameObject CamC;
    [SerializeField] private GameObject CamL;
    [SerializeField] private GameObject CamR;

    [SerializeField] private GameObject brushButton;
    [SerializeField] private GameObject endButton;
    #endregion

    #region Menus
    [SerializeField] private GameObject startMenu=null;
    #endregion
    #region Scripts
    [SerializeField] private AnimationController animControl;
    [SerializeField] private ToothBrush tBrush;
    [SerializeField] private ScoreSystemTooth scoreSys;

    #endregion
    #region GameObjects 
    [SerializeField] private GameObject leftConcealers;
    [SerializeField] private GameObject rightConcealers;
    [SerializeField] private GameObject stains;


    #endregion
    #region Variables
    bool pausebuttonpressed;
    bool brushinscene;
    
    #endregion
    private void Awake()
    {
        pausebuttonpressed = false;
        
    }
    private void Start()
    {
        startMenu.SetActive(true);
        
        brushButton.SetActive(false);
        endButton.SetActive(false);

        CamC.SetActive(false);
        CamL.SetActive(false);
        CamR.SetActive(false);
        leftConcealers.SetActive(false);
        rightConcealers.SetActive(false);

        stains.SetActive(false);

        animControl.EndMenuOnOff(false);


    }
    public void Play()
    {
        SceneManager.LoadScene("ToothGame");

    }

    public void Menu()
    {
        SceneManager.LoadScene("ToothMenu");
    }

    public void PauseResume()
    {
        if (!pausebuttonpressed)
        {
            animControl.PauseMenuOnOff(true);
            pausebuttonpressed = true;

        }
        else if(pausebuttonpressed)
        {
            animControl.PauseMenuOnOff(false);
            pausebuttonpressed = false;
        }
        
    }

    public void BrushInOut()
    {
        if(!brushinscene)
        {
            animControl.BrushInOutControl(true);
            brushinscene = true;
        }
        else if (brushinscene)
        {
            animControl.BrushInOutControl(false);
            brushinscene = false;
        }

    }

    public void StartTheGame()
    {
        animControl.StartMenuOff();
        animControl.BoyIn();

        // --Wait for sec--.
        StartCoroutine(StartTimeDelay());
        
    }

    public void RestartGame()
    {
        SceneManager.LoadScene("ToothGame");
    }

    public void EndTheGameButton()
    {
        animControl.CameraMove(0);
        animControl.MounthOpen(false);

        CamC.SetActive(false);
        CamL.SetActive(false);
        CamR.SetActive(false);

        leftConcealers.SetActive(false);
        rightConcealers.SetActive(false);

        brushButton.SetActive(false);
        endButton.SetActive(false);

        brushinscene = true;
        BrushInOut();

        animControl.EndMenuOnOff(true);

        stains.SetActive(false);

        scoreSys.endMenuScore();

    }

    public void CameraCenter()
    {
        animControl.CameraMove(0);

        CamC.SetActive(false);
        CamL.SetActive(true);
        CamR.SetActive(true);

        leftConcealers.SetActive(false);
        rightConcealers.SetActive(false);
        // Hides the brush
        brushinscene = true;
        BrushInOut();
        
    }

    public void CameraLeft()
    {
        animControl.CameraMove(1);
        CamC.transform.position = CamR.transform.position;

        CamC.SetActive(true);
        CamL.SetActive(false);
        CamR.SetActive(false);
        leftConcealers.SetActive(true);

        // Hides the brush
        brushinscene = true;
        BrushInOut();
        
    }

    public void CameraRight()
    {
        animControl.CameraMove(2);
        CamC.transform.position = CamL.transform.position;

        CamC.SetActive(true);
        CamL.SetActive(false);
        CamR.SetActive(false);
        rightConcealers.SetActive(true);

        // Hides the brush
        brushinscene = true;
        BrushInOut();       

    }

    // Basic time delay with operations
    IEnumerator StartTimeDelay()
    {
        yield return new WaitForSeconds(2.7f);
        stains.SetActive(true);

        brushButton.SetActive(true);
        endButton.SetActive(true);

        CamL.SetActive(true);
        CamR.SetActive(true);

    }
}
