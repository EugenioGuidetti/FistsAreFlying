using UnityEngine;
using System.Collections;

public class Action2 : MonoBehaviour {
	
	private Vector3 screenPoint;
	bool isMoving; 
	float speed;
	Vector3 currentMousePosition;
	Vector3 oldMousePosition = Vector3.zero;
	Vector3 mouseMovement; 
	Vector3 oggettoPos; 
	Vector3 offsetInit;
	Vector3 speed_vector = Vector3.zero;
	//	Vector3 mouseMovement2; 
	private float noise = 0.1f;
	float initialTime;
	float finalTime;
	float totalTime;
	//	float movimentDistance;
	
	// Use this for initialization
	
	void Start () {
		isMoving = false; 
		oggettoPos = transform.position;
	}
	// posizione iniziale del mouse
	void OnMouseDown (){
		if (speed_vector == Vector3.zero) {
			oldMousePosition = new Vector3 (Input.mousePosition.x, Input.mousePosition.y, 0);
			offsetInit = new Vector3 (oggettoPos.x - oldMousePosition.x, oggettoPos.y - oldMousePosition.y, 0);
			initialTime = Time.time;
		} 
		else
			isMoving = true; 
	}
	// posizione finale del mouse
	void OnMouseUp (){
		if (!isMoving) {
			currentMousePosition = new Vector3 (Input.mousePosition.x, Input.mousePosition.y, 0);
			mouseMovement = currentMousePosition + offsetInit;
			//			mouseMovement2 = currentMousePosition - oldMousePosition;
			//			movimentDistance = Mathf.Sqrt(Mathf.Pow(mouseMovement2.x, 2f) + Mathf.Pow(mouseMovement2.y, 2f));
			finalTime = Time.time; 
			totalTime = finalTime - initialTime; 
			speed_vector = new Vector3 (mouseMovement.x*1000/totalTime, mouseMovement.y*1000/totalTime, 0f);
			speed_vector.Normalize ();

			
			speed =  5/totalTime;
			
		}
	}
	
	void Update (){
		
		transform.Translate (speed_vector * Time.deltaTime* speed);
	}
	
	void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.name == "Wall_up" || other.gameObject.name == "Wall_down") {
			float noise_x = Random.value * 2 * noise - noise / 2f;
			float noise_y = Random.value * 2 * noise - noise / 2f;
			speed_vector = new Vector3 (speed_vector.x + noise_x, -speed_vector.y + noise_y, speed_vector.z);
			speed_vector.Normalize ();
		}
		if (other.gameObject.name == "Shield") {
			Debug.Log ("Parata!!");
			speed_vector = new Vector3 (0,0,0);
		}
		else if (other.gameObject.name == "collision")
			Debug.Log ("turno perso!");
		//da aggiustare: quando tocca lo shield poi collide anche con l'altro.
	}
	
	
	
}
