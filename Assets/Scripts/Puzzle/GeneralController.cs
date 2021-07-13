using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GeneralController : MonoBehaviour
{
    [SerializeField] private Sprite[] volumeSprites;
    public  AudioClip[] Sounds;
    private AudioSource _audioSource;
    
    private void Awake()
    {
        DontDestroyOnLoad(transform.gameObject);
        _audioSource = GetComponent<AudioSource>();
    }

    void Start()
    {
           
        // If there is no entry for isFirstTime means it is first time or if there is entry and it is not one means it is first time
        if(!PlayerPrefs.HasKey("isFirstTime") || PlayerPrefs.GetInt("isFirstTime") == 1)
        {
            // Set and save all your PlayerPrefs here.
            PlayerPrefs.DeleteKey("Level");
            // Now set the value of isFirstTime to be false in the PlayerPrefs.
            PlayerPrefs.SetInt("isFirstTime", 1);
            PlayerPrefs.Save();
        }
    }

    void Update()
    {
        if(Input.GetMouseButtonDown(0)) _audioSource.PlayOneShot(Sounds[0],1);
        
        if(GameObject.FindGameObjectWithTag("Music").GetComponent<MusicClass>()._audioSource.isPlaying) 
            gameObject.GetComponent<Button>().image.sprite = volumeSprites[0];
        else if(!GameObject.FindGameObjectWithTag("Music").GetComponent<MusicClass>()._audioSource.isPlaying)
            gameObject.GetComponent<Button>().image.sprite = volumeSprites[1];
    }

 
    public void ChangeVolumeStatus()
    {
        if (gameObject.GetComponent<Button>().image.sprite == volumeSprites[1])
        {
            gameObject.GetComponent<Button>().image.sprite = volumeSprites[0];     
            GameObject.FindGameObjectWithTag("Music").GetComponent<MusicClass>().UnpouseMusic();

        }
        else if (gameObject.GetComponent<Button>().image.sprite == volumeSprites[0])
        {
            gameObject.GetComponent<Button>().image.sprite = volumeSprites[1];  
            GameObject.FindGameObjectWithTag("Music").GetComponent<MusicClass>().PauseMusic();
        }
    }

}