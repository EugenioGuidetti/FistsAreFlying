using UnityEngine;
using System.Collections;

public class SGameLogic : MonoBehaviour {
	
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

	public GameObject animationLogic;
	public GameObject pauseGUI;
	public GameObject defenseGame;
	public GameObject defenseResult;
	public GameObject conflictGame;
	public GameObject conflictResult;
	private string minigameResult;
	
	public GameObject roundText;
	public GameObject turnTimeText;
	public GameObject messageText;
	private string oldMessage = "";
	public GameObject p1WinnedRoundsText;
	public GameObject p2WinnedRoundsText;
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
	private const int healthTotal = 20;
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
		roundText.GetComponent<GUIText>().text = "Round " + round.ToString();
		player1WinnedRounds = 0;
		player2WinnedRounds = 0;
		p1WinnedRoundsText.GetComponent<GUIText>().text = player1WinnedRounds.ToString();
		p2WinnedRoundsText.GetComponent<GUIText>().text = player2WinnedRounds.ToString();
		global = GameObject.Find("GlobalObject");
		conflictGame.GetComponent<ConflictLogic>().SetGlobal(global);
		if (global.GetComponent<Global>().GetOnlineGame()) {
			onlineMatch = true;
			defenseGame.GetComponent<DefenseLogic>().SetOnline();
			amIPlayer1 = global.GetComponent<Global>().GetAmIPlayer1();
			isOpponentReady = true;
			if (amIPlayer1) {
				defenseGame.GetComponent<DefenseLogic>().SetPlayer1();
				player1.GetComponent<SPlayer>().SetOnline();
				player2.GetComponent<SPlayer>().PutInHidePosition();
			} else {
				player2.GetComponent<SPlayer>().SetOnline();
				player1.GetComponent<SPlayer>().PutInHidePosition();
			}
		} else {
			player2.GetComponent<SPlayer>().PutInHidePosition();
		}
		if (global.GetComponent<Global>().GetTimeGame()) {
			timeMatch = true;
			if (onlineMatch) {
				time = global.GetComponent<Global>().GetOnlineTime();
			} else {
				time = global.GetComponent<Global>().GetTime();
			}
		} else {
			turnTimeText.GetComponent<GUIText>().text = "\u221E";
		}
		if (onlineMatch && timeMatch){
			turnTimeText.GetComponent<GUIText>().text = time.ToString();
			if (amIPlayer1) {
				StartCoroutine("TurnCountdownPlayer1");
			} else {
				StartCoroutine("TurnCountdownPlayer2");
			}
		}
		choosePhase = true;
		if(!onlineMatch){
			messageText.GetComponent<GUIText>().text= "Player 1 turn, tap for begin";
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
			EnableText();
			EndTurnPhase();
		}
	}

	private void pauseCheck() {
		if (Input.GetKeyDown(KeyCode.Escape)){
			if(!pauseGUI.activeSelf){
				Time.timeScale = 0;
				DisableText();
				oldMessage = messageText.GetComponent<GUIText>().text;
				messageText.GetComponent<GUIText>().text = "";
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
		EnableText();
		messageText.GetComponent<GUIText>().text = oldMessage;
		oldMessage = "";
		player1.SetActive(true);
		player2.SetActive(true);
		Time.timeScale = 1;
		/*if (!audio.isPlaying) audio.Play(); */		
	}
	
	private void LocalChoosePhase () {
		if (!player1Selected) {
			if(!tapPlayer1){
				if(Input.touches.Length == 1 && Input.GetTouch(0).phase == TouchPhase.Began && !pauseGUI.activeSelf){
					Debug.Log("tap rilevato");
					tapPlayer1=true;
					player1.GetComponent<SPlayer>().PutInShowPosition();
					messageText.GetComponent<GUIText>().text= "";
					if (timeMatch){
						turnTimeText.GetComponent<GUIText>().text = time.ToString();
						StartCoroutine("TurnCountdownPlayer1");
					}
				}
			} else if (player1.GetComponent<SPlayer>().GetHaveIChoosed()) {
				player1Move = player1.GetComponent<SPlayer>().GetSelectedMove();
				player1Selected = true;
				player1.GetComponent<SPlayer>().PutInHidePosition();
				messageText.GetComponent<GUIText>().text= "Player 2 turn, tap for begin";
			}
		} else {
			if (!tapPlayer2) {
				if(Input.touches.Length == 1 && Input.GetTouch(0).phase == TouchPhase.Began && !pauseGUI.activeSelf) {
					tapPlayer2=true;
					player2.GetComponent<SPlayer>().PutInShowPosition();
					messageText.GetComponent<GUIText>().text= "";
					if (timeMatch){
						turnTimeText.GetComponent<GUIText>().text = time.ToString();
						StartCoroutine("TurnCountdownPlayer2");
					}
				}
			} else if (!player2Selected) {
				if (player2.GetComponent<SPlayer>().GetHaveIChoosed()) {
					player2Move = player2.GetComponent<SPlayer>().GetSelectedMove();
					player2Selected = true;
					player2.GetComponent<SPlayer>().PutInHidePosition();
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
			if (player1.GetComponent<SPlayer>().GetHaveIChoosed()) {
				player1Selected = true;
				player1Move = player1.GetComponent<SPlayer>().GetSelectedMove();
				networkView.RPC("Player1Decision", RPCMode.Others, player1Move);
			}
		}
		if (!player2Selected) {
			if (player2.GetComponent<SPlayer>().GetHaveIChoosed()) {
				player2Move = player2.GetComponent<SPlayer>().GetSelectedMove();
				player2Selected = true;
			}
		}
		if (player1Selected && player2Selected) {
			isOpponentReady = false;
			choosePhase = false;
			StopCoroutine("TurnCountdown");
			StartCoroutine("MainFlow");
		}
	}
	
	private void OnlineP2ChoosePhase () {
		if (!player1Selected) {
			if (player1.GetComponent<SPlayer>().GetHaveIChoosed()) {
				player1Move = player1.GetComponent<SPlayer>().GetSelectedMove();
				player1Selected = true;
			}
		}
		if (!player2Selected) {
			if (player2.GetComponent<SPlayer>().GetHaveIChoosed()) {
				player2Selected = true;
				player2Move = player2.GetComponent<SPlayer>().GetSelectedMove();
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
		player1.GetComponent<SPlayer>().OnlineSelectMove(move);
	}
	
	[RPC] void Player2Decision (string move) {
		player2.GetComponent<SPlayer>().OnlineSelectMove(move);
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
						player1.GetComponent<SPlayer>().SetForcedMove();
					}
				} else {
					player1.GetComponent<SPlayer>().SetForcedMove();
				}
			}
			if (minigameResult.Equals("Player2")) {
				if (onlineMatch) {
					if (!amIPlayer1) {
						player2.GetComponent<SPlayer>().SetForcedMove();
					}
				} else {
					player2.GetComponent<SPlayer>().SetForcedMove();
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
		player1.GetComponent<SPlayer>().ShowSelectedMove();
		player2.GetComponent<SPlayer>().ShowSelectedMove();
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
			turnTimeText.GetComponent<GUIText>().text = countdown.ToString();
		}
		if (!player1Selected) {
			player1.GetComponent<SPlayer>().SetForcedMove();
		}
	}

	private IEnumerator TurnCountdownPlayer2 () {
		countdown = time;
		while (countdown > 0) {
			yield return new WaitForSeconds(1f);
			countdown --;
			turnTimeText.GetComponent<GUIText>().text = countdown.ToString();
		}
		if (!player2Selected) {
			player2.GetComponent<SPlayer>().SetForcedMove();
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
				DisableText();
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
				DisableText();
			}
		}
		animationLogic.GetComponent<AnimatorLogic>().SetMoves(player1Move, player2Move, outcome);
	}
	
	private void DisableText () {
		turnTimeText.SetActive(false);
		roundText.SetActive(false);
		p1WinnedRoundsText.SetActive(false);
		p2WinnedRoundsText.SetActive(false);
	}
	
	private void EnableText () {
		turnTimeText.SetActive(true);
		roundText.SetActive(true);
		p1WinnedRoundsText.SetActive(true);
		p2WinnedRoundsText.SetActive(true);
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
				messageText.GetComponent<GUIText>().text = "Player 1 wins the round!";
				animationLogic.GetComponent<AnimatorLogic>().EndRound("player2");
			} else if (player2Health > 0) {
				messageText.GetComponent<GUIText>().text = "Player 2 wins the round!";
				animationLogic.GetComponent<AnimatorLogic>().EndRound("player1");
			} else {
				messageText.GetComponent<GUIText>().text = "Round draw!";
				animationLogic.GetComponent<AnimatorLogic>().EndRound("draw");
			}
			yield return new WaitForSeconds(2f);
			messageText.GetComponent<GUIText>().text = "";
			p1WinnedRoundsText.GetComponent<GUIText>().text = player1WinnedRounds.ToString();
			p2WinnedRoundsText.GetComponent<GUIText>().text = player2WinnedRounds.ToString();
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
			player1.GetComponent<SPlayer>().NewTurn();
			player2.GetComponent<SPlayer>().NewTurn();		
		}
		player1Selected = false;
		player2Selected = false;
		if (onlineMatch) {
			if (timeMatch) {
				turnTimeText.GetComponent<GUIText>().text = time.ToString();
				if (amIPlayer1){
					StartCoroutine("TurnCountdownPlayer1");
				}
				else {
					StartCoroutine("TurnCountdownPlayer2");
				}
			}
		}
		else{
			messageText.GetComponent<GUIText>().text= "Player 1 turn, tap for begin";
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
		roundText.GetComponent<GUIText>().text = "Round " + round.ToString();
		player1Health = healthTotal;
		player2Health = healthTotal;
		UpdateHealthBar(player1HealthBar.GetComponent<SpriteRenderer>(), player1Health);
		UpdateHealthBar(player2HealthBar.GetComponent<SpriteRenderer>(), player2Health);
		player1.GetComponent<SPlayer>().NewRound();
		player2.GetComponent<SPlayer>().NewRound();
	}
	
	private IEnumerator EndMatch () {
		Debug.Log("entrato in EndMatch");
		StopCoroutine("EndTurnChecks");
		if (player1WinnedRounds > player2WinnedRounds) {
			messageText.GetComponent<GUIText>().text = "Player 1 wins the match!";
		} else if (player2WinnedRounds > player1WinnedRounds) {
			messageText.GetComponent<GUIText>().text = "Player 2 wins the match!";
		} else {
			messageText.GetComponent<GUIText>().text = "Match draw!";
		}
		yield return new WaitForSeconds(2f);		
		messageText.GetComponent<GUIText>().text = "";
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
