using UnityEngine;
using System.Collections;

public class TGameLogic : MonoBehaviour {
	
	private GameObject global;
	private bool onlineMatch;
	private bool amIPlayer1;
	private bool isOpponentReady = false;
	private bool timeMatch = false;
	private int time;
	private int countdown;
	private int round;
	private bool choosePhase = false;
	private bool endPhase = false;

	public GameObject matchLoop;
	public GameObject animationLogic;
	public GameObject pauseGUI;
	public GameObject defenseGame;
	public GameObject defenseResult;
	public GameObject conflictGame;
	public GameObject conflictResult;
	private string minigameResult;
	
	public GameObject roundGUI;
	public GameObject countDownPlayer1GUI;
	public GameObject countDownPlayer2GUI;
	public GameObject mainMessagesGUI;
	public GameObject winRoundsGUI;
	public GameObject player1HealthBar;
	public GameObject player2HealthBar;
	
	public GameObject player1;
	public GameObject player2;
	private string player1Move = "";
	private string player2Move = "";
	private bool player1Selected = false;
	private bool player2Selected = false;
	private bool tapPlayer1 = false;
	private bool tapPlayer2 = false;
	private int player1Health;
	private int player2Health;
	private float healthUnit;
	private float scaleUnit;
	private const int healthTotal = 15;
	private int player1WinnedRounds;
	private int player2WinnedRounds;
	
	private int normalDamage = 2;
	private Hashtable rules = new Hashtable();
	
