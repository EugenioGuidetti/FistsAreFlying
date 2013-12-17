using UnityEngine;
using System.Collections;

public class GameLogic : MonoBehaviour {

	private GameObject global;

	private bool turnTimeMatch = false;
	private float turnTime;
	private bool roundTimeMatch = false;
	private float player1Time;
	private float player2Time;
	private int turn;

	public GameObject MiniGameDefense;
	public GameObject MiniGameConflict;

	public GameObject Player1;
	public GameObject Player2;
	private string player1Move = "";
	private string player2Move = "";
	private bool player1Selected = false;
	private bool player2Selected = false;
	private int player1Health;
	private int player2Health;

	private int normalDamage = 2;
	private Hashtable rules = new Hashtable();

	// Use this for initialization
	void Start () {
		initializeRules();
		turn = 1;
		global = GameObject.Find("GlobalObject");
		if (global.GetComponent<Global>().GetTurnTimeGame()) {
			turnTimeMatch = true;
			turnTime = global.GetComponent<Global>().GetTurnTimeRound1();
		}
		if (global.GetComponent<Global>().GetRoundTimeGame()) {
			roundTimeMatch = true;
			player1Time = global.GetComponent<Global>().GetRoundTime();
			player2Time = global.GetComponent<Global>().GetRoundTime();
		}
	}

	private void initializeRules () {
		rules.Add ("PR.PL", "conflict");
		rules.Add ("PR.KR", "player2");
		rules.Add ("PR.KL", "player1");
		rules.Add ("PL.PR", "conflict");
		rules.Add ("PL.KR", "player1");
		rules.Add ("PL.KL", "player2");
		rules.Add ("KR.PR", "player1");
		rules.Add ("KR.PL", "player2");
		rules.Add ("KR.KL", "conflict");
		rules.Add ("KL.PR", "player2");
		rules.Add ("KL.PL", "player1");
		rules.Add ("KL.KR", "conflict");
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	private void ApplyRules () {
		string outcome;
		if (player1Move.Equals(player2Move)) {
			if (!player1Move.Equals("EM") && !player1Move.Equals("D")) {
				player1Health = player1Health - normalDamage;
				player2Health = player2Health - normalDamage;
			}
		} else if (player1Move.Equals("D") || player2Move.Equals("D")) {
			if (player1Move.Equals("D") && !player2Move.Equals("EM")) {
				//MiniGameDefense per il giocatore 1
			}
			if (!player1Move.Equals("EM") && player2Move.Equals("D")) {
				//MiniGameDefense per il giocatore 2
			}
		} else if (player1Move.Equals("EM") || player2Move.Equals("EM")) {
			if (player1Move.Equals("EM")) {
				player1Health = player1Health - normalDamage;
			}
			if (player2Move.Equals("EM")) {
				player2Health = player2Health - normalDamage;
			}
		} else {
			outcome = (string) rules[player1Move + "." + player2Move];
			if (outcome.Equals("player1")) {
				player1Health = player1Health - (normalDamage / 2);
				player2Health = player2Health - normalDamage;
			}
			if (outcome.Equals("player2")) {
				player1Health = player1Health - normalDamage;
				player2Health = player2Health - (normalDamage / 2);
			}
			if (outcome.Equals("conflict")) {
				//MiniGameConflict
			}
		}
	}
}
