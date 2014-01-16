using UnityEngine;
using System.Collections;

public class PlayerDefense : MonoBehaviour {

	public GameObject defense;
	public GameObject punchRight;
	public GameObject punchLeft;
	public GameObject kickRight;
	public GameObject kickLeft;

	private bool amIOnline = false;
	private bool amIAttacking = false;
	private GameObject attackingMove;
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
		if (amIOnline) {
			defense.GetComponent<DefenseShield>().SetPassive(true);
		}
		defense.SetActive(true);
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
		if (amIOnline) {
			attackingMove.GetComponent<DefenseAction>().SetPassive();
		}
		attackingMove.SetActive(true);
	}

	public void ResetMove () {
		if (amIAttacking) {
			attackingMove.GetComponent<Transform>().position = this.transform.position;
			attackingMove.SetActive(false);
			amIAttacking = false;
		} else {
			defense.GetComponent<Transform>().position = this.transform.position;
			if (amIOnline) {
				defense.GetComponent<DefenseShield>().SetPassive(false);
			}
			defense.SetActive(false);
		}
		amIOnline = false;
		this.gameObject.SetActive(false);
	}

	public void SetOnline () {
		amIOnline = true;
	}
}