	// Use this for initialization
	void Start () {
		initializeRules();
		player1Health = healthTotal;
		player2Health = healthTotal;
		healthUnit = 1f / player1Health;
		scaleUnit = player1HealthBar.transform.localScale.x * healthUnit;
		round = 1;
		player1WinnedRounds = 0;
		player2WinnedRounds = 0;
		global = GameObject.Find("GlobalObject");
		conflictGame.GetComponent<ConflictLogic>().SetGlobal(global);
		if (global.GetComponent<Global>().GetSound()) {
			matchLoop.SetActive(true);
		}
		if (global.GetComponent<Global>().GetOnlineGame()) {
			onlineMatch = true;
			defenseGame.GetComponent<DefenseLogic>().SetOnline();
			amIPlayer1 = global.GetComponent<Global>().GetAmIPlayer1();
			isOpponentReady = true;
			if (amIPlayer1) {
				defenseGame.GetComponent<DefenseLogic>().SetPlayer1();
				player1.GetComponent<TPlayer>().SetOnline();
				player2.GetComponent<TPlayer>().PutInHidePosition(amIPlayer1);
			} else {
				player2.GetComponent<TPlayer>().SetOnline();
				player1.GetComponent<TPlayer>().PutInHidePosition(amIPlayer1);
			}
		} else {
			player1.GetComponent<TPlayer>().PutInHidePosition(true);
			player2.GetComponent<TPlayer>().PutInHidePosition(false);
		}
		if (global.GetComponent<Global>().GetTimeGame()) {
			timeMatch = true;
			if (onlineMatch) {
				time = global.GetComponent<Global>().GetOnlineTime();
			} else {
				time = global.GetComponent<Global>().GetTime();
			}
		}
		if (onlineMatch && timeMatch){
			if (amIPlayer1) {
				countDownPlayer1GUI.GetComponent<CountDownGUI>().SetSprite(time);
				StartCoroutine("TurnCountdownPlayer1");
			} else {
				countDownPlayer2GUI.GetComponent<CountDownGUI>().SetSprite(time);
				StartCoroutine("TurnCountdownPlayer2");
			}
		}
		choosePhase = true;
		if(!onlineMatch){
			mainMessagesGUI.GetComponent<MainMessagesGUI>().SetSprite("p1Turn");
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
		if (!onlineMatch) {
			pauseCheck();
		} else {
			pauseCheckOnline();
		}
		if (choosePhase) {
			if (!onlineMatch) {
				LocalChoosePhase();
			} else if (isOpponentReady) {
				if (amIPlayer1) {
					OnlineP1ChoosePhase();
				} else {
					OnlineP2ChoosePhase();
				}
			}
		}
		if (endPhase && !defenseGame.activeSelf && !conflictGame.activeSelf && animationLogic.GetComponent<AnimatorLogic>().isAnmiationEnd()) {
			EndTurnPhase();
		}
	}
	
	private void pauseCheck() {
		if (Input.GetKeyDown(KeyCode.Escape)){
			if(!pauseGUI.activeSelf){
				Time.timeScale = 0;
				player1.SetActive(false);
				player2.SetActive(false);
				pauseGUI.SetActive(true);
				//audio.Pause();
			} else {
				ResumeGame();
			}
		}
	}
	
	private void pauseCheckOnline () {
		if (Input.GetKeyDown(KeyCode.Escape)) {
			Application.LoadLevel("MainMenu");
		}
	}
	
	private void ResumeGame () {
		pauseGUI.SetActive(false);
		player1.SetActive(true);
		player2.SetActive(true);
		Time.timeScale = 1;
		/*if (!audio.isPlaying) audio.Play(); */		
	}
	
	private void LocalChoosePhase () {
		if (!player1Selected) {
			if(!tapPlayer1){
				if(Input.touches.Length == 1 && Input.GetTouch(0).phase == TouchPhase.Began && !pauseGUI.activeSelf){
					tapPlayer1=true;
					player1.GetComponent<TPlayer>().PutInShowPosition();
					mainMessagesGUI.GetComponent<MainMessagesGUI>().SetSprite("");
					if (timeMatch){
						countDownPlayer1GUI.GetComponent<CountDownGUI>().SetSprite(time);
						StartCoroutine("TurnCountdownPlayer1");
					}
				}
			} else if (player1.GetComponent<TPlayer>().GetHaveIChoosed()) {
				player1Move = player1.GetComponent<TPlayer>().GetSelectedMove();
				player1Selected = true;
				player1.GetComponent<TPlayer>().PutInHidePosition(true);
				mainMessagesGUI.GetComponent<MainMessagesGUI>().SetSprite("p2Turn");
			}
		} else {
			if (!tapPlayer2) {
				if(Input.touches.Length == 1 && Input.GetTouch(0).phase == TouchPhase.Began && !pauseGUI.activeSelf) {
					tapPlayer2=true;
					player2.GetComponent<TPlayer>().PutInShowPosition();
					mainMessagesGUI.GetComponent<MainMessagesGUI>().SetSprite("");
					if (timeMatch){
						countDownPlayer2GUI.GetComponent<CountDownGUI>().SetSprite(time);
						StartCoroutine("TurnCountdownPlayer2");
					}
				}
			} else if (!player2Selected) {
				if (player2.GetComponent<TPlayer>().GetHaveIChoosed()) {
					player2Move = player2.GetComponent<TPlayer>().GetSelectedMove();
					player2Selected = true;
					player2.GetComponent<TPlayer>().PutInHidePosition(false);
				}
			}
		}
		if (player1Selected) {
			StopCoroutine("TurnCountdownPlayer1");
		}
		if (player2Selected) {
			StopCoroutine("TurnCountdownPlayer2");
		}
		if (player1Selected && player2Selected) {
			choosePhase = false;
			StartCoroutine("MainFlow");
		}
	}
	
	private void OnlineP1ChoosePhase () {
		if (!player1Selected) {
			if (player1.GetComponent<TPlayer>().GetHaveIChoosed()) {
				player1Selected = true;
				StopCoroutine("TurnCountdownPlayer1");
				player1Move = player1.GetComponent<TPlayer>().GetSelectedMove();
				player1.GetComponent<TPlayer>().PutInHidePosition(true);
				networkView.RPC("Player1Decision", RPCMode.Others, player1Move);
			}
		}
		if (!player2Selected) {
			if (player2.GetComponent<TPlayer>().GetHaveIChoosed()) {
				player2Move = player2.GetComponent<TPlayer>().GetSelectedMove();
				player2Selected = true;
			}
		}
		if (player1Selected && player2Selected) {
			isOpponentReady = false;
			choosePhase = false;
			StartCoroutine("MainFlow");
		}
	}
	
	private void OnlineP2ChoosePhase () {
		if (!player1Selected) {
			if (player1.GetComponent<TPlayer>().GetHaveIChoosed()) {
				player1Move = player1.GetComponent<TPlayer>().GetSelectedMove();
				player1Selected = true;
			}
		}
		if (!player2Selected) {
			if (player2.GetComponent<TPlayer>().GetHaveIChoosed()) {
				player2Selected = true;
				StopCoroutine("TurnCountdownPlayer2");
				player2Move = player2.GetComponent<TPlayer>().GetSelectedMove();
				player2.GetComponent<TPlayer>().PutInHidePosition(true);
				networkView.RPC("Player2Decision", RPCMode.Server, player2Move);
			}
		}
		if (player1Selected && player2Selected) {
			isOpponentReady = false;
			choosePhase = false;
			StopCoroutine("TurnCountdown");
			StartCoroutine("MainFlow");
		}
	}
	
	[RPC] void Player1Decision (string move) {
		player1.GetComponent<TPlayer>().OnlineSelectMove(move);
	}
	
	[RPC] void Player2Decision (string move) {
		player2.GetComponent<TPlayer>().OnlineSelectMove(move);
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
		if (defenseResult.GetComponent<DefenseResult>().GetFreshness()) {
			minigameResult = defenseResult.GetComponent<DefenseResult>().GetResult();
			if (minigameResult.Equals("Player1")) {
				if (onlineMatch) {
					if (amIPlayer1) {
						player1.GetComponent<TPlayer>().SetForcedMove();
					}
				} else {
					player1.GetComponent<TPlayer>().SetForcedMove();
				}
			}
			if (minigameResult.Equals("Player2")) {
				if (onlineMatch) {
					if (!amIPlayer1) {
						player2.GetComponent<TPlayer>().SetForcedMove();
					}
				} else {
					player2.GetComponent<TPlayer>().SetForcedMove();
				}
			}
		}
		animationLogic.GetComponent<AnimatorLogic>().EndMinigame(minigameResult);
		minigameResult = "";
		UpdateHealthBar(player1HealthBar.GetComponent<SpriteRenderer>(), player1Health);
		UpdateHealthBar(player2HealthBar.GetComponent<SpriteRenderer>(), player2Health);
		//controllo fine round e fine match
		StartCoroutine("EndTurnChecks");
	}
	
	private IEnumerator MainFlow () {
		yield return new WaitForSeconds(2f);
		player1.GetComponent<TPlayer>().ShowSelectedMove();
		player2.GetComponent<TPlayer>().ShowSelectedMove();
		//lanciare le animazioni di avvicinamento		
		yield return new WaitForSeconds(1f);
		ApplyRules();
		endPhase = true;
	}
	
	private IEnumerator TurnCountdownPlayer1 () {
		countdown = time;
		while (countdown > 0) {
			yield return new WaitForSeconds(1f);
			countdown --;
			countDownPlayer1GUI.GetComponent<CountDownGUI>().SetSprite(countdown);
		}
		if (!player1Selected) {
			player1.GetComponent<TPlayer>().SetForcedMove();
		}
	}
	
	private IEnumerator TurnCountdownPlayer2 () {
		countdown = time;
		while (countdown > 0) {
			yield return new WaitForSeconds(1f);
			countdown --;
			countDownPlayer2GUI.GetComponent<CountDownGUI>().SetSprite(countdown);
		}
		if (!player2Selected) {
			player2.GetComponent<TPlayer>().SetForcedMove();
		}
	}
	
	private void ApplyRules () {
		string outcome = "";
		if (player1Move.Equals(player2Move)) {
			if (!player1Move.Equals("EM") && !player1Move.Equals("D")) {
				outcome = "sameMoves";
				player1Health = player1Health - normalDamage;
				player2Health = player2Health - normalDamage;
			}
		} else if (player1Move.Equals("D") || player2Move.Equals("D")) {
			if (!player1Move.Equals("EM") && !player2Move.Equals("EM")) {
				outcome = "defense";
			}
		} else if (player1Move.Equals("EM") || player2Move.Equals("EM")) {
			if (player1Move.Equals("EM")) {
				player1Health = player1Health - normalDamage;
			}
			if (player2Move.Equals("EM")) {
				player2Health = player2Health - normalDamage;
			}
			outcome = "emptyVsDamage";
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
			}
		}
		animationLogic.GetComponent<AnimatorLogic>().SetMoves(player1Move, player2Move, outcome);
	}
	
