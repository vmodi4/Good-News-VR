using UnityEngine;
using System.Collections;

public class skysphereManager : MonoBehaviour
{
    private Material skyMat;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        skyMat = GetComponent<Renderer>().material;
    }

    public void FadeBackground(float startBackground, float endBackground, float duration)
    {
        StartCoroutine(FadeBackgroundCoroutine(startBackground, endBackground, duration));
    }

    public IEnumerator FadeBackgroundCoroutine(float startBackground, float endBackground, float duration)
    {
        float elapsed = 0f;
        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            skyMat.SetFloat("_MakeBlack", Mathf.Lerp(startBackground, endBackground, elapsed / duration));
            yield return null;
        }
        skyMat.SetFloat("_MakeBlack", endBackground);
    }
    public void SetBackground(float background)
    {
        skyMat.SetFloat("_MakeBlack", background);
    }
}
