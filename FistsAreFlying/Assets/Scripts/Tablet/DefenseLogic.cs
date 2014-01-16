using UnityEngine;
using System.Collections;

public class DefenseLogic : MonoBehaviour {

	private GameObject global;
	public GameObject player1;
	public GameObject player2;
	public GameObject result;
	public GameObject text;

	private GameObject attackingPlayer;
	private string hittenTarget = "";

	// Use this for initialization
	void Start () {}

	public void SetGlobal (GameObject global) {
		this.global = global;
	}
	
	// Update is called once per frame
	void Update () {
		if (attackingPlayer.activeSelf) {
			if (!global.GetComponent<Global>().GetOnlineGame()) {
				LocalRoutine();
			}
		} else if (global.GetComponent<Global>().GetAmIPlayer1()) {
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
		player1.GetComponent<PlayerDefense>().ResetMove();
		player2.GetComponent<PlayerDefense>().ResetMove();
		text.GetComponent<GUIText>().text = "";
		this.gameObject.SetActive(false);
	}
	
	public void SetPlayers (string player1Move, string player2Move) {
		player1.SetActive(true);
		player2.SetActive(true);
		if (!global.GetComponent<Global>().GetOnlineGame()) {
			if (player1Move.Equals("D")) {
				player1.GetComponent<PlayerDefense>().SetDefense();
				player2.GetComponent<PlayerDefense>().SetAttack(player2Move);
				attackingPlayer = player2;
			} else {			
				player1.GetComponent<PlayerDefense>().SetAttack(player1Move);
				player2.GetComponent<PlayerDefense>().SetDefense();
				attackingPlayer = player1;
			}
		} else {
			player1.GetComponent<PlayerDefense>().SetOnline();
			player2.GetComponent<PlayerDefense>().SetOnline();
			if (global.GetComponent<Global>().GetAmIPlayer1()) {
				if (player1Move.Equals("D")) {
					player1.GetComponent<PlayerDefense>().SetDefense();
					attackingPlayer = player2;
				} else {
					player1.GetComponent<PlayerDefense>().SetAttack(player1Move);
					attackingPlayer = player1;
				}
			} else {
				if (player2Move.Equals("D")) {
					player2.GetComponent<PlayerDefense>().SetDefense();
					attackingPlayer = player1;
				} else {
					player2.GetComponent<PlayerDefense>().SetAttack(player2Move);
					attackingPlayer = player2;
				}
			}
		}
	}
}
