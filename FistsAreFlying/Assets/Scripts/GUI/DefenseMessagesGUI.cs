using UnityEngine;
using System.Collections;

public class DefenseMessagesGUI : MonoBehaviour {
	
	public Sprite p1Frozen;
	public Sprite p2Frozen;

	// Use this for initialization
	void Start () { }
	
	// Update is called once per frame
	void Update () { }

	public void SetP1Frozen () {
		this.gameObject.GetComponent<SpriteRenderer>().sprite = p1Frozen;
	}

	public void SetP2Frozen () {
		this.gameObject.GetComponent<SpriteRenderer>().sprite = p2Frozen;
	}

	public void SetNoSprite () {
		this.gameObject.GetComponent<SpriteRenderer>().sprite = null;
	}
}
