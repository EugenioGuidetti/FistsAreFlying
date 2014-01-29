using UnityEngine;
using System.Collections;

public class ConflictLogic : MonoBehaviour {

	private GameObject global;
	public GameObject player1;
	public GameObject player2;
	public GameObject result;
	public GameObject pauseGUI;
	public GameObject conflictMessagesGUI;
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
	private Vector3 touchPosition = Vector3.zero;

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
			ApplyRules();
		}
	}

	private void LocalCheck () {
		foreach (Touch touch in Input.touches) {
			touchPosition = Camera.main.ScreenToWorldPoint(new Vector3(touch.position.x, touch.position.y, 10));
			if (touchPosition.x < this.transform.position.x && !player1Tapped) {
				player1Time = Time.time - startTime;
				player1Tapped = true;
				player1.GetComponent<PlayerConflict>().ShowMove(player1Move);
				if (!isLegal) {
					player2Time = randomRange + 1;
					player2Tapped = true;
				}
			}
			if (touchPosition.x > this.transform.position.x && !player2Tapped) {
				player2Time = Time.time - startTime;
				player2Tapped = true;
				player2.GetComponent<PlayerConflict>().ShowMove(player2Move);
				if(!isLegal) {
					player1Time = randomRange + 1;
					player1Tapped = true;
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

	private void ApplyRules () {
		if (player1Time < randomRange && player2Time < randomRange) {
			if (player1Time < player2Time) {
				result.GetComponent<ConflictResult>().SetResult("player2");
				conflictMessagesGUI.GetComponent<ConflictMessagesGUI>().SetSprite("p1Dirty");
			} else {
				result.GetComponent<ConflictResult>().SetResult("player1");
				conflictMessagesGUI.GetComponent<ConflictMessagesGUI>().SetSprite("p2Dirty");
			}
		} else if (player1Time < randomRange) {
			result.GetComponent<ConflictResult>().SetResult("player2");
			conflictMessagesGUI.GetComponent<ConflictMessagesGUI>().SetSprite("p1Dirty");
		} else if (player2Time < randomRange) {
			result.GetComponent<ConflictResult>().SetResult("player1");
			conflictMessagesGUI.GetComponent<ConflictMessagesGUI>().SetSprite("p2Dirty");
		} else if (player1Time > player2Time + drawOffset) {
			result.GetComponent<ConflictResult>().SetResult("player2");
			conflictMessagesGUI.GetComponent<ConflictMessagesGUI>().SetSprite("p2Faster");
		} else if (player2Time > player1Time + drawOffset) {
			result.GetComponent<ConflictResult>().SetResult("player1");
			conflictMessagesGUI.GetComponent<ConflictMessagesGUI>().SetSprite("p1Faster");
		} else {
			result.GetComponent<ConflictResult>().SetResult("draw");
			conflictMessagesGUI.GetComponent<ConflictMessagesGUI>().SetSprite("sameTime");
		}
		StartCoroutine("ConflictEnd");
	}

	private IEnumerator ConflictEnd () {
		yield return new WaitForSeconds(2f);
		isLegal = false;
		player1Tapped = false;
		player2Tapped = false;
		Camera.main.transform.position = new Vector3 (0, 0, Camera.main.transform.position.z);
		pauseGUI.GetComponent<Transform>().position = new Vector3 ( 0, 0, 0);
		player1.GetComponent<PlayerConflict>().ResetMoves();
		player2.GetComponent<PlayerConflict>().ResetMoves();
		conflictMessagesGUI.GetComponent<ConflictMessagesGUI>().SetSprite("");
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
		isLegal = true;
		conflictMessagesGUI.GetComponent<ConflictMessagesGUI>().SetSprite("tap");
	}

	public void SetPlayerMoves (string move1, string move2) {
		player1Move = move1;
		player2Move = move2;
	}
}
