using System;
using System.Collections.Generic;
using DataHandling.Networking;
using UnityEngine;
using UnityEngine.Serialization;

namespace DataHandling
{
    public class BlogControl : MonoBehaviour
    {
        [FormerlySerializedAs("_blog")] [SerializeField]
        private Blog blog;

        private List<List<Post>> _blogPages = new List<List<Post>>();

        private bool _isFetched = false;

        #region Blog Posts
        private void GetAllBlogPosts()
        {
            StartCoroutine(Requests.SendGetRequest(BlogConstants.BlogPostsUrl, (el) =>
            {
                blog = JsonUtility.FromJson<Blog>(el);
                CreateBlogPages();
                DownloadMediaForPage(_blogPages[0]);
            }));
        }
        private void CreateBlogPages()
        {
            var pages = new List<List<Post>>();
            var page = new List<Post>();
            var counter = 1;

            foreach (var post in blog.BlogPosts)
            {
                if (counter > BlogConstants.PagePostAmount)
                {
                    pages.Add(page);
                    page = null;
                    counter = 1;
                }

                if (page == null) continue;
                page.Add(post);
                counter++;

                if (post == blog.BlogPosts[blog.BlogPosts.Length - 1])
                {
                    pages.Add(page);
                }
            }
            _blogPages = pages;
        }

        public void GetBlogPage(int pageIndex, Action<List<Post>> callback)
        {
            if (Application.internetReachability == NetworkReachability.NotReachable)
            {
                callback(null);
            }
            else
            {
                if (_blogPages.Count - 1 > pageIndex)
                {
                    DownloadMediaForPage(_blogPages[pageIndex + 1]);
                }
                
                callback(_blogPages[pageIndex]);
            }
        }

        #endregion

        #region Media

        
        private void DownloadMediaForPage(List<Post> page)
        {
            foreach (var post in page)
            {
                switch (post.MediaType)
                {
                    case (int)MediaType.Image when post.MediaImage == null:
                    {
                        string url = $"{BlogConstants.BlogUrl}{post.MediaPath.Replace("..", "")}";

                        StartCoroutine(Requests.SendGetRequestFile(url, (data) =>
                        {
                            post.MediaImage = Utility.ByteToTexture2D(data);

                        }));
                        break;
                    }
                    
                    // toDO: When video attribute is added to Post class, it will be checked for null or not.
                    case (int)MediaType.Video:
                        // toDO: Video type support will be added.
                        break;
                }
            }
        }
        #endregion
        
        public static bool InstanceExists = false;
        private void Awake()
        {
            if (!InstanceExists)
            {
                InstanceExists = true;
                DontDestroyOnLoad(gameObject);
            }

            if (Application.internetReachability != NetworkReachability.NotReachable)
            {
                GetAllBlogPosts();
            }
        
        }

        private void FixedUpdate()
        {
            if (_isFetched) return;
            if (Application.internetReachability == NetworkReachability.NotReachable) return;
            
            _isFetched = true;
            GetAllBlogPosts();
        }
    }
}
