using UnityEngine;
using System.Collections;

public class MiniGameLogic : MonoBehaviour {
	float startTime; 
	float countdown; 
	bool isLegal; 
	private GameObject countdownText;

	// Use this for initialization
	void Start () {
		isLegal = false; 
		countdownText= GameObject.Find("GUI Text");
		countdownText.SetActive(false);
		startTime = Random.Range (2, 10);
		StartCoroutine(StartCountdown());


	}

	IEnumerator StartCountdown()
	{
		while (startTime >0)
		{
			yield return new WaitForSeconds(1.0f);
			startTime --;
			Debug.Log (startTime);
			//countdownText.GetComponent<GUIText>().text= countdown.ToString();

		}
		isLegal = true; 
		countdownText.SetActive (true); 
	}

	// Update is called once per frame
	void Update () {
	
	}
}
