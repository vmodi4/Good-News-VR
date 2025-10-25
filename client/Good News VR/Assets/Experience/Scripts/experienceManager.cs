using UnityEngine;
using System.Collections;

public class experienceManager : MonoBehaviour
{
	public displayText displayTextInstance;
	void Start()
	{
		StartCoroutine(EventTimeline());
	}

	IEnumerator EventTimeline()
	{
		displayTextInstance.Display("Start of timeline");

		yield return new WaitForSeconds(2f);
		displayTextInstance.Display("2 seconds passed – do first event");

		yield return new WaitForSeconds(3f);
		displayTextInstance.Display("Another 3 seconds passed – do next event");

		yield return new WaitForSeconds(5f);
		displayTextInstance.Display("Timeline complete");
	}
}
