using UnityEngine;
using System.Collections;

public class floorScaler : MonoBehaviour
{
    private Transform t;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        t = GetComponent<Transform>();
        t.localScale = new Vector3(0f, 0f, 0f);
    }

    public void AppearFloor(float duration)
    {
        StartCoroutine(AppearFloorCoroutine(duration));
    }

    private IEnumerator AppearFloorCoroutine(float duration)
    {
        float elapsed = 0f;
        Vector3 initialScale = t.localScale;
        Vector3 targetScale = new Vector3(1f, 1f, 1f);

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            t.localScale = Vector3.Lerp(initialScale, targetScale, elapsed / duration);
            yield return null;
        }

        t.localScale = targetScale;
    }
}
