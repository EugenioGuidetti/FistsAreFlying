using UnityEngine;
using System.Collections;

public class Action2 : MonoBehaviour {

	private Vector3 screenPoint;
	bool isMoving; 
	float speed =5f;
	Vector3 currentMousePosition;
	Vector3 oldMousePosition = Vector3.zero;
	Vector3 mouseMovement; 
	Vector3 oggettoPos; 
	Vector3 offsetInit;
	Vector3 speed_vector = Vector3.zero;
	private float noise = 0.1f;
	
	// Use this for initialization
	
	void Start () {
		isMoving = false; 
		oggettoPos = transform.position;
	}

	void OnMouseDown (){
		if (speed_vector == Vector3.zero) {
						oldMousePosition = new Vector3 (Input.mousePosition.x, Input.mousePosition.y, 0);
						offsetInit = new Vector3 (oggettoPos.x - oldMousePosition.x, oggettoPos.y - oldMousePosition.y, 0);
				} 
		else
						isMoving = true; 
	}

	void OnMouseDrag (){
				Debug.Log (isMoving);
				if (!isMoving) {
						currentMousePosition = new Vector3 (Input.mousePosition.x, Input.mousePosition.y, 0);
						mouseMovement = currentMousePosition + offsetInit;

						speed_vector = new Vector3 (mouseMovement.x, mouseMovement.y, 0f);
						speed_vector.Normalize ();
				}
		}
	
		void Update (){
	
		transform.Translate (speed_vector * speed*Time.deltaTime);
				
		if (transform.position.y < -4.0 || transform.position.y > 4.0) {
			float noise_x = Random.value*2*noise-noise/2f;
			float noise_y = Random.value*2*noise-noise/2f;
			speed_vector = new Vector3 (speed_vector.x + noise_x, -speed_vector.y + noise_y, speed_vector.z);

			transform.Translate (speed_vector *speed * Time.deltaTime); 	
				}

	}
}
