using UnityEngine;
using System.Collections;

public class DefenseAction : MonoBehaviour {

	private float speed = 0f;
	private float maxSpeed = 25f;
	private Vector3 direction = Vector3.zero;
	private Vector3 startPosition = Vector3.zero;
	private Vector3 endPosition = Vector3.zero;
	private Vector3 touchMovement = Vector3.zero;
	private Vector3 touchPosition = Vector3.zero;
	private int relevantTouch = -9999;
	private float startTime = 0f;
	private float endTime = 0f;
	private float totalTime = 0f;
	private bool isMoving = false;

	// Use this for initialization
	void Start () {}
	
	// Update is called once per frame
	void Update () {
		if (!isMoving) {
			foreach (Touch touch in Input.touches) {
				touchPosition = Camera.main.ScreenToWorldPoint(new Vector3(touch.position.x, touch.position.y, 10));
				if (touch.phase == TouchPhase.Began) {
					if (touchPosition.x >= this.transform.position.x - 1.25 && touchPosition.x <= this.transform.position.x + 1.25) {
						if (touchPosition.y >= this.transform.position.y - 1.25 && touchPosition.y <= this.transform.position.y + 1.25) {
							startTime = Time.time;
							startPosition = touchPosition;
							relevantTouch = touch.fingerId;
						}
					}
				} else if (touch.phase == TouchPhase.Ended && touch.fingerId == relevantTouch) {
					endTime = Time.time;
					endPosition = touchPosition;
					touchMovement = endPosition - startPosition;
					totalTime = endTime - startTime;
					direction = touchMovement;
					direction.Normalize();
					speed = Mathf.Sqrt(touchMovement.sqrMagnitude) / totalTime;
					if (speed > maxSpeed) {
						speed = maxSpeed;
					}
					isMoving = true;
				}
			}
		} else {
			transform.Translate(direction * speed * Time.deltaTime);
		}

	}
}
