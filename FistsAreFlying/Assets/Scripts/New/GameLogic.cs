﻿using UnityEngine;
using System.Collections;

public class GameLogic : MonoBehaviour {

	private GameObject global;

	private bool turnTimeMatch = false;
	private int turnTime;
	private int countdown;
	private int round;
	private bool choosePhase = false;
	private bool endPhase = false;

	public GameObject defenseGame;
	public GameObject defenseResult;
	public GameObject conflictGame;
	public GameObject conflictResult;
	private string minigameResult;

	public GameObject Player1;
	public GameObject Player2;
	private string player1Move = "";
	private string player2Move = "";
	private bool player1Selected = false;
	private bool player2Selected = false;
	private int player1Health = 20;
	private int player2Health = 20;

	private int normalDamage = 2;
	private Hashtable rules = new Hashtable();

	public GameObject debugText;
	// Use this for initialization
	void Start () {
		initializeRules();
		round = 1;
		global = GameObject.Find("GlobalObject");
		if (global.GetComponent<Global>().GetTurnTimeGame()) {
			turnTimeMatch = true;
			turnTime = global.GetComponent<Global>().GetTurnTime();
			StartCoroutine("TurnCountdown");
		}
		choosePhase = true;
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
		if (choosePhase) {
			LocalChoosePhase();
		}
		if (endPhase && !defenseGame.activeSelf && !conflictGame.activeSelf) {
			EndTurnPhase();
		}
	}

	private void LocalChoosePhase () {
		if (!player1Selected) {
			if (Player1.GetComponent<Player>().GetHaveIChoosed()) {
				player1Move = Player1.GetComponent<Player>().GetSelectedMove();
				player1Selected = true;
			}
		}
		if (!player2Selected) {
			if (Player2.GetComponent<Player>().GetHaveIChoosed()) {
				player2Move = Player2.GetComponent<Player>().GetSelectedMove();
				player2Selected = true;
			}
		}
		if (player1Selected && player2Selected) {
			choosePhase = false;
			StopCoroutine("TurnCountdown");
			StartCoroutine("MainFlow");
		}
	}

	private void EndTurnPhase () {
		endPhase = false;
		if (conflictResult.GetComponent<ConflictResult>().GetFreshness()) {
			minigameResult = conflictResult.GetComponent<ConflictResult>().GetResult();
			if (minigameResult.Equals("player1")) {
				player2Health = player2Health - normalDamage;
			}
			if (minigameResult.Equals("player2")) {
				player1Health = player1Health - normalDamage;
			}
			if (minigameResult.Equals("draw")) {
				player1Health = player1Health - normalDamage / 2;
				player2Health = player2Health - normalDamage / 2;
			}
		}
		//aggiornamento barre della vita
		if (defenseResult.GetComponent<DefenseResult>().GetFreshness()) {
			minigameResult = defenseResult.GetComponent<DefenseResult>().GetResult();
			if (minigameResult.Equals("Player1")) {
				Player1.GetComponent<Player>().SetForcedMove();
			}
			if (minigameResult.Equals("Player2")) {
				Player2.GetComponent<Player>().SetForcedMove();
			}
		}
		//controllo fine round e fine match
		Player1.GetComponent<Player>().NewTurn();
		Player2.GetComponent<Player>().NewTurn();
		player1Selected = false;
		player2Selected = false;
		if (turnTimeMatch) {
			StartCoroutine("TurnCountdown");
		}
		choosePhase=true;
	}

	private IEnumerator MainFlow () {
		yield return new WaitForSeconds(2f);
		Player1.GetComponent<Player>().ShowSelectedMove();
		Player2.GetComponent<Player>().ShowSelectedMove();
		//lanciare le animazioni di avvicinamento		
		yield return new WaitForSeconds(1f);
		ApplyRules();
		endPhase = true;
	}

	private IEnumerator TurnCountdown () {
		countdown = turnTime;
		while (countdown > 0) {
			yield return new WaitForSeconds(1f);
			countdown --;
			//cambiare la guitext
		}
		if (!player1Selected) {
			Player1.GetComponent<Player>().SetForcedMove();
		}
		if (!player2Selected) {
			Player2.GetComponent<Player>().SetForcedMove();
		}
	}

	private void ApplyRules () {
		string outcome;
		if (player1Move.Equals(player2Move)) {
			if (!player1Move.Equals("EM") && !player1Move.Equals("D")) {
				player1Health = player1Health - normalDamage;
				player2Health = player2Health - normalDamage;
			}
		} else if (player1Move.Equals("D") || player2Move.Equals("D")) {
			if (!player1Move.Equals("EM") && !player2Move.Equals("EM")) {
				defenseGame.SetActive(true);
				defenseGame.GetComponent<DefenseLogic>().SetPlayers(player1Move, player2Move);
				Camera.main.transform.position = new Vector3 (0, -15, Camera.main.transform.position.z);
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
				conflictGame.SetActive(true);
				conflictGame.GetComponent<ConflictLogic>().SetPlayerMoves(player1Move, player2Move);				
				Camera.main.transform.position = new Vector3 (0, 15, Camera.main.transform.position.z);
				conflictGame.GetComponent<ConflictLogic>().StartCountdown();
			}
		}
	}
}
