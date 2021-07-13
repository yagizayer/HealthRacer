using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using TMPro;

public class TextWrite : MonoBehaviour
{
    public TMP_Text uiText;
    public TextAsset textToWrite;
    public int characterIndex = 0;
    public float timePerCharacter;
    public float timer;
    private bool invisibleCharacters;
    public Action onComplete;

    private void Update()
    {
        if(textToWrite != null)
        {
            timer -= Time.deltaTime;
            while (timer <= 0f)
            {
                if (characterIndex >= textToWrite.text.Length)
                {
                    return;
                }
                timer += timePerCharacter;
                characterIndex++;
                string text = textToWrite.text.Substring(0, characterIndex);
                if (invisibleCharacters)
                {
                    text += "<color=#00000000>" + textToWrite.text.Substring(characterIndex) + "</color>";
                }
                uiText.text = text;
            }
        }
    }
}
