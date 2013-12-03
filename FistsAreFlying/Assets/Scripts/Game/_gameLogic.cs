using UnityEngine;
using System.Collections;

public class _gameLogic : MonoBehaviour {
	enum Move
	{
		LeftKick,
		LeftPunch,
		RightPunch,
		RightKick,
		Defense,
		EmptyMove,
		NoChoose
	};


	public bool selectionPhase=false;
	public float player1Health=8;
	public float player2Health=8;
	public int normalDamage=2;
	public float countdownValue;

	private int numberChoose;

	private GameObject globalObject;
	private GameObject countdownText;
	private GameObject player1Choose;
	private Animator animatorPlayer1;
	private Animator animatorPlayer2;
	private Move movePlayer1;
	private Sprite spritePlayer1;
	private GameObject player1Moves;
	private bool isTurnTimeGame;

	private GameObject player2Choose;
	private Move movePlayer2;
	private Sprite spritePlayer2;
	private GameObject player2Moves;

	private float healthUnit;
	static private int numberMoves=5;
	private int turn;
	private int moveCount1,moveCount2;
	private SpriteRenderer healthBar1;		
	private SpriteRenderer healthBar2;
	private Vector3 healthScale;
	private Sprite coverCard;
	private Sprite emptyMove;

	private Hashtable moves= new Hashtable();
	private Hashtable rules= new Hashtable();
	private string[] movesPlayer1= new string[numberMoves];
	private string[] movesPlayer2= new string[numberMoves];
	private float countdown;
	private bool chooseActive;
	private bool roundTerminato;
	private bool partitaTerminata;

	private int round;

	// Use this for initialization
	void Start () {
	
		inizializeVariables();
		inizializeGameObject();
		inizializeHashTables();
		if(isTurnTimeGame){
			StartCoroutine(StartCountdown());
		}
		else {
			countdownText.GetComponent<GUIText>().text= "";
		}
	}

	void inizializeVariables(){	
		partitaTerminata=false;
		roundTerminato=false;
		chooseActive=true;
		turn=1;
		healthUnit=1f/player1Health;
		
		moveCount1=0;
		moveCount2=0;
		movePlayer1=(Move) Move.NoChoose;
		movePlayer2=(Move) Move.NoChoose;
		
		selectionPhase=true;
		
		numberChoose=0;
		
	}

	void inizializeGameObject(){
		
		countdownText= GameObject.Find("Countdown");

		globalObject=GameObject.Find("GlobalObject");
		isTurnTimeGame= globalObject.GetComponent<GlobalObject>().turnTimeGame;
		round=globalObject.GetComponent<GlobalObject>().round;
		if(round==1){
			countdownValue= globalObject.GetComponent<GlobalObject>().turnTimeRound1;
		} else if(round==2){
			countdownValue= globalObject.GetComponent<GlobalObject>().turnTimeRound2;
		} else if (round==3){
			countdownValue= globalObject.GetComponent<GlobalObject>().turnTimeRound3;
		}
		player1Choose= GameObject.Find("Player1Choose");
		player1Moves= GameObject.Find ("Player1Moves");
		animatorPlayer1= GameObject.Find ("Player1").GetComponent<Animator>();
		player2Choose= GameObject.Find("Player2Choose");
		player2Moves= GameObject.Find ("Player2Moves");
		animatorPlayer2= GameObject.Find ("Player2").GetComponent<Animator>();
		
		coverCard=player1Choose.GetComponent<SpriteRenderer>().sprite;
		emptyMove= GameObject.Find("EmptySprite").GetComponent<SpriteRenderer>().sprite;

		healthBar1 = GameObject.Find("Player1HealthBar").GetComponent<SpriteRenderer>();
		healthBar2 = GameObject.Find("Player2HealthBar").GetComponent<SpriteRenderer>();
		healthScale = healthBar1.transform.localScale;
		
		
	}


