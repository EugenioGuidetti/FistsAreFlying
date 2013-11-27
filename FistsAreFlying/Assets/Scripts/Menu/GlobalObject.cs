using UnityEngine;
using System.Collections;

public class GlobalObject : MonoBehaviour {


	public bool turnTimeGame;
	public int turnTime;
	// Use this for initialization
	void Start () {
		turnTimeGame=false;
		turnTime=0;
		GameObject.DontDestroyOnLoad(this);
		Application.LoadLevel("MainMenu");
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void setTurnTimeGame (bool sent){
		this.turnTimeGame=sent;
	}
}
