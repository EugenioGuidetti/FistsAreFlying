using UnityEngine;
using System.Collections;

public class DefenseShieldOnline : MonoBehaviour {

	private bool ready = false;
	private float speed = 10f;
	private Vector3 touchPosition = Vector3.zero;
	private float wallUpperY;
	private float wallLowerY;
	private float wallHeight;

	// Use this for initialization
	void Start () {}
	
	// Update is called once per frame
	void Update () {
		if (ready) {			
			foreach (Touch touch in Input.touches) {
				touchPosition = Camera.main.ScreenToWorldPoint(new Vector3(touch.position.x, touch.position.y, 10));
				if (touchPosition.x >= this.transform.position.x - 2 && touchPosition.x <= this.transform.position.x + 2) {
					if (touchPosition.y > this.transform.position.y && this.transform.position.y < wallUpperY - wallHeight) {
						this.transform.Translate(Vector3.up * Time.deltaTime * speed);
					}
					if (touchPosition.y < this.transform.position.y && this.transform.position.y > wallLowerY + wallHeight) {
						this.transform.Translate(Vector3.down * Time.deltaTime * speed);
					}
				}
			}
		}	
	}

	public void SetWalls (GameObject wallUpper, GameObject wallLower) {		
		wallUpperY = wallUpper.GetComponent<Transform>().position.y;
		wallLowerY = wallLower.GetComponent<Transform>().position.y;
		wallHeight = wallUpper.GetComponent<BoxCollider2D>().size.y;
		ready = true;
	}
}
