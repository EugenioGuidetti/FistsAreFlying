using UnityEngine;
using System.Collections;

public class PlayerConflict : MonoBehaviour {

	public GameObject punchRight;
	public GameObject punchLeft;
	public GameObject kickRight;
	public GameObject kickLeft;

	// Use this for initialization
	void Start () {}
	
	// Update is called once per frame
	void Update () {}

	public void ShowMove (string conflictMove) {
		if (conflictMove.Equals("PR")) {
			punchRight.GetComponent<SpriteRenderer>().enabled = true;
			return;
		}
		if (conflictMove.Equals("PL")) {
			punchLeft.GetComponent<SpriteRenderer>().enabled = true;
			return;
		}
		if (conflictMove.Equals("KR")) {
			kickRight.GetComponent<SpriteRenderer>().enabled = true;
			return;
		}
		if (conflictMove.Equals("KL")) {
			kickLeft.GetComponent<SpriteRenderer>().enabled = true;
		}
	}

	public void ResetMoves () {
		punchRight.GetComponent<SpriteRenderer>().enabled = false;
		punchLeft.GetComponent<SpriteRenderer>().enabled = false;
		kickRight.GetComponent<SpriteRenderer>().enabled = false;
		kickLeft.GetComponent<SpriteRenderer>().enabled = false;
	}

}
