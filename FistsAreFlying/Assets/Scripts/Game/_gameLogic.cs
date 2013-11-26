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
		EmptyMove
	};

	public bool selectionPhase=false;
	public float player1Health=20;
	public float player2Health=20;
	public int normalDamage=2;

	int numberChoose;

	GameObject player1Choose;
	Move movePlayer1;
	Sprite spritePlayer1;
	GameObject player1Moves;


	GameObject player2Choose;
	Move movePlayer2;
	Sprite spritePlayer2;
	GameObject player2Moves;

	private int numberMoves=5;
	private int turn;
	private SpriteRenderer healthBar1;		
	private SpriteRenderer healthBar2;
	private Vector3 healthScale;
	private Sprite coverCard;

	Hashtable moves= new Hashtable();
	Hashtable rules= new Hashtable();
	ArrayList movesPlayer1= new ArrayList();
	ArrayList movesPlayer2= new ArrayList();


	// Use this for initialization
	void Start () {

		turn=1;



		player1Choose= GameObject.Find("Player1Choose");
		player1Moves= GameObject.Find ("Player1Moves");
		player2Choose= GameObject.Find("Player2Choose");
		player2Moves= GameObject.Find ("Player2Moves");

		coverCard=player1Choose.GetComponent<SpriteRenderer>().sprite;

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

		selectionPhase=true;

		numberChoose=0;
		healthBar1 = GameObject.Find("Player1HealthBar").GetComponent<SpriteRenderer>();
		healthBar2 = GameObject.Find("Player2HealthBar").GetComponent<SpriteRenderer>();
		healthScale = healthBar1.transform.localScale;
		

	}
	
	// Update is called once per frame
	void Update () {
	}



	public void setPlayerMove (string move, int player, Sprite sprite){
		if (player==1){
			movePlayer1= (Move) moves[move];
			movesPlayer1.Add(movePlayer1);
			spritePlayer1= sprite;
			Debug.Log(movePlayer1.ToString()+" Player1");
			numberChoose++;

		}
		else {
			movePlayer2= (Move) moves[move];
			movesPlayer2.Add (movePlayer2);
			spritePlayer2=sprite;
			Debug.Log(movePlayer2.ToString()+" Player2");
			numberChoose++;
		}
		if (numberChoose==2){
			numberChoose=0;
			selectionPhase=false;
			StartCoroutine(Waiting());
		}
	}

	IEnumerator Waiting(){
		Debug.Log("Fin qui ci siamo");
		yield return new WaitForSeconds(2);
		Debug.Log("Sono passati 2 secondi spero");
		flipCard ();
		yield return new WaitForSeconds(2);
		calculateDamage();
		yield return new WaitForSeconds(2);
		prepareNextTurn();

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
		selectionPhase=true;
		player1Moves.SetActive(true);
		player2Moves.SetActive(true);
		player1Choose.GetComponent<SpriteRenderer>().enabled=false;
		player2Choose.GetComponent<SpriteRenderer>().enabled=false;
		player1Choose.GetComponent<SpriteRenderer>().sprite= coverCard;
		player2Choose.GetComponent<SpriteRenderer>().sprite= coverCard;
		numberChoose=0;
	}

	void UpdateHealthBar ()
	{
		healthBar1.material.color = Color.Lerp(Color.green, Color.red, 1 - player1Health * 0.01f);
		healthBar1.transform.localScale = new Vector3(healthScale.x * player1Health * 0.05f, 1, 1);
		healthBar2.material.color = Color.Lerp(Color.green, Color.red, 1 - player2Health * 0.01f);
		healthBar2.transform.localScale = new Vector3(healthScale.x * player2Health * 0.05f, 1, 1);

	}



}
