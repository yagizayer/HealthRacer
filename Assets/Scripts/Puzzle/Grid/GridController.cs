using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GridController : MonoBehaviour
{
    int placedPuzzle = 0;
    int totalPuzzle = 9;
    
    public GameObject CongratPanel;
    // Start is called before the first frame update
    void Start()
    {
        GameObject.FindGameObjectWithTag("Music").GetComponent<MusicClass>().PlayMusic();
        this.gameObject.GetComponent<ParticleSystem>().Stop();
        CongratPanel.SetActive(false);
    }

    public void increaseNumber()
    {
        placedPuzzle++;
        if(placedPuzzle == totalPuzzle)
        {
            GameObject.FindGameObjectWithTag("MusicBtn").GetComponent<AudioSource>().PlayOneShot(GameObject.FindGameObjectWithTag("MusicBtn").GetComponent<GeneralController>().Sounds[3],1);
            CongratPanel.SetActive(true);
            this.gameObject.GetComponent<ParticleSystem>().Play();
        }
    }
    public void OnClickCongratButton()
    {
        SceneManager.LoadScene("PuzzleMenu");
    }
}
