using UnityEngine;
using System.Collections;

public class TChoosedMove : MonoBehaviour {
	
	private Sprite originalSprite;
	
	// Use this for initialization
	void Start () {
		originalSprite = this.gameObject.GetComponent<SpriteRenderer>().sprite;
	}
	
	// Update is called once per frame
	void Update () {}
	
	public void ResetSprite () {
		this.gameObject.GetComponent<SpriteRenderer>().sprite = originalSprite;
	}
}
