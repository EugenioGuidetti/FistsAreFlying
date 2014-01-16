﻿using UnityEngine;
using System.Collections;

public class ConflictLogic : MonoBehaviour {

	private GameObject global;
	public GameObject player1;
	public GameObject player2;
	public GameObject result;
	public GameObject text;

	private float drawOffset = 0.025f;
	private bool isLegal = false;
	private int randomRange = 0;
	private string player1Move = "";
	private string player2Move = "";
	private bool player1Tapped = false;
	private bool player2Tapped = false;
	private float player1Time = 0f;
	private float player2Time = 0f;
	private float startTime = 0f;
	private float legalTime = 0f;
	private Vector3 touchPosition = Vector3.zero;
	private 

	// Use this for initialization
	void Start () {}

	public void SetGlobal (GameObject global) {
		this.global = global;
	}
	
	// Update is called once per frame
	void Update () {
		if (!player1Tapped || !player2Tapped) {
			if (!global.GetComponent<Global>().GetOnlineGame()) {
				LocalCheck();
			} else {
				if (global.GetComponent<Global>().GetAmIPlayer1()) {
					P1OnlineCheck();
				} else {
					P2OnlineCheck();
				}
			}
		} else {
			StopCoroutine("Countdown");
			if (!global.GetComponent<Global>().GetOnlineGame()) {
				LocalApplyRules();
			} else {
				OnlineApplyRules();
			}
		}
	}

	private void LocalCheck () {
		foreach (Touch touch in Input.touches) {
			touchPosition = Camera.main.ScreenToWorldPoint(new Vector3(touch.position.x, touch.position.y, 10));
			if (touchPosition.x < this.transform.position.x && !player1Tapped) {
				player1Tapped = true;
				player1.GetComponent<PlayerConflict>().ShowMove(player1Move);
				if (isLegal) {
					player1Time = Time.time - startTime;
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
					player2Time = Time.time - startTime;
				} else {
					player1Tapped = true;
					player1Time = 1;
					player2Time = 1000;
				}
			}
		}
	}

	private void P1OnlineCheck () {
		if (!player1Tapped) {
			foreach (Touch touch in Input.touches) {
				touchPosition = Camera.main.ScreenToWorldPoint(new Vector3(touch.position.x, touch.position.y, 10));
				if (touchPosition.x < this.transform.position.x) {
					player1.GetComponent<PlayerConflict>().ShowMove(player1Move);
					player1Time = Time.time - startTime;
					networkView.RPC("Player1Timing", RPCMode.Others, player1Time);
					player1Tapped = true;
				}
			}
		}
	}


	private void P2OnlineCheck () {
		if (!player2Tapped) {
			foreach (Touch touch in Input.touches) {
				touchPosition = Camera.main.ScreenToWorldPoint(new Vector3(touch.position.x, touch.position.y, 10));
				if (touchPosition.x > this.transform.position.x) {
					player2.GetComponent<PlayerConflict>().ShowMove(player2Move);
					player2Time = Time.time - startTime;
					networkView.RPC("Player2Timing", RPCMode.Server, player2Time);
					player2Tapped = true;
				}
			}
		}	
	}

	[RPC] void Player1Timing (float tapTime) {
		player1Time = tapTime;
		player1Tapped = true;
	}

	[RPC] void Player2Timing (float tapTime) {
		player2Time = tapTime;
		player2Tapped = true;
	}

	private void LocalApplyRules () {
		if (player1Time > player2Time + drawOffset) {
			result.GetComponent<ConflictResult>().SetResult("player2");
			text.GetComponent<GUIText>().text = "Player 2 wins!";
		} else if (player1Time + drawOffset < player2Time) {
			result.GetComponent<ConflictResult>().SetResult("player1");
			text.GetComponent<GUIText>().text = "Player 1 wins!";
		} else {
			result.GetComponent<ConflictResult>().SetResult("draw");
			text.GetComponent<GUIText>().text = "Draw";
		}
		StartCoroutine("ConflictEnd");
	}

	private void OnlineApplyRules () {
		if (!isLegal) {
			if (player1Time < player2Time) {
				result.GetComponent<ConflictResult>().SetResult("player2");
				text.GetComponent<GUIText>().text = "Player 2 wins!";				
			} else {
				result.GetComponent<ConflictResult>().SetResult("player1");
				text.GetComponent<GUIText>().text = "Player 1 wins!";
			}
		} else if (player1Time <= legalTime || player1Time > player2Time + drawOffset) {
			result.GetComponent<ConflictResult>().SetResult("player2");
			text.GetComponent<GUIText>().text = "Player 2 wins!";
		} else if (player2Time <= legalTime || player2Time > player1Time + drawOffset) {
			result.GetComponent<ConflictResult>().SetResult("player1");
			text.GetComponent<GUIText>().text = "Player 1 wins!";
		} else {
			result.GetComponent<ConflictResult>().SetResult("draw");
			text.GetComponent<GUIText>().text = "Draw";
		}
		StartCoroutine("ConflictEnd");
	}

	private IEnumerator ConflictEnd () {
		yield return new WaitForSeconds(2f);
		isLegal = false;
		player1Tapped = false;
		player2Tapped = false;
		Camera.main.transform.position = new Vector3 (0, 0, Camera.main.transform.position.z);
		player1.GetComponent<PlayerConflict>().ResetMoves();
		player2.GetComponent<PlayerConflict>().ResetMoves();
		text.GetComponent<GUIText>().text = "";
		this.gameObject.SetActive(false);
	}

	public void StartCountdown () {
		if (global.GetComponent<Global>().GetOnlineGame()) {
			if (Network.isServer) {
				randomRange = Random.Range(2, 8);
				networkView.RPC("RangeComunication", RPCMode.Others, randomRange);
				isLegal = false;
				StartCoroutine("Countdown");
				return;
			}
			return;
		}
		randomRange = Random.Range(2, 8);
		isLegal = false;
		StartCoroutine("Countdown");
	}

	[RPC] void RangeComunication (int value) {
		randomRange = value;
		isLegal = false;
		StartCoroutine("Countdown");
	}

	private IEnumerator Countdown () {
		startTime = Time.time;
		yield return new WaitForSeconds(randomRange);
		legalTime = Time.time - startTime;
		isLegal = true;
		text.GetComponent<GUIText>().text = "Go!";
	}

	public void SetPlayerMoves (string move1, string move2) {
		player1Move = move1;
		player2Move = move2;
	}
}
