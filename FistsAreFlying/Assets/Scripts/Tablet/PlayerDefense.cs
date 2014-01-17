using UnityEngine;
using System.Collections;

public class PlayerDefense : MonoBehaviour {

	public GameObject defense;
	public GameObject punchRight;
	public GameObject punchLeft;
	public GameObject kickRight;
	public GameObject kickLeft;
	public GameObject wallUpper;
	public GameObject wallLower;

	public GameObject punchRightPrefab;
	public GameObject punchLeftPrefab;
	public GameObject kickRightPrefab;
	public GameObject kickLeftPrefab;
	public GameObject shieldPrefab;

	private bool amIAttacking = false;
	private GameObject attackingMove;
	private GameObject defensingMove;
	private bool haveIHitSomething = false;
	private string hittenTarget;

	// Use this for initialization
	void Start () {}
	
	// Update is called once per frame
	void Update () {
		if (amIAttacking) {
			if (attackingMove.GetComponent<DefenseAction>().GetHitStatus()) {
				hittenTarget = attackingMove.GetComponent<DefenseAction>().GetHittenTarget();
				haveIHitSomething = true;
			}
		}
	}

	public bool GetHitStatus () {
		if (haveIHitSomething) {
			haveIHitSomething = false;
			return true;
		}
		return haveIHitSomething;
	}

	public string GetHittenTarget () {
		haveIHitSomething = false;
		return hittenTarget;
	}

	public void SetDefense () {
		defense.SetActive(true);
		defense.GetComponent<DefenseShield>().SetWalls(wallUpper, wallLower);
	}

	public void SetAttack (string move) {
		if (move.Equals("PR")) {
			attackingMove = punchRight;
		}
		if (move.Equals("PL")) {
			attackingMove = punchLeft;
		}
		if (move.Equals("KR")) {
			attackingMove = kickRight;
		}
		if (move.Equals("KL")) {
			attackingMove = kickLeft;
		}
		amIAttacking = true;
		attackingMove.SetActive(true);
	}

	public void SetDefenseOnline() {
		Network.Instantiate(shieldPrefab, this.transform.position, Quaternion.identity, 0);
		defensingMove = GameObject.Find("TDefenseShield");
		defensingMove.GetComponent<DefenseShield>().SetWalls(wallUpper, wallLower);
	}

	public void SetAttackOnline (string move) {
		if (move.Equals("PR")) {
			Network.Instantiate(punchRightPrefab, this.transform.position, Quaternion.identity, 0);
			attackingMove = GameObject.Find("TDefensePR");
		}
		if (move.Equals("PL")) {
			Network.Instantiate(punchLeftPrefab, this.transform.position, Quaternion.identity, 0);
			attackingMove = GameObject.Find("TDefensePL");
		}
		if (move.Equals("KR")) {
			Network.Instantiate(kickRightPrefab, this.transform.position, Quaternion.identity, 0);
			attackingMove = GameObject.Find("TDefenseKR");
		}
		if (move.Equals("KL")) {
			Network.Instantiate(kickLeftPrefab, this.transform.position, Quaternion.identity, 0);
			attackingMove = GameObject.Find("TDefenseKL");
		}
		amIAttacking = true;
	}

	public void ResetMove () {
		if (amIAttacking) {
			attackingMove.GetComponent<Transform>().position = this.transform.position;
			attackingMove.SetActive(false);
			amIAttacking = false;
		} else {
			defense.GetComponent<Transform>().position = this.transform.position;
			defense.SetActive(false);
		}
		this.gameObject.SetActive(false);
	}

	public void ResetMoveOnline () {
		if (amIAttacking) {
			Network.Destroy(attackingMove.GetComponent<NetworkView>().viewID);
			amIAttacking = false;
		} else {
			Network.Destroy(defensingMove.GetComponent<NetworkView>().viewID);
		}
		this.gameObject.SetActive(false);
	}
}