	private IEnumerator EndTurnChecks () {
		//attendo che tornino indietro (devo farlo da qui perchè nel caso ci siano minigiochi
		yield return new WaitForSeconds (1f);
		if (player1Health <= 0 || player2Health <= 0) {
			if (player1Health <= 0) {
				player2WinnedRounds = player2WinnedRounds + 1;
			}
			if (player2Health <= 0) {
				player1WinnedRounds = player1WinnedRounds + 1;
			}
			//stampare risultato round
			if (player1Health > 0) {
				mainMessagesGUI.GetComponent<MainMessagesGUI>().SetSprite("p1WinRound");
				animationLogic.GetComponent<AnimatorLogic>().EndRound("player2");
			} else if (player2Health > 0) {
				mainMessagesGUI.GetComponent<MainMessagesGUI>().SetSprite("");
				animationLogic.GetComponent<AnimatorLogic>().EndRound("player1");
			} else {
				mainMessagesGUI.GetComponent<MainMessagesGUI>().SetSprite("roundDraw");
				animationLogic.GetComponent<AnimatorLogic>().EndRound("draw");
			}
			yield return new WaitForSeconds(2f);
			mainMessagesGUI.GetComponent<MainMessagesGUI>().SetSprite("");
			winRoundsGUI.GetComponent<WinRoundsGUI>().SetRoundPlayers ( player1WinnedRounds, player2WinnedRounds);
			if (round == 3) {
				StartCoroutine("EndMatch");
			} 
			if (round == 2) {
				if ((player1WinnedRounds == 2 && player2WinnedRounds == 0) || (player1WinnedRounds == 0 && player2WinnedRounds == 2)) {
					StartCoroutine("EndMatch");
				} else {
					NewRound();
					yield return new WaitForSeconds (4f);
				}
			}
			
			if (round == 1) {
				NewRound();
				yield return new WaitForSeconds (4f);
			}
		} else {
			player1.GetComponent<TPlayer>().NewTurn();
			player2.GetComponent<TPlayer>().NewTurn();		
		}
		player1Selected = false;
		player2Selected = false;
		if (onlineMatch) {
			if (timeMatch) {
				if (amIPlayer1){
					countDownPlayer1GUI.GetComponent<CountDownGUI>().SetSprite(time);
					StartCoroutine("TurnCountdownPlayer1");
				}
				else {
					countDownPlayer2GUI.GetComponent<CountDownGUI>().SetSprite(time);
					StartCoroutine("TurnCountdownPlayer2");
				}
			}
		}
		else{
			mainMessagesGUI.GetComponent<MainMessagesGUI>().SetSprite("p1Turn");
			tapPlayer1 = false;
			tapPlayer2 = false;
		}
		choosePhase = true;
		if (onlineMatch) {
			networkView.RPC("ComunicateReady", RPCMode.Others);
		}
	}
	
