using UnityEngine;
using System.Collections;

public class MainMessagesGUI : MonoBehaviour {

	public Sprite p1Turn;
	public Sprite p2Turn;
	public Sprite p1WinRound;
	public Sprite p2WinRound;
	public Sprite roundDraw;
	public Sprite p1WinMatch;
	public Sprite p2WinMatch;
	public Sprite matchDraw;

	// Use this for initialization
	void Start () { }
	
	// Update is called once per frame
	void Update () { }

	public void SetSprite (string message) {
		if (message.Equals("p1Turn")) {
			this.GetComponent<SpriteRenderer>().sprite = p1Turn;
			return;
		}
		if (message.Equals("p2Turn")) {
			this.GetComponent<SpriteRenderer>().sprite = p2Turn;
			return;
		}
		if (message.Equals("p1WinRound")) {
			this.GetComponent<SpriteRenderer>().sprite = p1WinRound;
			return;
		}
		if (message.Equals("p2WinRound")) {
			this.GetComponent<SpriteRenderer>().sprite = p2WinRound;
			return;
		}
		if (message.Equals("roundDraw")) {
			this.GetComponent<SpriteRenderer>().sprite = roundDraw;
			return;
		}
		if (message.Equals("p1WinMatch")) {
			this.GetComponent<SpriteRenderer>().sprite = p1WinMatch;
			return;
		}
		if (message.Equals("p2WinMatch")) {
			this.GetComponent<SpriteRenderer>().sprite = p2WinMatch;
			return;
		}
		if (message.Equals("matchDraw")) {
			this.GetComponent<SpriteRenderer>().sprite = matchDraw;
			return;
		}
		if (message.Equals("")) {
			this.GetComponent<SpriteRenderer>().sprite = null;
			return;
		}
	}
}
