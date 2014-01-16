using UnityEngine;
using System.Collections;

public class DefenseActionOnline : MonoBehaviour {

	public Sprite spritePL;
	public Sprite spriteKR;
	public Sprite spriteKL;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	[RPC] void SetSprite (string move) {
		if (move.Equals("PL")) {
			this.gameObject.GetComponent<SpriteRenderer>().sprite = spritePL;
		}
		if (move.Equals("KR")) {
			this.gameObject.GetComponent<SpriteRenderer>().sprite = spriteKR;
		}
		if (move.Equals("KL")) {
			this.gameObject.GetComponent<SpriteRenderer>().sprite = spriteKL;
		}
	}
}
