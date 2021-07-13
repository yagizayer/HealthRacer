using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GetCurrentPuzzle : MonoBehaviour
{
    private int currentLevel;
    [SerializeField] private Sprite[] puzzles;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        currentLevel = PlayerPrefs.GetInt("Level");
        gameObject.GetComponent<Image>().sprite = puzzles[currentLevel];
    }
}
