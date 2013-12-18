using UnityEngine;
using System.Collections;

public class ConflictLogic : MonoBehaviour {

	public GameObject player1;
	public GameObject player2;
	public GameObject result;
	public GameObject text;

	private int drawOffset = 3;
	private bool isLegal = false;
	private int randomRange = 0;
	private string player1Move = "";
	private string player2Move = "";
	private bool player1Tapped = false;
	private bool player2Tapped = false;
	private int player1Time = 0;
	private int player2Time = 0;
	private Vector3 touchPosition = Vector3.zero;
	private 

	// Use this for initialization
	void Start () {}
	
	// Update is called once per frame
	void Update () {
		if (!player1Tapped || !player2Tapped) {
			CheckPlayers();
		} else {
			StopCoroutine("Countdown");
			ApplyRules();
		}
	}

	private void CheckPlayers () {
		foreach (Touch touch in Input.touches) {
			touchPosition = Camera.main.ScreenToWorldPoint(new Vector3(touch.position.x, touch.position.y, 10));
			if (touchPosition.x < this.transform.position.x && !player1Tapped) {
				player1Tapped = true;
				player1.GetComponent<PlayerConflict>().ShowMove(player1Move);
				if (isLegal) {
					player1Time = Time.frameCount;
				} else {
					player2Tapped = true;
					player1Time = 1000;
					player2Time = 1;
				}
			}
			if (touchPosition.x > this.transform.position.x && !player2Tapped) {
				player2Tapped = true;
				player2.GetComponent<PlayerConflict>().ShowMove(player2Move);
				if(isLegal) {
					player2Time = Time.frameCount;
				} else {
					player1Tapped = true;
					player1Time = 1;
					player2Time = 1000;
				}
			}
		}
	}

	private void ApplyRules () {
		if (player1Time > player2Time + drawOffset) {
			result.GetComponent<ConflictResult>().SetResult("player2");
			text.GetComponent<GUIText>().text = "Player 2 wins!";
		} else if (player1Time + drawOffset < player2Time) {
			result.GetComponent<ConflictResult>().SetResult("player1");
			text.GetComponent<GUIText>().text = "PLayer 1 wins!";
		} else {
			result.GetComponent<ConflictResult>().SetResult("draw");
			text.GetComponent<GUIText>().text = "Draw";
		}
		StartCoroutine("ConflictEnd");
	}

	private IEnumerator ConflicEnd () {
		yield return new WaitForSeconds(2f);
		isLegal = false;
		player1Tapped = false;
		player2Tapped = false;
		Camera.main.transform.position = Vector3.zero;
		player1.GetComponent<PlayerConflict>().ResetMoves();
		player2.GetComponent<PlayerConflict>().ResetMoves();
		text.GetComponent<GUIText>().text = "";
		this.gameObject.SetActive(false);
	}

	public void StartCountdown () {
		randomRange = Random.Range(2, 8);
		isLegal = false;
		StartCoroutine("Countdown");
	}

	private IEnumerator Countdown () {
		yield return new WaitForSeconds(randomRange);
		isLegal = true;
		text.GetComponent<GUIText>().text = "Go!";
	}

	public void SetPlayerMoves (string player1Move, string player2Move) {
		this.player1Move = player1Move;
		this.player2Move = player2Move;
	}
}
