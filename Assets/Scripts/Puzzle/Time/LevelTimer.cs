using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LevelTimer : MonoBehaviour {
     
    // set it to true when gameplay has started, to false when level finished or game paused
    public bool timerRunning = false;
    public void SetTimerRunning (bool value) { timerRunning = value; }
    [SerializeField] private List<int> timeLimits = new List<int>(); // list of time limits in increasing order, for example 0, 20, 40
    [SerializeField] private TMP_Text ClockTime;

     
    [SerializeField] float remainingTime = 150;
     
    private void Update () {
        if (timerRunning) {
            remainingTime -= Time.deltaTime;
            ClockTime.text = Mathf.RoundToInt(remainingTime).ToString();
        }
    }
    // call from anywhere you want to know how many stars remain. Mathf.Clamp() used to not go below 0 or above 3
    // +1 added to form the following pattern: 40-60 seconds = 3 stars, 20-40 seconds = 2 stars, 0-20 seconds 1 star, 0 stars otherwise
   
    public int GetPoints () {
        for (int i = 0; i < timeLimits.Count; ++i) {
            if (remainingTime < timeLimits[i]) { return i; }
        }
        return timeLimits.Count;
    }
     
}