	void inizializeHashTables(){
	
		moves.Add("Left Kick",Move.LeftKick);
		moves.Add("Right Kick",Move.RightKick);
		moves.Add("Left Punch",Move.LeftPunch);
		moves.Add("Right Punch",Move.RightPunch);
		moves.Add("Defense",Move.Defense);
		moves.Add("Empty Move",Move.EmptyMove);
		
		rules.Add (Move.LeftKick.ToString()+Move.LeftPunch.ToString(), "player1");
		rules.Add (Move.LeftPunch.ToString()+Move.RightKick.ToString(), "player1");
		rules.Add (Move.RightKick.ToString ()+Move.RightPunch.ToString(), "player1");
		rules.Add (Move.RightPunch.ToString()+Move.LeftKick.ToString (), "player1");
		rules.Add (Move.LeftPunch.ToString()+Move.LeftKick.ToString(), "player2");
		rules.Add (Move.RightKick.ToString()+Move.LeftPunch.ToString(), "player2");
		rules.Add (Move.RightPunch.ToString ()+Move.RightKick.ToString(), "player2");
		rules.Add (Move.LeftKick.ToString()+Move.RightPunch.ToString (), "player2");
		rules.Add (Move.LeftKick.ToString()+Move.RightKick.ToString(), "scontro");
		rules.Add (Move.RightKick.ToString()+Move.LeftKick.ToString(), "scontro");
		rules.Add (Move.RightPunch.ToString()+Move.LeftPunch.ToString(),"scontro");
		rules.Add (Move.LeftPunch.ToString()+Move.RightPunch.ToString(),"scontro");
	}
	
	// Update is called once per frame
	void Update () {
		checkGameState();
		if(chooseActive==false && roundTerminato==false && partitaTerminata==false)
		{
			StartCoroutine(mainFlow());
			chooseActive=true;
		}
	}



	private void checkGameState(){
	
		if(Input.GetMouseButton(0) && roundTerminato){
			Application.LoadLevel("GameNormal");
		}
		if(Input.GetMouseButton(0) && partitaTerminata){
			Application.LoadLevel("MainMenu");
		}
		if(!roundTerminato && !partitaTerminata){
			if(player1Health<=0 && player2Health<=0){
				animatorPlayer1.SetBool("isDeath",true);
				animatorPlayer2.SetBool("isDeath",true);
				roundTerminato=true;
				StopAllCoroutines();
				countdownText.GetComponent<GUIText>().text= "PAREGGIO!";
				globalObject.GetComponent<GlobalObject>().round++;
				globalObject.GetComponent<GlobalObject>().roundWinPlayer2++;
				globalObject.GetComponent<GlobalObject>().roundWinPlayer1++;
			} else if(player1Health<=0){
				animatorPlayer1.SetBool("isDeath",true);
				StopAllCoroutines();
				countdownText.GetComponent<GUIText>().text= "PLAYER2 VINCE IL ROUND!";
				globalObject.GetComponent<GlobalObject>().round++;
				globalObject.GetComponent<GlobalObject>().roundWinPlayer2++;
				
			} else if(player2Health<=0){
				animatorPlayer2.SetBool("isDeath",true);
				roundTerminato=true;
				StopAllCoroutines();
				countdownText.GetComponent<GUIText>().text= "PLAYER1 VINCE IL ROUND!";
				globalObject.GetComponent<GlobalObject>().round++;
				globalObject.GetComponent<GlobalObject>().roundWinPlayer1++;
			}
		}
		if(globalObject.GetComponent<GlobalObject>().roundWinPlayer2==2){
			roundTerminato=false;
			partitaTerminata=true;
			countdownText.GetComponent<GUIText>().text= "PLAYER2 VINCE LA PARTITA!";
		} else if(globalObject.GetComponent<GlobalObject>().roundWinPlayer1==2){
			roundTerminato=false;
			partitaTerminata=true;
			countdownText.GetComponent<GUIText>().text= "PLAYER1 VINCE LA PARTITA!";
		}
	}



