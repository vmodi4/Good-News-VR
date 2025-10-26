using UnityEngine;
using TMPro;
public class displayText : MonoBehaviour
{
	public void Display(string message)
	{
		gameObject.GetComponent<TextMeshPro>().text = message;
	}
	void Update()
	{

	}
}
