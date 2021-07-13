// Dogukan Kaan Bozkurt
//      github.com/dkbozkurt
// BeyazDisler

 /*
  * Audio's of the game is controled from this script.
  */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioController : MonoBehaviour
{
    #region Audios
    [SerializeField] private AudioSource brushing=null;
    [SerializeField] private AudioSource clicked=null;
    //[SerializeField] private GameObject backgroundAudio=null;
    #endregion

    #region Mute/unMute
    [SerializeField] private Image soundOnIcon=null;
    [SerializeField] private Image soundOffIcon=null;
    private bool muted = false;
    #endregion

    // Necessary function for continuing background music between scenes
    /*
    private void Awake()
    {
        DontDestroyOnLoad(backgroundAudio.transform);
    }
    */
    private void Start()
    {
        if (PlayerPrefs.HasKey("muted"))
        {
            PlayerPrefs.SetInt("muted", 0);
            Load();
        }
        else
        {
            Load();
        }

        UpdateButtonIcon();
        AudioListener.pause = muted;
    }

    public void Brushing()
    {
        brushing.Play();
    }
        
    public void Click()
    {
        clicked.Play();
    }

    public void OnButtonPress()
    {
        if(!muted)
        {
            muted = true;
            AudioListener.pause = true;
        }
        else
        {
            muted = false;
            AudioListener.pause = false;
        }
        Save();
        UpdateButtonIcon();
    }

    private void UpdateButtonIcon()
    {
        if(!muted)
        {
            soundOnIcon.enabled = true;
            soundOffIcon.enabled = false;

        }
        else
        {
            //soundOnIcon.enabled = false;
            soundOffIcon.enabled = true;

        }
    }

    private void Load()
    {
        muted = PlayerPrefs.GetInt("muted") == 1;
    }

    private void Save()
    {
        PlayerPrefs.SetInt("muted", muted ? 1 : 0);
    }




}