	[RPC] void ComunicateReady () {
		isOpponentReady = true;
	}
	
	private void NewRound () {
		animationLogic.GetComponent<AnimatorLogic>().NewRound();
		round = round + 1;
		roundGUI.GetComponent<RoundGUI>().SetSprite (round);
		player1Health = healthTotal;
		player2Health = healthTotal;
		UpdateHealthBar(player1HealthBar.GetComponent<SpriteRenderer>(), player1Health);
		UpdateHealthBar(player2HealthBar.GetComponent<SpriteRenderer>(), player2Health);
		player1.GetComponent<TPlayer>().NewRound();
		player2.GetComponent<TPlayer>().NewRound();
	}
	
	private IEnumerator EndMatch () {
		StopCoroutine("EndTurnChecks");
		if (player1WinnedRounds > player2WinnedRounds) {
			mainMessagesGUI.GetComponent<MainMessagesGUI>().SetSprite("p1WinMatch");
		} else if (player2WinnedRounds > player1WinnedRounds) {
			mainMessagesGUI.GetComponent<MainMessagesGUI>().SetSprite("p2WinMatch");
		} else {
			mainMessagesGUI.GetComponent<MainMessagesGUI>().SetSprite("matchDraw");
		}
		yield return new WaitForSeconds(2f);
		mainMessagesGUI.GetComponent<MainMessagesGUI>().SetSprite("");
		Application.LoadLevel("MainMenu");
	}
	
	private void UpdateHealthBar (SpriteRenderer healthBar, int actualHealth) {
		if (actualHealth < 0) {
			actualHealth = 0;
		}
		healthBar.material.color = Color.Lerp(Color.green, Color.red, 1 - actualHealth * healthUnit);
		healthBar.transform.localScale = new Vector3(scaleUnit * actualHealth, healthBar.transform.localScale.y, healthBar.transform.localScale.z);
	}
}