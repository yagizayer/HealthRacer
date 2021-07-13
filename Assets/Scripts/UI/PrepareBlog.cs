using System.Collections;
using System.Collections.Generic;
using DataHandling;
using UnityEngine;
using UnityEngine.UI;

public class PrepareBlog : MonoBehaviour
{
    [SerializeField] private RawImage imageDisplay;
    [SerializeField] private Text titleDisplay;
    [SerializeField] private Text blogTextDisplay;
    [SerializeField] private Post currentPost;


    public void Start()
    {
        currentPost = UI_Consts.LastClickedPost;

        imageDisplay.texture = currentPost.MediaImage;

        titleDisplay.text = currentPost.PostTitle;
        blogTextDisplay.text = currentPost.PostDescription;

    }
}
