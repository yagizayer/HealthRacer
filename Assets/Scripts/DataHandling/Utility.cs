using UnityEngine;

namespace DataHandling
{
    public static class Utility
    {
        public static Texture2D ByteToTexture2D(byte[] data)
        {
            Texture2D texture = new Texture2D(1, 1, TextureFormat.RGB24, false)
            {
                filterMode = FilterMode.Trilinear
            };
            texture.LoadImage(data);

            return texture;
        }

    }
}