using UnityEngine;
using System.Collections;

public class Global : MonoBehaviour {

	private bool onlineGame = false;
	private bool turnTimeGame = false;
	private bool roundTimeGame = false;
	private int turnTimeRound1 = 15;
	private int turnTimeRound2 = 10;
	private int turnTimeRound3 = 5;
	private int roundTime = 80;

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

	public bool GetRoundTimeGame () {
		return roundTimeGame;
	}
	
	public void SetRoundTimeGame (bool flag){
		roundTimeGame = flag;
	}

	public bool GetTurnTimeGame () {
		return turnTimeGame;
	}

	public void SetTurnTimeGame (bool flag){
		turnTimeGame = flag;
	}

	public int GetTurnTimeRound1 () {
		return turnTimeRound1;
	}

	public int GetTurnTimeRound2 () {
		return turnTimeRound2;
	}

	public int GetTurnTimeRound3 () {
		return turnTimeRound3;
	}

	public void SetTurnTime (int round1, int round2, int round3) {
		turnTimeRound1 = round1;
		turnTimeRound2 = round2;
		turnTimeRound3 = round3;
	}

	public int GetRoundTime () {
		return roundTime;
	}

	public void SetRoundTime (int time) {
		roundTime = time;
	}

}
