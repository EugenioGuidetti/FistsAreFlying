using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SPlayer : MonoBehaviour {
	
	private bool amIOnline = false;

	public GameObject moves;
	public GameObject punchRight;
	public GameObject punchLeft;
	public GameObject kickRight;
	public GameObject kickLeft;
	public GameObject defense;
	public GameObject emptyMove;
	public GameObject choosedMove;
	
	private bool haveIChoosed = false;
	private bool forcedMove = false;
	private string selectedMove = "";
	private IList<GameObject> alreadyUsedMoves = new List<GameObject>();
	private IList<GameObject> notYetUsedMoves =  new List<GameObject>();
	private IList<GameObject> touchedMoves = new List<GameObject>();

	private Vector3 showPosition = new Vector3 (0,0,0);
	private Vector3 hidePosition = new Vector3 (7,0,0);

	// Use this for initialization
	void Awake () {
		ResetMoves();
		notYetUsedMoves.Add(emptyMove);
	}
	
	private void  ResetMoves () {
		if (alreadyUsedMoves.Count > 0) {
			alreadyUsedMoves.Clear();
		}
		notYetUsedMoves.Clear();
		notYetUsedMoves.Add(punchRight);
		notYetUsedMoves.Add(punchLeft);
		notYetUsedMoves.Add(kickRight);
		notYetUsedMoves.Add(kickLeft);
		notYetUsedMoves.Add(defense);
	}
	
	// Update is called once per frame
	void Update () {
		if (!haveIChoosed) {
			touchedMoves.Clear();
			if (!forcedMove) {
				if (notYetUsedMoves.Contains(punchRight) && punchRight.GetComponent<SMove>().getAmISelected()) {
					selectedMove = "PR";
					touchedMoves.Add(punchRight);
				}
				if (notYetUsedMoves.Contains(punchLeft) && punchLeft.GetComponent<SMove>().getAmISelected()) {
					selectedMove = "PL";
					touchedMoves.Add(punchLeft);
				}
				if (notYetUsedMoves.Contains(kickRight) && kickRight.GetComponent<SMove>().getAmISelected()) {
					selectedMove = "KR";
					touchedMoves.Add(kickRight);
				}
				if (notYetUsedMoves.Contains(kickLeft) && kickLeft.GetComponent<SMove>().getAmISelected()) {
					selectedMove = "KL";
					touchedMoves.Add(kickLeft);
				}
				if (notYetUsedMoves.Contains(defense) && defense.GetComponent<SMove>().getAmISelected()) {
					selectedMove = "D";
					touchedMoves.Add(defense);
				}
				if (emptyMove.GetComponent<SMove>().getAmISelected()) {
					selectedMove = "EM";
					touchedMoves.Add(emptyMove);
				}
				if (touchedMoves.Count > 1) {
					selectedMove = "";
				} else if (touchedMoves.Count > 0) {
					SelectMove(touchedMoves[0]);
				}
			} else {
				selectedMove = "EM";
				touchedMoves.Add(emptyMove);
				SelectMove(emptyMove);
				forcedMove = false;
			}
		}
	}
	
	private void SelectMove (GameObject move) {
		haveIChoosed = true;
		if (!selectedMove.Equals("EM")) {
			notYetUsedMoves.Remove(move);
			alreadyUsedMoves.Add(move);
		}
		foreach (GameObject notYetUsedMove in notYetUsedMoves) {
			notYetUsedMove.GetComponent<BoxCollider2D>().enabled = false;
			if (!amIOnline) {
				notYetUsedMove.GetComponent<SpriteRenderer>().enabled = false;
			}
		}
		foreach (GameObject alreadyUsedMove in alreadyUsedMoves) {
			alreadyUsedMove.GetComponent<BoxCollider2D>().enabled = false;
			if (!amIOnline) {
				alreadyUsedMove.GetComponent<SpriteRenderer>().enabled = false;
			}
		}
		choosedMove.GetComponent<SpriteRenderer>().enabled = true;
	}


	public void OnlineSelectMove (string move) {
		selectedMove = move;
		if (selectedMove.Equals("PR")) {
			touchedMoves.Add(punchRight);
		}
		if (selectedMove.Equals("PL")) {
			touchedMoves.Add(punchLeft);
		}
		if (selectedMove.Equals("KR")) {
			touchedMoves.Add(kickRight);
		}
		if (selectedMove.Equals("KL")) {
			touchedMoves.Add(kickLeft);
		}
		if (selectedMove.Equals("D")) {
			touchedMoves.Add(defense);
		}
		if (selectedMove.Equals("EM")) {
			touchedMoves.Add(emptyMove);
		}
		SelectMove(touchedMoves[0]);
	}

	public void ShowSelectedMove () {
		choosedMove.GetComponent<SpriteRenderer>().sprite = touchedMoves[0].GetComponent<SpriteRenderer>().sprite;
		if (touchedMoves[0] != emptyMove){
			touchedMoves[0].GetComponent<SMove>().UseMove();
		}

	}
	
	public void NewTurn () {
		if (notYetUsedMoves.Count == 1 && notYetUsedMoves[0].Equals(emptyMove)) {
			ResetMoves();
			foreach (GameObject notYetUsedMove in notYetUsedMoves){
				notYetUsedMove.GetComponent<SMove>().ResetMove();
			}
			notYetUsedMoves.Add(emptyMove);
		}
		choosedMove.GetComponent<SChoosedMove>().ResetSprite();
		choosedMove.GetComponent<SpriteRenderer>().enabled = false;
		foreach (GameObject notYetUsedMove in notYetUsedMoves) {
			notYetUsedMove.GetComponent<SpriteRenderer>().enabled = true;
			notYetUsedMove.GetComponent<BoxCollider2D>().enabled = true;
		}
		if (alreadyUsedMoves.Count > 0){
			foreach (GameObject alreadyUsedMove in alreadyUsedMoves) {
				alreadyUsedMove.GetComponent<SpriteRenderer>().enabled = true;
			}
		}

		haveIChoosed = false;
	}
	
	public void NewRound () {
		ResetMoves();
		foreach (GameObject notYetUsedMove in notYetUsedMoves){
			notYetUsedMove.GetComponent<SMove>().ResetMove();
		}
		notYetUsedMoves.Add(emptyMove);
		choosedMove.GetComponent<SChoosedMove>().ResetSprite();
		choosedMove.GetComponent<SpriteRenderer>().enabled = false;
		foreach (GameObject notYetUsedMove in notYetUsedMoves) {
			notYetUsedMove.GetComponent<SpriteRenderer>().enabled = true;
			notYetUsedMove.GetComponent<BoxCollider2D>().enabled = true;
		}
		haveIChoosed = false;
	}
	
	public bool GetHaveIChoosed () {
		return haveIChoosed;
	}
	
	public void SetForcedMove () {
		forcedMove = true;
	}
	
	public string GetSelectedMove () {
		return selectedMove;
	}
	
	public void SetOnline () {
		amIOnline = true;
	}

	public void PutInShowPosition () {
		moves.transform.position = showPosition;
		foreach (GameObject notYetUsedMove in notYetUsedMoves) {
			notYetUsedMove.GetComponent<SMove>().SetPosition();
		}
		foreach (GameObject alreadyUsedMove in alreadyUsedMoves) {
			alreadyUsedMove.GetComponent<SMove>().SetPosition();
		}
	}

	public void PutInHidePosition () {
		moves.transform.position = hidePosition;
		foreach (GameObject notYetUsedMove in notYetUsedMoves) {
			notYetUsedMove.GetComponent<SMove>().SetPosition();
		}
		foreach (GameObject alreadyUsedMove in alreadyUsedMoves) {
			alreadyUsedMove.GetComponent<SMove>().SetPosition();
		}
	}
}
