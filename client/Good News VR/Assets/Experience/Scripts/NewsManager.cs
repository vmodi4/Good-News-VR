using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using TMPro;

[System.Serializable]
public class NewsResponse
{
    public string headline;
    public string more_info_1;
    public string more_info_2;
    public string more_info_3;
}

// unemotive-idella-cotemporarily.ngrok-free.dev
// test.sneakycloud.xyz

public class NewsManager : MonoBehaviour
{
    [Header("Assign your TMP panels here")]
    public TextMeshProUGUI[] newsPanels; // 4 panels for each JSON field

    [Header("API Settings")]
    public string apiUrl = "https://test.sneakycloud.xyz"; // replace with your endpoint

    public experienceManager em;
    public NewsResponse nr;

    void Start()
    {
        // StartCoroutine(FetchNews());
    }

    IEnumerator FetchNews()
    {
        using (UnityWebRequest request = UnityWebRequest.Get(apiUrl))
        {
            yield return request.SendWebRequest();

            if (request.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError("Error fetching news: " + request.error);
            }
            else
            {
                Debug.Log("Received response: " + request.downloadHandler.text);
                string json = request.downloadHandler.text;

                // Remove markdown code block ticks if needed
                json = json.Replace("```json", "").Replace("```", "").Trim();

                try
                {
                    NewsResponse response = JsonUtility.FromJson<NewsResponse>(json);
                    em.nr = response;
                    nr = response;
                }
                catch (System.Exception e)
                {
                    Debug.LogError("Failed to parse JSON: " + e.Message + "\nJSON: " + json);
                }
            }
        }
    }
}