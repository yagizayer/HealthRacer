using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class DragAndDrop_ : MonoBehaviour
{
    [SerializeField] private Sprite[] Levels;
    [SerializeField] private TMP_Text correctPieces;
    [SerializeField] private GameObject SelectedPiece;
    [SerializeField] private Camera myCam;
    [SerializeField] private GameObject GameUI;
    [SerializeField] private GameObject EndMenu;
    
    int OIL = 1;    
    public int PlacedPieces = 0;
    private int ScoreStars=0;
     
    void Start()
    {
        this.GetComponent<LevelTimer>().timerRunning=true;
        for (int i = 0;i < 36; i++)
        {
            GameObject.Find("Piece (" + i + ")").transform.Find("Puzzle").GetComponent<SpriteRenderer>().sprite = Levels[PlayerPrefs.GetInt("Level")];
        }
        
    }

    void Update()
    {
        correctPieces.text = PlacedPieces.ToString();
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
            if (hit)
            {
                if (hit.transform.CompareTag("Puzzle"))
                {
                    if (!hit.transform.GetComponent<piceseScript>().InRightPosition)
                    {
                        SelectedPiece = hit.transform.gameObject;
                        SelectedPiece.GetComponent<piceseScript>().Selected = true;
                        SelectedPiece.GetComponent<SortingGroup>().sortingOrder = OIL;
                        OIL++;
                    }
                }
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            if (SelectedPiece != null)
            {
                SelectedPiece.GetComponent<piceseScript>().Selected = false;
                SelectedPiece = null;
            }
        }
        if (SelectedPiece != null)
        {
            Vector3 MousePoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            SelectedPiece.transform.position = new Vector3(MousePoint.x,MousePoint.y,0);
        }             
        if (PlacedPieces == 36 & this.GetComponent<LevelTimer>().timerRunning)
        {
            this.GetComponent<LevelTimer>().timerRunning=false;
            myCam.GetComponent<Animator>().SetBool("Play", true);
            StartCoroutine(ActivateWholeEndView());
            //GameObject.FindGameObjectWithTag("MusicBtn").GetComponent<AudioSource>().PlayOneShot(GameObject.FindGameObjectWithTag("MusicBtn").GetComponent<GeneralController>().Sounds[3],1);

        }
    }
    public void NextLevel()
    {
        if(PlayerPrefs.GetInt("Level") != 3)
        PlayerPrefs.SetInt("Level", PlayerPrefs.GetInt("Level")+1);
        SceneManager.LoadScene("Jigsaw");
    }

    public void SameLevel()
    {
        SceneManager.LoadScene("Jigsaw");
    }
    public void BacktoMenu()
    {
        SceneManager.LoadScene("Menu");
    }

    IEnumerator  ActivateWholeEndView()
    {
        GameUI.SetActive(false);
        ScoreStars=gameObject.GetComponent<LevelTimer>().GetPoints();
        EndMenu.GetComponent<Image>().sprite = Levels[PlayerPrefs.GetInt("Level")];
        yield return new WaitForSeconds(1.5f);
        GameObject.FindGameObjectWithTag("MusicBtn").GetComponent<AudioSource>().PlayOneShot(GameObject.FindGameObjectWithTag("MusicBtn").GetComponent<GeneralController>().Sounds[3],1);
        EndMenu.SetActive(true);
        if (Levels[PlayerPrefs.GetInt("Level")].name == "puzzle3")
        {
            EndMenu.gameObject.transform.GetChild(0).GetComponent<TMP_Text>().text = "Tebrikler Tüm Yapbozları Tamamladın!";
            EndMenu.gameObject.transform.GetChild(1).gameObject.SetActive(false);
            EndMenu.gameObject.transform.GetChild(2).gameObject.transform.position = new Vector3(500,96,0);
            PlayerPrefs.SetInt("Level", PlayerPrefs.GetInt("Level")-2);
        }
        Debug.Log(ScoreStars);
    }
}