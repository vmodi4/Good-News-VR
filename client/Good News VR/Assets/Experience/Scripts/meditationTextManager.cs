using UnityEngine;
using System.Collections;
using TMPro;

public class meditationTextManager : MonoBehaviour
{
	private TextMeshPro tmp;
	// Start is called once before the first execution of Update after the MonoBehaviour is created
	void Start()
	{
		tmp = GetComponent<TextMeshPro>();
	}

	public void FadeText(float startAlpha, float endAlpha, float duration)
	{
		StartCoroutine(FadeTextCoroutine(startAlpha, endAlpha, duration));
	}

	public void SetText(string text)
	{
		tmp.text = text;
	}

	public IEnumerator FadeTextCoroutine(float startAlpha, float endAlpha, float duration)
	{
		float elapsed = 0f;
		while (elapsed < duration)
		{
			elapsed += Time.deltaTime;
			tmp.faceColor = Color.Lerp(new Color(1, 1, 1, startAlpha), new Color(1, 1, 1, endAlpha), elapsed / duration);
			yield return null;
		}
		tmp.faceColor = new Color(1, 1, 1, endAlpha);
	}
	public void SetOpacity(float alpha)
	{
		tmp.faceColor = new Color(1, 1, 1, alpha);
	}

	public IEnumerator ChangeText(string newText, float fadeOutDuration, float fadeInDuration)
	{
		// Fade out
		StartCoroutine(FadeTextCoroutine(tmp.faceColor.a, 0f, fadeOutDuration));
		yield return new WaitForSeconds(fadeOutDuration);

		// Change text
		SetText(newText);

		// Fade in
		StartCoroutine(FadeTextCoroutine(0f, 1f, fadeInDuration));
		yield return new WaitForSeconds(fadeInDuration);
	}
}
