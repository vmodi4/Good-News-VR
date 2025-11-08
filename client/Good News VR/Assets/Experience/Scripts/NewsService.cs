using System.Diagnostics;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;

/// <summary>
/// Handles loading an array of NewsItem objects from an API endpoint.
/// </summary>
public static class NewsService
{
    /// <summary>
    /// Fetches an array of NewsItem objects from a JSON API endpoint.
    /// </summary>
    /// <param name="apiUrl">The URL returning a JSON array of news items.</param>
    public static async Task<NewsItem[]> GetNewsFromAPI(string apiUrl)
    {
        UnityEngine.Debug.Log("I called the function!!!!!!");
        using (UnityWebRequest request = UnityWebRequest.Get(apiUrl))
        {
            request.downloadHandler = new DownloadHandlerBuffer();
            var operation = request.SendWebRequest();

            // Wait until request completes
            while (!operation.isDone)
                await Task.Yield();

            if (request.result != UnityWebRequest.Result.Success)
            {
                UnityEngine.Debug.LogError($"Error fetching news: {request.error}");
                return null;
            }

            string json = request.downloadHandler.text;
            UnityEngine.Debug.Log(json);

            try
            {
                // Wrap the array if needed for Unity's JsonUtility
                // Unity can't directly deserialize a raw array,
                // so your API can either return a wrapper object, or we can add one:
                NewsItemArrayWrapper wrapper = JsonUtility.FromJson<NewsItemArrayWrapper>(
                    json
                );
                return wrapper.good_news;
            }
            catch (System.Exception ex)
            {
                UnityEngine.Debug.LogError($"Failed to parse news JSON: {ex.Message}");
                return null;
            }
        }
    }

    // Helper wrapper for Unity's JsonUtility
    [System.Serializable]
    private class NewsItemArrayWrapper
    {
        public NewsItem[] good_news;
    }
}

