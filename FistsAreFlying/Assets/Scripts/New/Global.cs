using UnityEngine;
using System.Collections;

public class Global : MonoBehaviour {

	private bool onlineGame = false;
	private bool turnTimeGame = false;
	private int turnTime = 10;

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

	public bool GetTurnTimeGame () {
		return turnTimeGame;
	}

	public void SetTurnTimeGame (bool flag){
		turnTimeGame = flag;
	}

	public int GetTurnTime () {
		return turnTime;
	}

	public void SetTurnTime (int time) {
		turnTime = time;
	}

}
