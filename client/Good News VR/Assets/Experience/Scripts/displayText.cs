using UnityEngine;
using TMPro;
public class displayText : MonoBehaviour
{
	[SerializeField] private GameObject headline;
	[SerializeField] private Transform mainCamera;

	[SerializeField] private float headlineDistance = 3f;

	public void Display(string message)
	{
		headline.GetComponent<TextMeshPro>().text = message;
		headline.transform.position = mainCamera.position + mainCamera.forward * headlineDistance;
		headline.transform.rotation = Quaternion.LookRotation(headline.transform.position - mainCamera.position);
	}
	void Update()
	{

	}
}
