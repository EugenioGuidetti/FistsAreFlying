using UnityEngine;
using System.Collections;

public class ConflictMessagesGUI : MonoBehaviour {

	public Sprite p1Faster;
	public Sprite p2Faster;
	public Sprite p1Dirty;
	public Sprite p2Dirty;
	public Sprite sameTime;
	public Sprite tap;

	// Use this for initialization
	void Start () { }
	
	// Update is called once per frame
	void Update () { }

	public void SetSprite (string message) {
		if (message == "p1Faster") {
			this.GetComponent<SpriteRenderer>().sprite = p1Faster;
			return;
		}
		if (message == "p2Faster") {
			this.GetComponent<SpriteRenderer>().sprite = p2Faster;
			return;
		}
		if (message == "p1Dirty") {
			this.GetComponent<SpriteRenderer>().sprite = p1Dirty;
			return;
		}
		if (message == "p2Dirty") {
			this.GetComponent<SpriteRenderer>().sprite = p2Dirty;
			return;
		}
		if (message == "sameTime") {
			this.GetComponent<SpriteRenderer>().sprite = sameTime;
			return;
		}
		if (message == "tap") {
			this.GetComponent<SpriteRenderer>().sprite = tap;
			return;
		}
		if (message == "") {
			this.GetComponent<SpriteRenderer>().sprite = null;
			return;
		}
	}

}
