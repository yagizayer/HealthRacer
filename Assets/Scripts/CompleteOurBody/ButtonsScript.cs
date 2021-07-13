//Dogukan Kaan Bozkurt
//      github.com/dkbozkurt
//GEFEASOFT

/*
 * The script for the buttons and information windows.
 * add script into canvas and adjust the scenes and button's messages from the inspector.
 */

using System;   // To use Convert func. for converting int to string.
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class ButtonsScript : MonoBehaviour
{
    //For pause menu
    public static bool GameIsPaused = false;
    [SerializeField] private GameObject pauseMenuUI;
    [SerializeField] private GameObject pauseButton;

    //For info message
    [SerializeField] private GameObject infoMenuUI;
    [SerializeField] private GameObject infoButton;
    bool infoswitch = false;

    //For rotate button
    [SerializeField] private GameObject rotateButton;
    [SerializeField] private GameObject negrotateButton;

    //For hint button
    [SerializeField] private HintSystem hintSys;
    [SerializeField] private GameObject hintButton;
    [SerializeField] private GameObject hintTextObj;
    private TMP_Text hintText;
    private int hintCount = 2;
    [SerializeField] private SoundHandler sounds;
    

    private void Start()
    {
        hintText = hintTextObj.GetComponent<TMP_Text>();
        hintText.text = Convert.ToString(hintCount);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameIsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        pauseButton.SetActive(true);
                
        infoButton.SetActive(true);

        rotateButton.SetActive(true);
        negrotateButton.SetActive(true);

        hintButton.SetActive(true);
        //Line for play the game in normal speed.
        //Time.timeScale = 1f;
        GameIsPaused = false;
    }
    public void Pause()
    {
        pauseMenuUI.SetActive(true);
        pauseButton.SetActive(false);

        infoButton.SetActive(false);
        infoMenuUI.SetActive(false);
        infoswitch = false;

        rotateButton.SetActive(false);
        negrotateButton.SetActive(false);

        hintButton.SetActive(false);
        //Line for stop the game.
        //Time.timeScale = 0f;
        GameIsPaused = true;
    }

    public void LoadMenu()
    {
        SceneManager.LoadScene("CompleteOurBodyMenu");
    }

    //Info message functions
    public void InfoMessage()
    {
        if (!infoswitch)     //it helps us to make switch type of button.
        {
            infoMenuUI.SetActive(true);
            infoswitch = true;
        }
        else
        {
            infoMenuUI.SetActive(false);
            infoswitch = false;
        }     
    }

    //PlayAgain function for Game1
    public void PlayAgain1()
    {
        SceneManager.LoadScene("CompleteOurBodyGame");
    }

    //PlayAgain function for Game2
    public void PlayAgain2()
    {
        SceneManager.LoadScene("Game2");      
    }

    // Hint Button's function.
    public void HintSystem()
    {
        if (hintCount == 0)
        {
            sounds.PlayWrong();
        }
        else
        {
            hintCount--;
            hintText.text = Convert.ToString(hintCount);

            // Calls the expected hint box, depends on the organ.
            hintSys.redFrame();
        }
    }


    //We dont have quit button in our game so it is deactivated.
    /*
    public void QuitGame()
    {
        Application.Quit();
    }
    */
}
