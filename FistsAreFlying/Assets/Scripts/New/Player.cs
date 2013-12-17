using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Player : MonoBehaviour {

	public GameObject punchRight;
	public GameObject punchLeft;
	public GameObject kickRight;
	public GameObject kickLeft;
	public GameObject defense;
	public GameObject emptyMove;
	public GameObject choosedMove;
	private Sprite coveredMoveSprite;

	private bool haveIChoosed = false;
	private string selectedMove = "";
	private IList<GameObject> alreadyUsedMoves = new List<GameObject>();
	private IList<GameObject> notYetUsedMoves =  new List<GameObject>();
	private IList<GameObject> touchedMoves = new List<GameObject>();

	// Use this for initialization
	void Start () {
		ResetMoves();
		notYetUsedMoves.Add(emptyMove);
		coveredMoveSprite = choosedMove.GetComponent<SpriteRenderer>().sprite;
	}

	private void  ResetMoves () {
		if (alreadyUsedMoves.Count > 0) {
			alreadyUsedMoves.Clear();
		}
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
			if (notYetUsedMoves.Contains(punchRight) && punchRight.GetComponent<Move>().getAmISelected()) {
				selectedMove = "PR";
				touchedMoves.Add(punchRight);
			}
			if (notYetUsedMoves.Contains(punchLeft) && punchLeft.GetComponent<Move>().getAmISelected()) {
				selectedMove = "PL";
				touchedMoves.Add(punchLeft);
			}
			if (notYetUsedMoves.Contains(kickRight) && kickRight.GetComponent<Move>().getAmISelected()) {
				selectedMove = "KR";
				touchedMoves.Add(kickRight);
			}
			if (notYetUsedMoves.Contains(kickLeft) && kickLeft.GetComponent<Move>().getAmISelected()) {
				selectedMove = "KL";
				touchedMoves.Add(kickLeft);
			}
			if (notYetUsedMoves.Contains(defense) && defense.GetComponent<Move>().getAmISelected()) {
				selectedMove = "D";
				touchedMoves.Add(defense);
			}
			if (emptyMove.GetComponent<Move>().getAmISelected()) {
				selectedMove = "EM";
				touchedMoves.Add(emptyMove);
			}
			if (touchedMoves.Count > 1) {
				selectedMove = "";
			} else if (touchedMoves.Count > 0) {
				SelectMove(touchedMoves[0]);
			}
		}
	}

	private void SelectMove (GameObject move) {
		haveIChoosed = true;
		foreach (GameObject notYetUsedMove in notYetUsedMoves) {
			notYetUsedMove.GetComponent<BoxCollider2D>().enabled = false;
			notYetUsedMove.GetComponent<SpriteRenderer>().enabled = false;
		}
		if (!selectedMove.Equals("EM")) {
			notYetUsedMoves.Remove(move);
			alreadyUsedMoves.Add(move);
		}
	}

	public void PrepareSelectedMove () {
		foreach (GameObject notYetUsedMove in notYetUsedMoves) {
			notYetUsedMove.GetComponent<SpriteRenderer>().enabled = false;
		}
		foreach (GameObject alreadyUsedMove in alreadyUsedMoves) {
			alreadyUsedMove.GetComponent<SpriteRenderer>().enabled = false;
		}
		choosedMove.GetComponent<SpriteRenderer>().enabled = true;
	}

	public void ShowSelectedMove () {
		choosedMove.GetComponent<SpriteRenderer>().sprite = touchedMoves[0].GetComponent<SpriteRenderer>().sprite;
	}

	public void NewTurn () {
		if (notYetUsedMoves.Count == 1 && notYetUsedMoves[0].Equals(emptyMove)) {
			foreach (GameObject alreadyUsedMove in alreadyUsedMoves) {
				alreadyUsedMoves.Remove(alreadyUsedMove);
				notYetUsedMoves.Add(alreadyUsedMove);
			}
		}
		choosedMove.GetComponent<SpriteRenderer>().sprite = coveredMoveSprite;	foreach (GameObject notYetUsedMove in notYetUsedMoves) {
			notYetUsedMove.GetComponent<SpriteRenderer>().enabled = true;
			notYetUsedMove.GetComponent<BoxCollider2D>().enabled = true;
		}
		haveIChoosed = false;
	}

	public bool GetHaveIChoosed () {
		return haveIChoosed;
	}

	public string GetSelectedMove () {
		return selectedMove;
	}
}
