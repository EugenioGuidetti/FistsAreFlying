using UnityEngine;
using System.Collections;

public class DefenseAction : MonoBehaviour {

	private int relevantTouch = -9999;
	private float noise = 0.1f;
	private float noiseY;
	private float startTime = 0f;
	private float endTime = 0f;
	private float totalTime = 0f;
	private float speed = 0f;
	private const float minSpeed = 5f;
	private const float maxSpeed = 25f;
	private Vector3 direction = Vector3.zero;
	private Vector3 startPosition = Vector3.zero;
	private Vector3 endPosition = Vector3.zero;
	private Vector3 touchMovement = Vector3.zero;
	private Vector3 touchPosition = Vector3.zero;
	private bool isMoving = false;
	private bool haveIHitSomething = false;
	private string hittenTarget = "";

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
					if (speed > minSpeed) {
						isMoving = true;
					}
				}
			}
		} else {
			transform.Translate(direction * speed * Time.deltaTime);
		}
	}

	void OnTriggerEnter2D (Collider2D other) {
		if (other.gameObject.name.Equals("WallUpper") || other.gameObject.name.Equals("WallLower")) {
			//ho tolto un diviso 2f perché l'intervallo mi sembrava asimmetrico
			noiseY = Random.value * 2 * noise - noise;
			direction = new Vector3(direction.x, noiseY - direction.y, direction.z);
			direction.Normalize();
		}
		if (other.gameObject.name.Equals("Defense") || other.gameObject.name.Equals("DefenseShield(Clone)") || 
		    other.gameObject.name.Equals("Player1") || other.gameObject.name.Equals("Player2")) {
			direction = Vector3.zero;
			speed = 0;
			hittenTarget = other.gameObject.name.ToString();
			haveIHitSomething = true;
		}
	}

	public bool GetHitStatus () {
		return haveIHitSomething;
	}

	public string GetHittenTarget () {
		haveIHitSomething = false;
		relevantTouch = -9999;
		isMoving = false;
		return hittenTarget;
	}
}
