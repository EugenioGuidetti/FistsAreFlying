using UnityEngine;
using System.Collections;

public class AvatarManager : MonoBehaviour {

	private GameObject global;
	private bool effectsActive;
	public GameObject avatar;
	public AudioClip punchLeftClip;
	public AudioClip punchRightClip;
	public AudioClip kickLeftClip;
	public AudioClip kickRightClip;
	public AudioClip defenseClip;
	public AudioClip emptyMoveClip;
	public AudioClip lightDamageClip;
	public AudioClip heavyDamageClip;
	public AudioClip deathClip;


	// Use this for initialization
	void Start () {
		global = GameObject.Find("GlobalObject");
		effectsActive = global.GetComponent<Global>().GetEffects();
		if (!effectsActive) {
			this.gameObject.GetComponent<AudioSource>().enabled = false;
		}

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
			this.gameObject.GetComponent<AudioSource>().clip = emptyMoveClip;
			this.gameObject.GetComponent<AudioSource>().PlayDelayed(0.10f);
			return;
		}
		if (move == "D"){
			avatar.GetComponent<Animator>().SetTrigger("defense");
			this.gameObject.GetComponent<AudioSource>().clip = defenseClip;
			this.gameObject.GetComponent<AudioSource>().PlayDelayed(0.1f);
			return;
		}
		if (move == "PR"){
			avatar.GetComponent<Animator>().SetTrigger("rightPunch");
			this.gameObject.GetComponent<AudioSource>().clip = punchRightClip;
			this.gameObject.GetComponent<AudioSource>().PlayDelayed(0.5f);
			return;
		}
		if (move == "PL"){
			avatar.GetComponent<Animator>().SetTrigger("leftPunch");
			this.gameObject.GetComponent<AudioSource>().clip = punchLeftClip;
			this.gameObject.GetComponent<AudioSource>().PlayDelayed(0.5f);
			return;
		}
		if (move == "KR"){
			avatar.GetComponent<Animator>().SetTrigger("rightKick");
			this.gameObject.GetComponent<AudioSource>().clip = kickRightClip;
			this.gameObject.GetComponent<AudioSource>().PlayDelayed(0.5f);
			return;
		}
		if (move == "KL"){
			avatar.GetComponent<Animator>().SetTrigger("leftKick");
			this.gameObject.GetComponent<AudioSource>().clip = kickLeftClip;
			this.gameObject.GetComponent<AudioSource>().PlayDelayed(0.5f);
			return;
		}
		if (move == "HD"){
			avatar.GetComponent<Animator>().SetTrigger("heavyDamage");
			this.gameObject.GetComponent<AudioSource>().clip = heavyDamageClip;
			this.gameObject.GetComponent<AudioSource>().PlayDelayed(0.5f);
			return;
		}
		if (move == "LD"){
			avatar.GetComponent<Animator>().SetTrigger("lightDamage");
			this.gameObject.GetComponent<AudioSource>().clip = lightDamageClip;
			this.gameObject.GetComponent<AudioSource>().PlayDelayed(0.5f);
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
			this.gameObject.GetComponent<AudioSource>().clip = deathClip;
			this.gameObject.GetComponent<AudioSource>().PlayDelayed(0.5f);
		}
		if (move == "GETBACK") {
			avatar.GetComponent<Animator>().SetTrigger("getBack");
		}
	}
}
