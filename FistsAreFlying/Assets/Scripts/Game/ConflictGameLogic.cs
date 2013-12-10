using UnityEngine;
using System.Collections;

public class ConflictGameLogic : MonoBehaviour {

	public float startTime; 
	float countdown; 
	bool isLegal; 
	bool clickG1; 
	bool clickG2; 
	int timeClickG1; 
	int timeClickG2; 
	Vector3 screenMouse; 


	private float offset=20;
	private Camera mainCamera;
	private Camera thisCamera;
	private _gameLogic gameLogic;
	private GameObject countdownText;

	private GameObject mainCountdown;
	
	// Use this for initialization
	void Start () {
		mainCamera=GameObject.Find ("Main Camera").GetComponent<Camera>();
		thisCamera= GameObject.Find("ConflictGameCam").GetComponent<Camera>();
		gameLogic= GameObject.Find("GameLogic").GetComponent<_gameLogic>(); 
		clickG1 = false; 
		clickG2 = false;
		countdownText= GameObject.Find("Via");
		countdownText.SetActive(false);
		mainCountdown= GameObject.Find("Countdown");





	}

	IEnumerator StartConflictCountdown()
	{
		mainCountdown.GetComponent<GUIText>().enabled=false;
		isLegal = false;
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
	}
	


	void OnMouseUpAsButton(){
		screenMouse = thisCamera.ScreenToWorldPoint (Input.mousePosition);
		Debug.Log (screenMouse.x);
		//controllo che sia in posizione del Giocatore 1 (sx)
		if (screenMouse.x < transform.position.x) {
			//se clicca prima del tempo
			if (!clickG1){
				clickG1 =true;
				if (!isLegal) {
					clickG2 = true;  
					timeClickG2 =1; 
					timeClickG1 =1000;
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
					timeClickG2 =1000; 
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

		if( gameLogic.startCountdownMiniGame){
			startTime = Random.Range (2, 10);
			StartCoroutine("StartConflictCountdown");
			gameLogic.startCountdownMiniGame=false;
		}

		if (clickG1 && clickG2) {
			StartCoroutine("EndFlow");

		}
	}

	IEnumerator EndFlow(){
		if (timeClickG1 > timeClickG2 + offset){
			gameLogic.conflictPlayer2=true;
			Debug.Log ("ha vinto G2");
			countdownText.GetComponent<GUIText>().text="VINCE PLAYER2"; 
		} else if (timeClickG2 > timeClickG1 + offset){
			gameLogic.conflictPlayer1=true;
			Debug.Log ("ha vinto G1");
			countdownText.GetComponent<GUIText>().text="VINCE PLAYER1"; 
		} else {
			gameLogic.conflictDrow=true;
			Debug.Log ("parità");
			countdownText.GetComponent<GUIText>().text="PARITA'"; 
		}
		yield return new WaitForSeconds(2);
		countdownText.GetComponent<GUIText>().text="VIA";
		countdownText.SetActive (false);
		mainCountdown.GetComponent<GUIText>().enabled=true;
		gameLogic.miniGame=false;
		thisCamera.enabled=false;
		mainCamera.enabled=true;
		clickG1=false;
		clickG2=false;
		StopCoroutine("StartConflictCountdown");
	
	}

	 
}

