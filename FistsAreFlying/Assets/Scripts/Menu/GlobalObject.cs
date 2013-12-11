using UnityEngine;
using System.Collections;

public class GlobalObject : MonoBehaviour {


	public bool turnTimeGame;
	public bool roundTimeGame;
	public int turnTimeRound1,turnTimeRound2,turnTimeRound3;
	public int roundTime;
	public int roundWinPlayer1;
	public int roundWinPlayer2;
	public int round;
	// Use this for initialization
	void Start () {
		turnTimeGame=false;
		turnTimeRound1=15;
		turnTimeRound2=10;
		turnTimeRound3=5;
		roundTime=80;
		round= 1;
		roundWinPlayer1=0;
		roundWinPlayer2=0;
		GameObject.DontDestroyOnLoad(this);
		Application.LoadLevel("MainMenu");
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void setRoundTimeGame (bool sent){
		this.roundTimeGame=sent;
	}
	void setTurnTimeGame (bool sent){
		this.turnTimeGame=sent;
	}
}
