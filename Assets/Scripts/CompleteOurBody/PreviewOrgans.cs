using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PreviewOrgans : MonoBehaviour
{
    [SerializeField] private CanvasGroup[] organs;

    public void ShowOrgans(string organTag)
    {
        //foreach (var organ in organs)
        //{
        //    organ.alpha = 1;
        //}

        var alphaOrgan = Array.Find(organs, el => el.gameObject.CompareTag(organTag));
        alphaOrgan.alpha = 1;
    }

    public void HideOrgans()
    {
        foreach (var organ in organs)
        {
            organ.alpha = 0;
        }
    }
}
