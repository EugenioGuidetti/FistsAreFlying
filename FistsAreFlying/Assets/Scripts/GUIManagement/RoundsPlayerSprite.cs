using UnityEngine;
using System.Collections;

public class RoundsPlayerSprite : MonoBehaviour {

	public GameObject round1;
	public GameObject round2;
	public GameObject round3;

	// Use this for initialization
	void Start () { }
	
	// Update is called once per frame
	void Update () { }

	public void EnableRounds (int index) {
		if (index == 1) {
			round1.GetComponent<SpriteRenderer>().enabled = true;
		}
		if (index == 2) {
			round2.GetComponent<SpriteRenderer>().enabled = true;
		}
		if (index == 3) {
			round3.GetComponent<SpriteRenderer>().enabled = true;
		}
	}
}
