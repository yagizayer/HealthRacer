using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class UI_SetDynamicObjects : MonoBehaviour
{
    static private bool instanceExists;

    [SerializeField] private List<GameObject> Backgrounds;
    [SerializeField] private List<GameObject> Cards;
    [SerializeField] private List<GameObject> Buttons;
    [SerializeField] private List<GameObject> ExitButtons;
    [SerializeField] private List<GameObject> Titles;
    [SerializeField] private List<GameObject> Texts;
    [SerializeField] private List<GameObject> Icons;
    [SerializeField] private List<GameObject> ButtonTexts;
    static bool InstanceActive = false;
    private void Awake()
    {
        if (!InstanceActive)
        {
            InstanceActive = true;
            SceneManager.sceneLoaded += FillLists;
        }
    }
    void Start()
    {
        if (!instanceExists)
        {
            instanceExists = true;
            DontDestroyOnLoad(this.gameObject);
        }
    }
    void ResetLists()
    {
        Backgrounds.Clear();
        Cards.Clear();
        ExitButtons.Clear();
        Buttons.Clear();
        Titles.Clear();
        Texts.Clear();
        Icons.Clear();
        ButtonTexts.Clear();
    }

    void FillLists(Scene scene, LoadSceneMode mode)
    {
        ResetLists();
        foreach (GameObject item in GameObject.FindGameObjectsWithTag(UI_Consts.tags["background"]))
            if (!Backgrounds.Contains(item))
                Backgrounds.Add(item);
        foreach (GameObject item in GameObject.FindGameObjectsWithTag(UI_Consts.tags["card"]))
            if (!Cards.Contains(item))
                Cards.Add(item);
        foreach (GameObject item in GameObject.FindGameObjectsWithTag(UI_Consts.tags["button"]))
            if (!Buttons.Contains(item))
                Buttons.Add(item);
        foreach (GameObject item in GameObject.FindGameObjectsWithTag(UI_Consts.tags["exitButton"]))
            if (!ExitButtons.Contains(item))
                ExitButtons.Add(item);
        foreach (GameObject item in GameObject.FindGameObjectsWithTag(UI_Consts.tags["text"]))
            if (!Texts.Contains(item))
                Texts.Add(item);
        foreach (GameObject item in GameObject.FindGameObjectsWithTag(UI_Consts.tags["icon"]))
            if (!Icons.Contains(item))
                Icons.Add(item);
        foreach (GameObject item in GameObject.FindGameObjectsWithTag(UI_Consts.tags["text"]))
            if (!ButtonTexts.Contains(item) && item.transform.parent.CompareTag(UI_Consts.tags["button"]))
                ButtonTexts.Add(item);
        SetObjects();
    }
    public void FillLists()
    {
        ResetLists();
        foreach (GameObject item in GameObject.FindGameObjectsWithTag(UI_Consts.tags["background"]))
            if (!Backgrounds.Contains(item))
                Backgrounds.Add(item);
        foreach (GameObject item in GameObject.FindGameObjectsWithTag(UI_Consts.tags["card"]))
            if (!Cards.Contains(item))
                Cards.Add(item);
        foreach (GameObject item in GameObject.FindGameObjectsWithTag(UI_Consts.tags["button"]))
            if (!Buttons.Contains(item))
                Buttons.Add(item);
        foreach (GameObject item in GameObject.FindGameObjectsWithTag(UI_Consts.tags["exitButton"]))
            if (!ExitButtons.Contains(item))
                ExitButtons.Add(item);
        foreach (GameObject item in GameObject.FindGameObjectsWithTag(UI_Consts.tags["title"]))
            if (!Titles.Contains(item))
                Titles.Add(item);
        foreach (GameObject item in GameObject.FindGameObjectsWithTag(UI_Consts.tags["text"]))
            if (!Texts.Contains(item))
                Texts.Add(item);
        foreach (GameObject item in GameObject.FindGameObjectsWithTag(UI_Consts.tags["icon"]))
            if (!Icons.Contains(item))
                Icons.Add(item);
        foreach (GameObject item in GameObject.FindGameObjectsWithTag(UI_Consts.tags["text"]))
            if (!ButtonTexts.Contains(item) && item.transform.parent.CompareTag(UI_Consts.tags["button"]))
                ButtonTexts.Add(item);
        SetObjects();
    }

    void SetObjects()
    {
        foreach (GameObject item in Backgrounds)
        {
            item.GetComponent<Image>().sprite = UI_Consts.BackgroundImage;
        }
        foreach (GameObject item in Cards)
        {
            item.GetComponent<Image>().color = UI_Consts.CardBgColor;
        }
        foreach (GameObject item in Buttons)
        {
            item.GetComponent<Image>().color = UI_Consts.ButtonColor;
        }
        foreach (GameObject item in ExitButtons)
        {
            item.GetComponent<Image>().color = UI_Consts.ExitButtonColor;
        }
        foreach (GameObject item in Icons)
        {
            item.GetComponent<Image>().color = UI_Consts.ButtonIconColor;
        }
        foreach (GameObject item in Titles)
        {
            item.GetComponent<Text>().fontSize = (int)UI_Consts.TitleFontSize;
            item.GetComponent<Text>().color = UI_Consts.TitleColor;
        }
        foreach (GameObject item in Texts)
        {
            Text tempText = item.GetComponent<Text>();
            TextMeshPro tempTMP = item.GetComponent<TextMeshPro>();
            if (tempText != null)
            {
                tempText.fontSize = (int)UI_Consts.TextFontSize;
                tempText.color = UI_Consts.TextColor;
            }
            else if (tempTMP != null)
            {
                tempText.fontSize = (int)UI_Consts.TextFontSize;
                tempText.color = UI_Consts.TextColor;
            }
        }
        foreach (GameObject item in ButtonTexts)
        {
            Text tempText = item.GetComponent<Text>();
            TextMeshPro tempTMP = item.GetComponent<TextMeshPro>();
            if (tempText != null)
            {
                tempText.fontSize = (int)UI_Consts.ButtonTextFontSize;
                tempText.color = UI_Consts.ButtonIconColor;
            }
            else if (tempTMP != null)
            {
                tempText.fontSize = (int)UI_Consts.ButtonTextFontSize;
                tempText.color = UI_Consts.ButtonIconColor;
            }
        }
    }

}
