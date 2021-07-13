using System.Collections;
using System.Collections.Generic;
using DataHandling;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace UI
{
    public class UI_BlogLoader : MonoBehaviour
    {

        [SerializeField] private GameObject blogCardPrefab;
        [SerializeField] private Transform blogParent;
        [SerializeField] private Transform loadingIcon;
        [SerializeField] private GameObject connectionError;
        private UI_SetDynamicObjects _setDynamicObjects;

        private bool _dataFetched = false;
        private GameObject _dataHandler;

        private List<Post> _posts = new List<Post>();
        private int _currentPage = 0;

        private void Start()
        {
            _dataHandler = FindObjectOfType<BlogControl>().gameObject;
            if (BlogControl.InstanceExists)
            {
                StartCoroutine(RotateLoadingIcon());
                _dataHandler.GetComponent<BlogControl>().GetBlogPage(0, (value) =>
                {
                    if (value == null)
                    {
                        connectionError.SetActive(true);
                        return;
                    }
                    _posts = value;
                    DisplayData(_posts);
                    _dataFetched = true;
                });
            }
            else
            {
                loadingIcon.gameObject.SetActive(false);
                _dataHandler.GetComponent<BlogControl>().GetBlogPage(0, (value) =>
                {
                    if (value == null)
                    {
                        connectionError.SetActive(true);
                        return;
                    }
                    _posts = value;
                    DisplayData(_posts);
                    _dataFetched = true;
                });
            }
        }


        private IEnumerator RotateLoadingIcon()
        {
            while (!_dataFetched)
            {
                loadingIcon.GetComponent<RectTransform>().Rotate(new Vector3(0, 0, 2));

                yield return null;
            }
            loadingIcon.gameObject.SetActive(false);

        }
        public void LoadMorePost(Vector2 pos)
        {
            _dataHandler.GetComponent<BlogControl>().GetBlogPage(++_currentPage, (value) =>
            {
                if (value == null)
                {
                    connectionError.SetActive(true);
                    return;
                }

                _posts.AddRange(value);
                if (pos.y < 0)
                {
                    foreach (Post post in _posts)
                    {
                        GameObject temp = Instantiate(blogCardPrefab, blogParent, true);
                        temp.GetComponent<Button>().onClick.AddListener(() => { UI_Consts.LastClickedPost = post; SceneManager.LoadScene("BlogDetail"); });
                        temp.transform.localScale = Vector3.one;

                        // displaying post

                        RawImage image = temp.gameObject.GetComponentInChildren<Image>().gameObject.GetComponentInChildren<RawImage>();
                        Text title = temp.transform.Find("Baslik").transform.Find("Text").GetComponent<Text>();
                        Text text = temp.transform.Find("Onmetin").transform.Find("Text").GetComponent<Text>();

                        title.text = post.PostTitle;
                        text.text = post.PostDescription;
                        image.texture = post.MediaImage;
                    }
                    DisplayData(_posts);
                }
                _dataFetched = true;
            });
        }
        private void DisplayData(List<Post> posts)
        {
            foreach (Post post in posts)
            {
                GameObject temp = Instantiate(blogCardPrefab, blogParent, true);
                temp.GetComponent<Button>().onClick.AddListener(() => { UI_Consts.LastClickedPost = post; SceneManager.LoadScene("BlogDetail"); });
                temp.transform.localScale = Vector3.one;

                // Displaying post

                RawImage image = temp.gameObject.GetComponentInChildren<Image>().gameObject.GetComponentInChildren<RawImage>();
                Text title = temp.transform.Find("Baslik").transform.Find("Text").GetComponent<Text>();
                Text text = temp.transform.Find("Onmetin").transform.Find("Text").GetComponent<Text>();

                title.text = post.PostTitle;
                text.text = post.PostDescription;
                image.texture = post.MediaImage;
            }
            _setDynamicObjects = FindObjectOfType<UI_SetDynamicObjects>();
            _setDynamicObjects.FillLists();
        }
    }
}
