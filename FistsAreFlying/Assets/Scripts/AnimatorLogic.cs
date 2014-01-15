using UnityEngine;
using System.Collections;

public class AnimatorLogic : MonoBehaviour {

	public GameObject avatar1Manager;
	public GameObject avatar2Manager;
	public GameObject defenseGame;
	public GameObject conflictGame;
	
	private bool animationEnd = false;

	private string player1move;
	private string player2move;
	private string typeAnimation;

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

	public void EndMinigame (){
		avatar1Manager.GetComponent<AvatarManager>().setAnimation ("MB");
		avatar2Manager.GetComponent<AvatarManager>().setAnimation ("MB");
	}

	private IEnumerator StartAnimation () {
		avatar1Manager.GetComponent<AvatarManager>().setAnimation ("MF");
		avatar2Manager.GetComponent<AvatarManager>().setAnimation ("MF");
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
			yield return new WaitForSeconds(normalDurate);
			conflictGame.SetActive(true);
			conflictGame.GetComponent<ConflictLogic>().SetPlayerMoves(player1move, player2move);
			Camera.main.transform.position = new Vector3 (0, 15, Camera.main.transform.position.z);
			conflictGame.GetComponent<ConflictLogic>().StartCountdown();
		}
		else if (typeAnimation == "defense") {
			avatar1Manager.GetComponent<AvatarManager>().setAnimation (player1move);
			avatar2Manager.GetComponent<AvatarManager>().setAnimation (player2move);
			yield return new WaitForSeconds(normalDurate);
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