	public void setPlayerMove (string move, int player, Sprite sprite){
			if (player==1 && chooseActive){
				movePlayer1= (Move) moves[move];
				if(!(movePlayer1==Move.EmptyMove))
				{
					movesPlayer1[moveCount1]=move;
					moveCount1++;
				}
				spritePlayer1= sprite;
				Debug.Log(movePlayer1.ToString()+" Player1");
				numberChoose++;

			}
			if(player==2 && chooseActive) {
				movePlayer2= (Move) moves[move];
				if(!(movePlayer2==Move.EmptyMove))
				{
					movesPlayer1[moveCount2]=move;
					moveCount2++;
				}
				spritePlayer2=sprite;
				Debug.Log(movePlayer2.ToString()+" Player2");
				numberChoose++;
			}
		if(numberChoose==2){
			chooseActive=false;
			selectionPhase=false;	
			StopAllCoroutines();
		}
	}

	void playerMoveChosen (){
		if (movePlayer1==Move.NoChoose)
			{
				Debug.Log("entrato");
				player1Moves.SetActive(false);
				player1Choose.GetComponent<SpriteRenderer>().enabled=true;
				movePlayer1=(Move) Move.EmptyMove;
				spritePlayer1=emptyMove;
			}
		if (movePlayer2==Move.NoChoose)
			{
				player2Moves.SetActive(false);
				player2Choose.GetComponent<SpriteRenderer>().enabled=true;
				movePlayer2=(Move) Move.EmptyMove;
				spritePlayer2=emptyMove;
			}
		Debug.Log(movePlayer1.ToString());
		Debug.Log (movePlayer2.ToString());
		selectionPhase=false;	
		chooseActive=false;
	}

	IEnumerator StartCountdown()
	{
		countdown = countdownValue;
		while (countdown >0)
		{
			yield return new WaitForSeconds(1.0f);
			countdown --;
			countdownText.GetComponent<GUIText>().text= countdown.ToString();
		}
		playerMoveChosen();
	}
	


	IEnumerator mainFlow(){

		yield return new WaitForSeconds(2);
		flipCard ();
		startAnimation();
		yield return new WaitForSeconds(2);
		calculateDamage();
		UpdateHealthBar(healthBar1, player1Health);
		UpdateHealthBar(healthBar2, player2Health);
		//yield return new WaitForSeconds(2);
		prepareNextTurn();

	}

	void startAnimation(){
		if(movePlayer1==Move.LeftPunch){
			animatorPlayer1.SetTrigger("isLeftPunch");
		}
		else if(movePlayer1==Move.RightPunch){
			animatorPlayer1.SetTrigger("isRightPunch");
		}
		else if(movePlayer1==Move.EmptyMove){
			animatorPlayer1.SetTrigger("isEmptyMove");
		}
		else if (movePlayer1==Move.LeftKick){
			animatorPlayer1.SetTrigger("isLeftKick");
		}
		else if (movePlayer1==Move.RightKick){
			animatorPlayer1.SetTrigger("isRightKick");
		}
		else if (movePlayer1==Move.Defense){
			animatorPlayer1.SetTrigger("isDefense");
		}
		if(movePlayer2==Move.LeftPunch){
			animatorPlayer2.SetTrigger("isLeftPunch");
		}
		else if(movePlayer2==Move.RightPunch){
			animatorPlayer2.SetTrigger("isRightPunch");
		}
		else if(movePlayer2==Move.EmptyMove){
			animatorPlayer2.SetTrigger("isEmptyMove");
		}
		else if (movePlayer2==Move.LeftKick){
			animatorPlayer2.SetTrigger("isLeftKick");
		}
		else if (movePlayer2==Move.RightKick){
			animatorPlayer2.SetTrigger("isRightKick");
		}
		else if (movePlayer2==Move.Defense){
			animatorPlayer2.SetTrigger("isDefense");
		}
	}


