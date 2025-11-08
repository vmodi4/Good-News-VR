using UnityEngine;
using System.Collections;
using System.Diagnostics;
using System.Threading.Tasks;

public class experienceManager : MonoBehaviour
{
	public meditationTextManager mtm;
	public textboxManager tm;

	public skysphereManager sm;

	public floorScaler fs;

	public GameObject particles;
	public GameObject floor;

	public narrationManager nm;

	public NewsResponse nr;

	public AudioClip boxBreathingClip;
	public AudioClip transitionClip;

	public AudioClip newsClip1;
	public AudioClip newsClip2;
	public AudioClip newsClip3;

	public NewsItem[] newsItems;

	private Material skyMat;
	async Task Start()
	{
		particles.SetActive(false);
		sm.SetBackground(0f);
		mtm.SetOpacity(0f);
		StartCoroutine(EventTimeline());
		// await AudioHelper.FetchAudioClip(this, "");
		// newsItems = await NewsService.GetNewsFromAPI("https://test.sneakycloud.xyz/");
		// AudioHelper.FetchAudioClip(this, "test.sneakycloud.xyz", newsItems[0].audio_id, (clip) =>
		// {
		// 	newsClip1 = clip;
		// }, (error) =>
		// {
		// 	UnityEngine.Debug.LogError("Failed to load audio clip for news item 1: " + error);
		// });

		// AudioHelper.FetchAudioClip(this, "test.sneakycloud.xyz", newsItems[1].audio_id, (clip) =>
		// {
		// 	newsClip2 = clip;
		// }, (error) =>
		// {
		// 	UnityEngine.Debug.LogError("Failed to load audio clip for news item 2: " + error);
		// });

		// AudioHelper.FetchAudioClip(this, "test.sneakycloud.xyz", newsItems[2].audio_id, (clip) =>
		// {
		// 	newsClip3 = clip;
		// }, (error) =>
		// {
		// 	UnityEngine.Debug.LogError("Failed to load audio clip for news item 3: " + error);
		// });
	}

	IEnumerator EventTimeline()
	{

		nm.PlayNarration(boxBreathingClip);

		yield return new WaitForSeconds(16f);
		mtm.SetText("Inhale...");
		mtm.FadeText(0f, 1f, 1f);

		yield return new WaitForSeconds(12f);
		mtm.SetText("Hold...");

		yield return new WaitForSeconds(14f);
		mtm.SetText("Exhale...");

		yield return new WaitForSeconds(15f);
		// mtm.SetText("Hold...");

		mtm.FadeText(1f, 0f, 1f);
		yield return new WaitForSeconds(boxBreathingClip.length - (16f + 12f + 14f + 15f));
		yield return new WaitForSeconds(3f);

		// yield return new WaitForSeconds(20f);

		sm.FadeBackground(0f, 1f, transitionClip.length);
		nm.PlayNarration(transitionClip);
		yield return new WaitForSeconds(transitionClip.length + 3f);
		particles.SetActive(true);
		fs.AppearFloor(2f);
		yield return new WaitForSeconds(3f);
		tm.ShowPanel(newsItems[0].headline, newsItems[0].info1 + "\n" + newsItems[0].info2 + "\n" + newsItems[0].info3);
		nm.PlayNarration(newsClip1);
		yield return new WaitForSeconds(newsClip1.length + 2f);
		tm.HidePanel();

		yield return new WaitForSeconds(5f);

		tm.ShowPanel(newsItems[1].headline, newsItems[1].info1 + "\n" + newsItems[1].info2 + "\n" + newsItems[1].info3);
		nm.PlayNarration(newsClip2);
		yield return new WaitForSeconds(newsClip2.length + 2f);
		tm.HidePanel();

		yield return new WaitForSeconds(5f);

		tm.ShowPanel(newsItems[2].headline, newsItems[2].info1 + "\n" + newsItems[2].info2 + "\n" + newsItems[2].info3);
		nm.PlayNarration(newsClip3);
		yield return new WaitForSeconds(newsClip3.length + 2f);
		tm.HidePanel();

		yield return null;
	}
}
