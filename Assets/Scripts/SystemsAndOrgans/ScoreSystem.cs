//Dogukan Kaan Bozkurt
//      github.com/dkbozkurt
//GEFEASOFT

/* Game Over screen. Depends on the points that collected from the game.
 * Result will be shown depends on the score. 
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement; //For TextMeshPro

public class ScoreSystem : MonoBehaviour
{ 
    //Score Text
    public TMP_Text scoreText; 
    
    private int player_score;
    private int numOfOrgans;

    //For GameOver Screen
    [SerializeField] private GameObject gameEndMenu;
    [SerializeField] private TMP_Text gemText;

    [SerializeField] private GameObject rotateButton;
    [SerializeField] private GameObject negRotateButton;
    [SerializeField] private GameObject humanBody;
    [SerializeField] private GameObject organSlots;

    public int getScore() { return player_score; }
    public void setScore(int score) { 

        player_score += score;

        //Function block score to go below zero.
        if (player_score < 0)
        {
            player_score = 0;
        }
    }

    public int getnumOfOrgans() { return numOfOrgans; }
    public void setnumOfOrgans(int numoo) { numOfOrgans += numoo; }  

    private void Start()
    {
        numOfOrgans = 0;
        player_score = 0;
        scoreText.text = "SKOR : " + player_score;
    }

    //GameEndScreen will pop up when game is ended.
    public void GameEndScreen()
    {
        negRotateButton.SetActive(false);
        rotateButton.SetActive(false);
        humanBody.SetActive(false);

        if (SceneManager.GetActiveScene().name == "CompleteOurBodyGame")
        {
            var childCountOrganSlots = organSlots.transform.childCount;
            for (int ind = 0; ind < childCountOrganSlots; ind++)
            {
                organSlots.transform.GetChild(ind).gameObject.SetActive(false);
            }
        }
       

        gameEndMenu.SetActive(true);
        if (getScore() >= 1800)
        {
            gemText.text = "HARİKA!!!\n\nHİÇ HATA YAPMADAN OYUNU TAMAMLADIN. ORGANLARIMIZI VE SİSTEMLERİ ÇOK İYİ TANIYORSUN.\nSKORUN: " + player_score;
        }
        else if (getScore() >= 1600)
        {
            gemText.text = "TEBRİKLER!!\n\nORGANLARA VE SİSTEMLERE HAKİMSİN. GAYET BAŞARILISIN.\nSKORUN: " + player_score;
        }
        else if(getScore() >= 1300)
        {
            gemText.text = "GAYET İYİ!\n\nORGANLAR VE SİSTEMLERİNİ YAVAŞ YAVAŞ ÇÖZÜYORSUN. PRATİK YAPMAYA DEVAM ET!\nSKORUN: " + player_score;
        }
        else if (getScore() >= 900)
        {
            gemText.text = "İDARE EDER\n\nORGANLAR VE SİSTEMLERINE DAHA SIKI ÇALIŞMALISIN.\nSKORUN: " + player_score;
        }
        else
        {
            gemText.text = "BAŞARISIZ OLDUN!\n\nTEKRAR OYNAYARAK KENDİNİ GELİŞTİRMEYİ DENE.\nSKORUN: " + player_score;
        }

    }

}
