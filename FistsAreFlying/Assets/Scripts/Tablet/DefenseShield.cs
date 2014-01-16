using UnityEngine;
using System.Collections;

public class DefenseShield : MonoBehaviour {

	private bool passive = false;

	public GameObject wallUpper;
	public GameObject wallLower;

	private float speed = 10f;
	private Vector3 touchPosition = Vector3.zero;
	private float wallUpperY;
	private float wallLowerY;
	private float wallHeight;

	// Use this for initialization
	void Start () {
		wallUpperY = wallUpper.GetComponent<Transform>().position.y;
		wallLowerY = wallLower.GetComponent<Transform>().position.y;
		wallHeight = wallUpper.GetComponent<BoxCollider2D>().size.y;
	}
	
	// Update is called once per frame
	void Update () {
		if (!passive) {
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

	public void SetPassive (bool flag) {
		passive = flag;
	}
}
