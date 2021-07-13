using UnityEngine;

namespace DataHandling
{
    [System.Serializable]
    public class Blog
    {
        [SerializeField]
        private Post[] blogposts;
        public Post[] BlogPosts { get { return blogposts; } }
    }

    [System.Serializable]
    public class Post
    {
        [SerializeField]
        private int post_id;
        public int PostId { get { return post_id; } }

        [SerializeField]
        private string media_path;
        public string MediaPath { get { return media_path; } }

        [SerializeField]
        private int media_type;
        public int MediaType { get { return media_type; } }

        [SerializeField]
        private Texture media_image;
        public Texture MediaImage { get { return media_image; } set { media_image = value; } }

        [SerializeField]
        private string post_title;
        public string PostTitle { get { return post_title; } }

        [SerializeField]
        private string post_author;
        public string PostAuthor { get { return post_author; } }

        [SerializeField]
        private string created_at;
        public string CreatedAt { get { return created_at; } }

        [SerializeField]
        private string post_description;
        public string PostDescription { get { return post_description; } }
    }
}