using UnityEngine;
using System.Collections;

public class CountDownGUI : MonoBehaviour {

	public Sprite sprite0;
	public Sprite sprite1;
	public Sprite sprite2;
	public Sprite sprite3;
	public Sprite sprite4;
	public Sprite sprite5;
	public Sprite sprite6;
	public Sprite sprite7;
	public Sprite sprite8;
	public Sprite sprite9;
	public Sprite sprite10;
	public Sprite sprite11;
	public Sprite sprite12;
	public Sprite sprite13;
	public Sprite sprite14;
	public Sprite sprite15;
	private Sprite[] sprites = new Sprite[16];

	// Use this for initialization
	void Start () {
		inizializeArray();
	}

	private void inizializeArray() {
		sprites[0] = sprite0;
		sprites[1] = sprite1;
		sprites[2] = sprite2;
		sprites[3] = sprite3;
		sprites[4] = sprite4;
		sprites[5] = sprite5;
		sprites[6] = sprite6;
		sprites[7] = sprite7;
		sprites[8] = sprite8;
		sprites[9] = sprite9;
		sprites[10] = sprite10;
		sprites[11] = sprite11;
		sprites[12] = sprite12;
		sprites[13] = sprite13;
		sprites[14] = sprite14;
		sprites[15] = sprite15;
	}
	
	// Update is called once per frame
	void Update () {}

	public void SetSprite (int index) {
		this.GetComponent<SpriteRenderer>().sprite = sprites[index];
	}
}