using UnityEngine;
using System.Collections;

public class MoveSelectedPlayer1 : MonoBehaviour {
	GameObject player1Choose;
	_gameLogic gameLogic;
	GameObject player1Moves;
	// Use this for initialization
	void Start () {
		player1Choose= GameObject.Find("Player1Choose");
		gameLogic = GameObject.Find("GameLogic").GetComponent<_gameLogic>();
		player1Moves= GameObject.Find("Player1Moves");
	}
	
	void Update () {
	}

	void OnMouseDown () {
		if (gameLogic.selectionPhase){
			player1Choose.GetComponent<SpriteRenderer>().enabled=true;
			if(tag!="Empty Move")
			{
				GetComponent<SpriteRenderer>().enabled=false;
				GetComponent<BoxCollider2D>().enabled=false;
			}
			player1Moves.SetActive(false);
			gameLogic.setPlayerMove(this.tag.ToString(), 1, this.GetComponent<SpriteRenderer>().sprite);
		}
	}

}
