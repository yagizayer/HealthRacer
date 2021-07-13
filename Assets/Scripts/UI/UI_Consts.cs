using System.Collections;
using System.Collections.Generic;
using DataHandling;
using UnityEngine;

public static class UI_Consts
{
    public static Post LastClickedPost;

    private static Sprite backgroundImage = Resources.Load<Sprite>("UI/mobileBG");
    public static Sprite BackgroundImage
    {
        get { return backgroundImage; }
        set
        {
            backgroundImage = value;
        }
    }
    private static Color cardBgColor = new Color(255f / 255, 255f / 255, 255f / 255); // default is white
    public static Color CardBgColor
    {
        get { return cardBgColor; }
        set
        {
            cardBgColor = value;
        }
    }
    private static Color buttonColor = new Color(40f / 255, 221f / 255, 150f / 255); // default is green
    public static Color ButtonColor
    {
        get { return buttonColor; }
        set
        {
            buttonColor = value;
        }
    }
    private static Color exitButtonColor = new Color(255f / 255, 93f / 255, 91f / 255); // default is red
    public static Color ExitButtonColor
    {
        get { return exitButtonColor; }
        set
        {
            exitButtonColor = value;
        }
    }
    private static Color buttonIconColor = new Color(255f / 255, 255f / 255, 255f / 255); // default is white;
    public static Color ButtonIconColor
    {
        get { return buttonIconColor; }
        set
        {
            buttonIconColor = value;
        }
    }
    private static float buttonTextFontSize = 100;
    public static float ButtonTextFontSize
    {
        get { return buttonTextFontSize; }
        set
        {
            buttonTextFontSize = value;
        }
    }
    private static float titleFontSize = 70;
    public static float TitleFontSize
    {
        get { return titleFontSize; }
        set
        {
            titleFontSize = value;
        }
    }
    private static Color titleColor = new Color(50f / 255, 50f / 255, 50f / 255);// default is dark gray;
    public static Color TitleColor
    {
        get { return titleColor; }
        set
        {
            titleColor = value;
        }
    }
    private static float textFontSize = 50;
    public static float TextFontSize
    {
        get { return textFontSize; }
        set
        {
            textFontSize = value;
        }
    }
    private static Color textColor = new Color(50f / 255, 50f / 255, 50f / 255);// default is dark gray;
    public static Color TextColor
    {
        get { return textColor; }
        set
        {
            textColor = value;
        }
    }

    public static Dictionary<string, string> tags = new Dictionary<string, string>(){
        {"background","UI_Background"},
        {"button","UI_Button"},
        {"exitButton","UI_ExitButton"},
        {"icon","UI_Icon"},
        {"text","UI_Text"},
        {"title","UI_Title"},
        {"card","UI_Card"},
    };






}
