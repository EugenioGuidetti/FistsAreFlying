using UnityEngine;
using System.Collections;

public class DefenseLogic : MonoBehaviour {

	public GameObject player1;
	public GameObject player2;
	public GameObject result;
	public GameObject text;

	private GameObject attackingPLayer;
	private string hittenTarget = "";

	// Use this for initialization
	void Start () {}
	
	// Update is called once per frame
	void Update () {
		if (attackingPLayer.activeSelf) {
			if(attackingPLayer.GetComponent<PlayerDefense>().GetHitStatus()) {
				hittenTarget = attackingPLayer.GetComponent<PlayerDefense>().GetHittenTarget();
				if (hittenTarget.Equals(attackingPLayer.gameObject.name.ToString()) || hittenTarget.Equals("Defense")) {
					result.GetComponent<DefenseResult>().SetResult(attackingPLayer.gameObject.name.ToString());
					text.GetComponent<GUIText>().text = attackingPLayer.gameObject.name.ToString() + " is frozen in the next turn!";
				} else {
					result.GetComponent<DefenseResult>().SetResult("Fail");					
					text.GetComponent<GUIText>().text = attackingPLayer.gameObject.name.ToString() + " could act in the next turn!";
				}

			}
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
		if (player1Move.Equals("D")) {
			player1.GetComponent<PlayerDefense>().SetDefense();
			player2.GetComponent<PlayerDefense>().SetAttack(player2Move);
			attackingPLayer = player2;
		} else {			
			player1.GetComponent<PlayerDefense>().SetAttack(player1Move);
			player2.GetComponent<PlayerDefense>().SetDefense();
			attackingPLayer = player1;
		}
	}
}
