using UnityEngine;

public class NewsManager : MonoBehaviour
{
    public string apiUrl = "https://localhost:8000/";

    private async void Start()
    {
        NewsItem[] newsArray = await NewsService.GetNewsFromAPI(apiUrl);

        if (newsArray != null)
        {
            foreach (var news in newsArray)
            {
                Debug.Log($"📰 {news.headline}");
                Debug.Log($" - {news.fact1}");
            }
        }
    }
}

