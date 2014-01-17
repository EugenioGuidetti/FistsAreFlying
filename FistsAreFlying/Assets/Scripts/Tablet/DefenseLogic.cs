using UnityEngine;
using System.Collections;

public class DefenseLogic : MonoBehaviour {

	private bool online = false;
	private bool amIPlayer1 = false;
	public GameObject player1;
	public GameObject player2;
	public GameObject result;
	public GameObject text;

	private GameObject attackingPlayer;
	private string hittenTarget = "";

	// Use this for initialization
	void Start () {}

	public void SetOnline () {
		online = true;
	}

	public void SetPlayer1 () {
		amIPlayer1 = true;
	}
	
	// Update is called once per frame
	void Update () {
		if (attackingPlayer.activeSelf) {
			if (!online) {
				LocalRoutine();
			}
		} else if (amIPlayer1) {
			if (attackingPlayer.Equals(player1)) {
				OnlineAttackRoutine();
			} else {
				OnlineDefenseRoutine();
			}
		} else {
			if (attackingPlayer.Equals(player2)) {
				OnlineAttackRoutine();
			} else {
				OnlineDefenseRoutine();
			}
		}
	}

	private void LocalRoutine () {
		if(attackingPlayer.GetComponent<PlayerDefense>().GetHitStatus()) {
			hittenTarget = attackingPlayer.GetComponent<PlayerDefense>().GetHittenTarget();
			ApplyRules();
			StartCoroutine("DefenseEnd");
		}		
	}

	private void OnlineAttackRoutine () {
		if(attackingPlayer.GetComponent<PlayerDefense>().GetHitStatus()) {
			hittenTarget = attackingPlayer.GetComponent<PlayerDefense>().GetHittenTarget();
			networkView.RPC("HitComunication", RPCMode.Others, hittenTarget);
			ApplyRules();
			hittenTarget = "";
			StartCoroutine("DefenseEnd");
		}
	}

	private void OnlineDefenseRoutine () {
		if(!hittenTarget.Equals("")) {
			ApplyRules();
			hittenTarget = "";
			StartCoroutine("DefenseEnd");
		}
	}

	[RPC] void HitComunication (string target) {
		hittenTarget = target;
	}

	private void ApplyRules () {
		if (hittenTarget.Equals(attackingPlayer.gameObject.name.ToString()) || hittenTarget.Equals("Defense")) {
			result.GetComponent<DefenseResult>().SetResult(attackingPlayer.gameObject.name.ToString());
			text.GetComponent<GUIText>().text = attackingPlayer.gameObject.name.ToString() + " is frozen in the next turn!";
		} else {
			result.GetComponent<DefenseResult>().SetResult("Fail");					
			text.GetComponent<GUIText>().text = attackingPlayer.gameObject.name.ToString() + " could act in the next turn!";
		}
	}

	private IEnumerator DefenseEnd () {
		yield return new WaitForSeconds(2f);
		Camera.main.transform.position = new Vector3 (0, 0, Camera.main.transform.position.z);
		if (!online) {
			player1.GetComponent<PlayerDefense>().ResetMove();
			player2.GetComponent<PlayerDefense>().ResetMove();
		} else {
			player1.GetComponent<PlayerDefense>().ResetMoveOnline();
			player2.GetComponent<PlayerDefense>().ResetMoveOnline();			
		}
		text.GetComponent<GUIText>().text = "";
		this.gameObject.SetActive(false);
	}
	
	public void SetPlayers (string player1Move, string player2Move) {
		if (player1Move.Equals("D")) {
			if (!online) {
				player1.GetComponent<PlayerDefense>().SetDefense();
				player2.GetComponent<PlayerDefense>().SetAttack(player2Move);
			} else {
				if (amIPlayer1) {
					player1.GetComponent<PlayerDefense>().SetDefenseOnline();
				} else {
					player2.GetComponent<PlayerDefense>().SetAttackOnline(player2Move);
				}
			}			
			attackingPlayer = player2;
		} else {
			if (!online) {
				player1.GetComponent<PlayerDefense>().SetAttack(player1Move);
				player2.GetComponent<PlayerDefense>().SetDefense();
			} else {
				if (amIPlayer1) {
					player1.GetComponent<PlayerDefense>().SetAttackOnline(player1Move);
				} else {
					player2.GetComponent<PlayerDefense>().SetDefenseOnline();
				}
			}
			attackingPlayer = player1;
		}
		player1.SetActive(true);
		player2.SetActive(true);
	}
}
