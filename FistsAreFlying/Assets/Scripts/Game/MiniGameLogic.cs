using UnityEngine;
using System.Collections;

public class MiniGameLogic : MonoBehaviour {

	public float startTime; 
	float countdown; 
	bool isLegal; 
	bool clickG1; 
	bool clickG2; 
	int timeClickG1; 
	int timeClickG2; 
	Vector3 screenMouse; 


	private GameObject countdownText;
	private GameObject lose;
	private GameObject win;
	private GameObject parity;
	// IMPORTA STA
	
	// Use this for initialization
	void Start () {
		isLegal = false; 
		clickG1 = false; 
		clickG2 = false;
		countdownText= GameObject.Find("Via");
		countdownText.SetActive(false);
		lose= GameObject.Find("G2 Win");
		lose.SetActive(false);
		win = GameObject.Find("G1 Win");
		win.SetActive(false);
		parity = GameObject.Find("Pareggio");
		parity.SetActive(false);
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
		yield return new WaitForSeconds (1.0f);
		countdownText.SetActive (false);
	}
	


	void OnMouseUpAsButton(){
		screenMouse = Camera.main.ScreenToWorldPoint (Input.mousePosition);
		Debug.Log (screenMouse.x);
		//controllo che sia in posizione del Giocatore 1 (sx)
		if (screenMouse.x < transform.position.x) {
			//se clicca prima del tempo
			if (!clickG1){
				clickG1 =true;
				if (!isLegal) {
					clickG2 = true;  
					timeClickG2 =1; 
					timeClickG1 =10;
					//se clicca in tempo prendo il time
				} else {
					timeClickG1 =Time.frameCount;
					Debug.Log ("tempo G1:" + timeClickG1);
				}
			}

		}
		if (screenMouse.x > transform.position.x){
			if (!clickG2){
				clickG2 = true; 
				//se clicca troppo presto
				if (!isLegal) {
					clickG1= true;
					timeClickG2 =10; 
					timeClickG1 = 1;
					//se clicca in tempo prendo il time
				} else {
					timeClickG2 =  Time.frameCount;
					Debug.Log ("tempo G2:" + timeClickG2);
				}
			}
		}
	}
	

	// Update is called once per frame
	void Update () {

		if (clickG1 && clickG2) {
			if (timeClickG1 > timeClickG2){
				Debug.Log ("ha vinto G2");
				lose.SetActive (true); 
			}
			if (timeClickG1 < timeClickG2){
				Debug.Log ("ha vinto G1");
				win.SetActive (true); 
			}
			if (timeClickG1 == timeClickG2){
				Debug.Log ("parità");
				parity.SetActive (true);
			}

		}
	}

	 
}

