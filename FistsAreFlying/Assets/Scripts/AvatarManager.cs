using UnityEngine;
using System.Collections;

public class AvatarManager : MonoBehaviour {

	public GameObject avatar;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void setAnimation (string move) {
		if (move == "MF"){
			this.gameObject.GetComponent<Animator>().SetTrigger("moveForward");
			avatar.GetComponent<Animator>().SetTrigger("moveForward");
			return;
		}
		if (move == "MB"){
			this.gameObject.GetComponent<Animator>().SetTrigger("moveBack");
			avatar.GetComponent<Animator>().SetTrigger("moveForward");
			return;
		}
		if (move == "EM"){
			avatar.GetComponent<Animator>().SetTrigger("emptyMove");
			return;
		}
		if (move == "D"){
			avatar.GetComponent<Animator>().SetTrigger("defense");
			return;
		}
		if (move == "PR"){
			avatar.GetComponent<Animator>().SetTrigger("rightPunch");
			return;
		}
		if (move == "PL"){
			avatar.GetComponent<Animator>().SetTrigger("leftPunch");
			return;
		}
		if (move == "KR"){
			avatar.GetComponent<Animator>().SetTrigger("rightKick");
			return;
		}
		if (move == "KL"){
			avatar.GetComponent<Animator>().SetTrigger("leftKick");
			return;
		}
		if (move == "HD"){
			avatar.GetComponent<Animator>().SetTrigger("heavyDamage");
			return;
		}
		if (move == "LD"){
			avatar.GetComponent<Animator>().SetTrigger("lightDamage");
			return;
		}
		if (move == "STOP") {
			avatar.GetComponent<Animator>().speed = 0;
			return;
		}
		if (move == "START") {
			avatar.GetComponent<Animator>().speed = 1;
			return;
		}
		if (move == "DEATH") {
			avatar.GetComponent<Animator>().SetTrigger("death");
		}
		if (move == "GETBACK") {
			avatar.GetComponent<Animator>().SetTrigger("getBack");
		}
	}
}
