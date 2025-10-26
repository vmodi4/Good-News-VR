using UnityEngine;
using System.Collections;

public class experienceManager : MonoBehaviour
{
	public displayText displayTextInstance;
	public textboxManager textboxManagerInstance;
	void Start()
	{
		StartCoroutine(EventTimeline());
	}

	IEnumerator EventTimeline()
	{
		textboxManagerInstance.ShowPanel("Experience Start", "This is the beginning of the experience.");

		yield return new WaitForSeconds(10f);
		textboxManagerInstance.HideBody();

		yield return new WaitForSeconds(3f);
		textboxManagerInstance.ShowBody("Here is some additional information displayed in the body text.");

		yield return new WaitForSeconds(10f);
		textboxManagerInstance.HidePanel();
	}
}
