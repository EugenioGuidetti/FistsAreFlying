using UnityEngine;
using System.Collections;

public class RoundGUI : MonoBehaviour {

	public Sprite sprite0;
	public Sprite sprite1;
	public Sprite sprite2;
	public Sprite sprite3;
	private Sprite[] sprites = new Sprite[4];

	// Use this for initialization
	void Start () {
		inizializeSprites();
	}

	private void inizializeSprites() {
		sprites[0] = sprite0;
		sprites[1] = sprite1;
		sprites[2] = sprite2;
		sprites[3] = sprite3;
	}

	// Update is called once per frame
	void Update () {}

	public void SetSprite (int index) {
		this.GetComponent<SpriteRenderer>().sprite = sprites[index];
	}
}
