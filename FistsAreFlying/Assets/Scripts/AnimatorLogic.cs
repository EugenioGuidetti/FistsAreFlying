using UnityEngine;
using System.Collections;

public class AnimatorLogic : MonoBehaviour {

	public GameObject avatar1Manager;
	public GameObject avatar2Manager;
	public GameObject defenseGame;
	public GameObject conflictGame;
	
	private bool animationEnd = false;
	private bool player1Frozen = false;
	private bool player2Frozen = false;
	private bool player1Death = false;
	private bool player2Death = false;

	private string player1move;
	private string player2move;
	private string typeAnimation;
	private string result;

	private float normalDurate = 1f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public bool isAnmiationEnd () {
		if (animationEnd) {
			animationEnd = false;
			return true;
		}
		return animationEnd;
	}

	public void SetMoves (string player1move, string player2move, string typeAnimation) {
		this.player1move = player1move;
		this.player2move = player2move;
		this.typeAnimation = typeAnimation;
		StartCoroutine("StartAnimation");
	}

	public void EndMinigame (string result){
		this.result = result;
		StartCoroutine("AnimationAfetrMinigame");
	}

	public void EndRound (string result) {
		this.result = result;
		StartCoroutine("AnimationEndTurn");
	}

	private IEnumerator AnimationEndTurn () {
		if (result == "player1") {
			avatar1Manager.GetComponent<AvatarManager>().setAnimation ("DEATH");
			player1Death = true;
		} else if (result == "player2") {
			avatar2Manager.GetComponent<AvatarManager>().setAnimation ("DEATH");
			player2Death = true;
		} else if (result == "draw") {
			avatar1Manager.GetComponent<AvatarManager>().setAnimation ("DEATH");
			player1Death = true;
			avatar2Manager.GetComponent<AvatarManager>().setAnimation ("DEATH");
			player2Death = true;
		}
		yield return new WaitForSeconds (1.2f);
		avatar1Manager.GetComponent<AvatarManager>().setAnimation ("STOP");
		avatar2Manager.GetComponent<AvatarManager>().setAnimation ("STOP");
	}

	public void NewRound () {
		StartCoroutine("NewRoundAnimation");
	}

	private IEnumerator NewRoundAnimation () {
		yield return new WaitForSeconds(2f);
		avatar1Manager.GetComponent<AvatarManager>().setAnimation ("START");
		avatar2Manager.GetComponent<AvatarManager>().setAnimation ("START");
		if (player1Death) {
			avatar1Manager.GetComponent<AvatarManager>().setAnimation ("GETBACK");
		}
		if (player2Death) {
			avatar2Manager.GetComponent<AvatarManager>().setAnimation ("GETBACK");
		}
	}

	private IEnumerator AnimationAfetrMinigame () {
		avatar1Manager.GetComponent<AvatarManager>().setAnimation ("START");
		avatar2Manager.GetComponent<AvatarManager>().setAnimation ("START");
		yield return new WaitForSeconds (normalDurate/2);
		if (result == "Player1") {
			avatar1Manager.GetComponent<AvatarManager>().setAnimation ("STOP");
			player1Frozen = true;
			avatar2Manager.GetComponent<AvatarManager>().setAnimation ("MB");
		} else if (result == "Player2") {
			avatar2Manager.GetComponent<AvatarManager>().setAnimation ("STOP");
			player2Frozen = true;
			avatar1Manager.GetComponent<AvatarManager>().setAnimation ("MB");
		} else if (result == "draw") {
			avatar1Manager.GetComponent<AvatarManager>().setAnimation ("LD");
			avatar2Manager.GetComponent<AvatarManager>().setAnimation ("LD");
			yield return new WaitForSeconds (normalDurate);
			avatar1Manager.GetComponent<AvatarManager>().setAnimation ("MB");
			avatar2Manager.GetComponent<AvatarManager>().setAnimation ("MB");
		} else if (result == "player1") {
			avatar2Manager.GetComponent<AvatarManager>().setAnimation ("HD");
			yield return new WaitForSeconds (normalDurate);
			avatar1Manager.GetComponent<AvatarManager>().setAnimation ("MB");
			avatar2Manager.GetComponent<AvatarManager>().setAnimation ("MB");
		} else if (result == "player2") {
			avatar1Manager.GetComponent<AvatarManager>().setAnimation ("HD");
			yield return new WaitForSeconds (normalDurate);
			avatar1Manager.GetComponent<AvatarManager>().setAnimation ("MB");
			avatar2Manager.GetComponent<AvatarManager>().setAnimation ("MB");
		} else if (result == "Fail") {
			avatar1Manager.GetComponent<AvatarManager>().setAnimation ("MB");
			avatar2Manager.GetComponent<AvatarManager>().setAnimation ("MB");
		}
	}

