using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

namespace DataHandling.Networking
{
    public static class Requests
    {
        /// <summary>
        /// Send GET request to get string data.
        /// </summary>
        /// <param name="url">URL</param>
        /// <param name="callback">Callback(string)</param>
        public static IEnumerator SendGetRequest(string url, System.Action<string> callback)
        {
            using var www = UnityWebRequest.Get(url);
            yield return www.SendWebRequest();

            if (www.result == UnityWebRequest.Result.ConnectionError)
            {
                // toDO: Error handling system will be added.
                Debug.Log(www.error);
            }
            else
            {
                if (www.isDone)
                {
                    var result = System.Text.Encoding.UTF8.GetString(www.downloadHandler.data);
                    
                    callback(result);
                }
                else
                {
                    // toDO: Error handling system will be added.
                    Debug.Log("Error! data couldn't get.");
                }
            }
        }
        
        /// <summary>
        /// Send GET request to download any file.
        /// </summary>
        /// <param name="url">URL</param>
        /// <param name="callback">Callback(file)</param>
        public static IEnumerator SendGetRequestFile(string url, System.Action<byte[]> callback)
        {
            var www = UnityWebRequestTexture.GetTexture(url);
            yield return www.SendWebRequest();

            if (www.result == UnityWebRequest.Result.ConnectionError || 
                www.result == UnityWebRequest.Result.ProtocolError)
            {
                // toDO: Error handling system will be added.
                Debug.Log(www.error);
            }
            else
            {
                callback(www.downloadHandler.data);
            }
        }
    }
}