	void flipCard () {
		player1Choose.GetComponent<SpriteRenderer>().sprite=spritePlayer1;
		player2Choose.GetComponent<SpriteRenderer>().sprite=spritePlayer2;
	}

	void calculateDamage() {
		string result;

		if( movePlayer1.Equals(movePlayer2)){
			if(!movePlayer1.Equals(Move.EmptyMove) && !movePlayer1.Equals(Move.Defense))
			{
				player1Health= player1Health- normalDamage;
				player2Health= player2Health- normalDamage;
			}
		}
		else if((movePlayer1.Equals(Move.Defense) && !movePlayer2.Equals(Move.EmptyMove)) || (movePlayer2.Equals (Move.Defense) && !movePlayer1.Equals(Move.EmptyMove))){
			//chiamo minigioco difesa e vedo se far saltare turno
		}
		else if(movePlayer1.Equals(Move.EmptyMove) && !(movePlayer2.Equals (Move.EmptyMove)|| movePlayer2.Equals(Move.Defense))){
			player1Health= player1Health - normalDamage;
		}
		else if(movePlayer2.Equals(Move.EmptyMove) && !(movePlayer1.Equals (Move.EmptyMove)|| movePlayer1.Equals(Move.Defense))){
			player2Health= player2Health - normalDamage;
		}
		else if((movePlayer1.Equals(Move.Defense) && movePlayer2.Equals(Move.EmptyMove)) || (movePlayer2.Equals (Move.Defense) && movePlayer1.Equals(Move.EmptyMove))){
			//nessuno subisce danno
		}
		else {
			result= (string) rules[movePlayer1.ToString()+ movePlayer2.ToString()];
			if (result.Equals("player1")){
				player1Health= player1Health- (normalDamage/2);
				player2Health= player2Health- normalDamage;
			} 
			else if (result.Equals("player2")){
				player1Health= player1Health- normalDamage;
				player2Health= player2Health- (normalDamage/2);
			}
			else if (result.Equals("scontro")){
				//chiamo minigioco scontro e aggiorno la vita
			}
		}

	}


	void prepareNextTurn(){
		player1Moves.SetActive(true);
		if(moveCount1==5){
			enableSprite(player1Moves.GetComponentsInChildren<SpriteRenderer>());
			enableCollider(player1Moves.GetComponentsInChildren<BoxCollider2D>());
			moveCount1=0;
		}
		player2Moves.SetActive(true);
		if(moveCount2==5){
			enableSprite(player2Moves.GetComponentsInChildren<SpriteRenderer>());
			enableCollider (player2Moves.GetComponentsInChildren<BoxCollider2D>());
			moveCount2=0;
		}
		player1Choose.GetComponent<SpriteRenderer>().enabled=false;
		player2Choose.GetComponent<SpriteRenderer>().enabled=false;
		player1Choose.GetComponent<SpriteRenderer>().sprite= coverCard;
		player2Choose.GetComponent<SpriteRenderer>().sprite= coverCard;
		numberChoose=0;
		movePlayer1=(Move) Move.NoChoose;
		movePlayer2=(Move) Move.NoChoose;
		if(isTurnTimeGame){
			StartCoroutine(StartCountdown());
			countdownText.GetComponent<GUIText>().text="TIME";
		}
		selectionPhase=true;
	}

	private void enableSprite ( SpriteRenderer[] ourRenderer)
	{
		foreach(SpriteRenderer renderer in ourRenderer)
		{
			renderer.enabled = true;
		}
	}

	private void enableCollider (BoxCollider2D[] ourCollider){
		foreach(BoxCollider2D collider in ourCollider)
		{
			collider.enabled = true;
		}
	}

	void UpdateHealthBar (SpriteRenderer healthBar, float playerHealth)
	{
	
		healthBar.material.color = Color.Lerp(Color.green, Color.red, 1 - playerHealth * healthUnit);
		healthBar.transform.localScale = new Vector3(healthScale.x * playerHealth * healthUnit, healthBar.transform.localScale.y, healthBar.transform.localScale.z);
	}



}