	private IEnumerator StartAnimation () {
		if (!player1Frozen) {
			avatar1Manager.GetComponent<AvatarManager>().setAnimation ("MF");
		}
		if (!player2Frozen) {
			avatar2Manager.GetComponent<AvatarManager>().setAnimation ("MF");
		}
		if (player1Frozen) {
			player1Frozen = false;
			avatar1Manager.GetComponent<AvatarManager>().setAnimation ("START");
		}
		
		if (player2Frozen) {
			player2Frozen = false;
			avatar2Manager.GetComponent<AvatarManager>().setAnimation ("START");
		}
		yield return new WaitForSeconds(normalDurate);
		if (typeAnimation == "sameMoves") {
			avatar1Manager.GetComponent<AvatarManager>().setAnimation (player1move);
			avatar2Manager.GetComponent<AvatarManager>().setAnimation ("HD");
			yield return new WaitForSeconds(normalDurate);
			avatar2Manager.GetComponent<AvatarManager>().setAnimation (player2move);
			avatar1Manager.GetComponent<AvatarManager>().setAnimation ("HD");
			yield return new WaitForSeconds(normalDurate);
			avatar1Manager.GetComponent<AvatarManager>().setAnimation ("MB");
			avatar2Manager.GetComponent<AvatarManager>().setAnimation ("MB");
		}
		else if (typeAnimation == "player1") {
			avatar2Manager.GetComponent<AvatarManager>().setAnimation (player2move);
			avatar1Manager.GetComponent<AvatarManager>().setAnimation ("LD");
			yield return new WaitForSeconds(normalDurate);
			avatar1Manager.GetComponent<AvatarManager>().setAnimation (player1move);
			avatar2Manager.GetComponent<AvatarManager>().setAnimation ("HD");
			yield return new WaitForSeconds(normalDurate);
			avatar1Manager.GetComponent<AvatarManager>().setAnimation ("MB");
			avatar2Manager.GetComponent<AvatarManager>().setAnimation ("MB");
		}
		else if (typeAnimation == "player2") {
			avatar1Manager.GetComponent<AvatarManager>().setAnimation (player1move);
			avatar2Manager.GetComponent<AvatarManager>().setAnimation ("LD");
			yield return new WaitForSeconds(normalDurate);
			avatar2Manager.GetComponent<AvatarManager>().setAnimation (player2move);
			avatar1Manager.GetComponent<AvatarManager>().setAnimation ("HD");
			yield return new WaitForSeconds(normalDurate);
			avatar1Manager.GetComponent<AvatarManager>().setAnimation ("MB");
			avatar2Manager.GetComponent<AvatarManager>().setAnimation ("MB");
		}
		else if (typeAnimation == "conflict") {
			avatar1Manager.GetComponent<AvatarManager>().setAnimation (player1move);
			avatar2Manager.GetComponent<AvatarManager>().setAnimation (player2move);
			conflictGame.SetActive(true);
			yield return new WaitForSeconds(normalDurate);
			conflictGame.GetComponent<ConflictLogic>().SetPlayerMoves(player1move, player2move);
			Camera.main.transform.position = new Vector3 (0, 15, Camera.main.transform.position.z);
			conflictGame.GetComponent<ConflictLogic>().StartCountdown();
		}
		else if (typeAnimation == "defense") {
			avatar1Manager.GetComponent<AvatarManager>().setAnimation (player1move);
			avatar2Manager.GetComponent<AvatarManager>().setAnimation (player2move);
			yield return new WaitForSeconds(normalDurate/2);
			avatar1Manager.GetComponent<AvatarManager>().setAnimation ("STOP");
			avatar2Manager.GetComponent<AvatarManager>().setAnimation ("STOP");
			defenseGame.SetActive(true);
			defenseGame.GetComponent<DefenseLogic>().SetPlayers(player1move, player2move);
			Camera.main.transform.position = new Vector3 (0, -15, Camera.main.transform.position.z);
		}
		else if (typeAnimation == "emptyVsDamage") {
			if (player1move == "EM") {
				avatar1Manager.GetComponent<AvatarManager>().setAnimation (player1move);
				yield return new WaitForSeconds(normalDurate);
				avatar2Manager.GetComponent<AvatarManager>().setAnimation (player2move);
				avatar1Manager.GetComponent<AvatarManager>().setAnimation ("HD");
				yield return new WaitForSeconds(normalDurate);
				avatar1Manager.GetComponent<AvatarManager>().setAnimation ("MB");
				avatar2Manager.GetComponent<AvatarManager>().setAnimation ("MB");
			}
			else {
				avatar2Manager.GetComponent<AvatarManager>().setAnimation (player2move);
				yield return new WaitForSeconds(normalDurate);
				avatar1Manager.GetComponent<AvatarManager>().setAnimation (player1move);
				avatar2Manager.GetComponent<AvatarManager>().setAnimation ("HD");
				yield return new WaitForSeconds(normalDurate);
				avatar1Manager.GetComponent<AvatarManager>().setAnimation ("MB");
				avatar2Manager.GetComponent<AvatarManager>().setAnimation ("MB");
			}			
		}
		else if (typeAnimation == "") {
			avatar1Manager.GetComponent<AvatarManager>().setAnimation (player1move);
			avatar2Manager.GetComponent<AvatarManager>().setAnimation (player2move);
			yield return new WaitForSeconds(normalDurate);
			avatar1Manager.GetComponent<AvatarManager>().setAnimation ("MB");
			avatar2Manager.GetComponent<AvatarManager>().setAnimation ("MB");
		}
		animationEnd = true;
	}
}
