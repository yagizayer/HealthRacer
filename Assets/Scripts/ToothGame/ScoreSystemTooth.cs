// Dogukan Kaan Bozkurt
//      github.com/dkbozkurt
// BeyazDisler

/*
 * Score system script of the game.
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ScoreSystemTooth : MonoBehaviour
{
    
    [SerializeField] private GameObject endText;

    private float totalScore;

    public TMP_Text endscreenText;

    void Start()
    {
        totalScore = 0;
        
    }

    public void setTotalScore(float score)
    {
        totalScore = totalScore + score;
        
    }

    public float getTotalScore()
    {
        return totalScore;
    }

    public void endMenuScore()
    {

        if (getTotalScore() < 75)
        {
            endscreenText.text = "Lekelerin % " + getTotalScore() + " kadarlnl temizledin. Tekrar dene.";
        }
        else
        {
            endscreenText.text = "Lekelerin % " + getTotalScore() + " kadarlnl temizledin. Tebrikler !!";
        }

    }

}
