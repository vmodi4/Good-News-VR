using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

public static class AudioHelper
{
    /// <summary>
    /// Fetches an MP3 file from the API and converts it into an AudioClip.
    /// </summary>
    /// <param name="monoBehaviour">The MonoBehaviour to run the coroutine on.</param>
    /// <param name="audioId">The unique ID of the audio resource.</param>
    /// <param name="onLoaded">Callback invoked with the loaded AudioClip.</param>
    /// <param name="onError">Optional callback for handling errors.</param>
    public static void FetchAudioClip(MonoBehaviour monoBehaviour, string audioId, System.Action<AudioClip> onLoaded, System.Action<string> onError = null)
    {
        monoBehaviour.StartCoroutine(FetchAudioClipCoroutine(audioId, onLoaded, onError));
    }

    private static IEnumerator FetchAudioClipCoroutine(string audioId, System.Action<AudioClip> onLoaded, System.Action<string> onError)
    {
        string url = $"https://localhost:8000/audio/{audioId}"; // Replace with your actual endpoint

        using (UnityWebRequest www = UnityWebRequestMultimedia.GetAudioClip(url, AudioType.MPEG))
        {
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError($"Failed to load audio: {www.error}");
                onError?.Invoke(www.error);
            }
            else
            {
                AudioClip clip = DownloadHandlerAudioClip.GetContent(www);
                onLoaded?.Invoke(clip);
            }
        }
    }
}

