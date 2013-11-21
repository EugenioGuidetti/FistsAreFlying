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
	
	// Update is called once per frame
	//void Update () {
	//	if (Input.GetMouseButtonDown(0) && gameLogic.selectionPhase)
	//	{
			//Creating container for the raycast result
	//		RaycastHit hitInfo = new RaycastHit();
			//Making the raycast
			//if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hitInfo))
		//	{
		//		if (hitInfo.transform.tag=="LeftPunch")
		//		{
		//			player1Choose.GetComponent<SpriteRenderer>().enabled=true;
		//		}
		//	}
	//	}

	//}
	void OnMouseDown () {
		if (gameLogic.selectionPhase){
			player1Choose.GetComponent<SpriteRenderer>().enabled=true;
			enabled=false;
			player1Moves.SetActive(false);
			gameLogic.setPlayerMove(this.tag.ToString(), 1, this.GetComponent<SpriteRenderer>().sprite);
	
		}
	}

}
