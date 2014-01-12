using UnityEngine;
using System.Collections;

public class Global : MonoBehaviour {

	private bool onlineGame = false;
	private bool amIPlayer1 = false;
	private bool timeGame = false;
	private int time = 10;
	private bool soundEnable = true;
	private bool effectsEnable = true;

	// Use this for initialization
	void Start () {
		GameObject.DontDestroyOnLoad(this);
		Application.LoadLevel("MainMenu");
	}
	
	// Update is called once per frame
	void Update () {}

	public bool GetOnlineGame () {
		return onlineGame;
	}

	public void SetOnlineGame (bool flag) {
		onlineGame = flag;
	}

	public bool GetAmIPlayer1 () {
		return amIPlayer1;
	}

	public void SetAmIPlayer1 (bool flag) {
		amIPlayer1 = flag;
	}

	public bool GetTimeGame () {
		return timeGame;
	}

	public void SetTimeGame (bool flag){
		timeGame = flag;
	}

	public int GetTime () {
		return time;
	}

	public void SetTime (int time) {
		this.time = time;
	}

	public bool GetSound () {
		return soundEnable;
	}

	public void SetSound (bool flag) {
		soundEnable = flag;
	}

	public bool GetEffects () {
		return effectsEnable;
	}

	public void SetEffects (bool flag) {
		effectsEnable = flag;
	}

}
