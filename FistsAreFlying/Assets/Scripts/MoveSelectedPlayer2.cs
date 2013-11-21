using UnityEngine;
using System.Collections;

public class MoveSelectedPlayer2 : MonoBehaviour {
	GameObject player2Choose;
	_gameLogic gameLogic;
	GameObject player2Moves;
	// Use this for initialization
	void Start () {
		player2Choose= GameObject.Find("Player2Choose");
		gameLogic = GameObject.Find("GameLogic").GetComponent<_gameLogic>();
		player2Moves= GameObject.Find("Player2Moves");
	}
	
	// Update is called once per frame
	void Update () {
	}

	void OnMouseDown() {
		if (gameLogic.selectionPhase){
			player2Choose.GetComponent<SpriteRenderer>().enabled=true;
			player2Moves.SetActive(false);
			gameLogic.setPlayerMove(this.tag.ToString(), 2, this.GetComponent<SpriteRenderer>().sprite);
		}
	}
	
}
