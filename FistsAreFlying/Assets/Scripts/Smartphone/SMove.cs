using UnityEngine;
using System.Collections;

public class SMove : MonoBehaviour {
	
	private Camera mainCamera;
	public Sprite usedMove;
	private Sprite availableMove;
	
	private bool amISelected = false;
	private Vector2 position;
	private Vector2 offset ;
	private Vector3 touchCoordinates;
	
	// Use this for initialization
	void Start () {
		position = new Vector2(transform.position.x, transform.position.y);
		offset = new Vector2(0.5f, 0.5f);
		mainCamera = GameObject.Find("Main Camera").GetComponent<Camera>();
		availableMove = this.gameObject.GetComponent<SpriteRenderer>().sprite;
	}
	
	// Update is called once per frame
	void Update () {
		if (GetComponent<BoxCollider2D>().enabled) {
			foreach (Touch touch in Input.touches) {
				touchCoordinates = mainCamera.ScreenToWorldPoint(new Vector3(touch.position.x, touch.position.y, 10));
				if (touch.phase == TouchPhase.Began) {
					if (touchCoordinates.x <= position.x + offset.x && touchCoordinates.x >= position.x - offset.x) {
						if (touchCoordinates.y <= position.y + offset.y && touchCoordinates.y >= position.y - offset.y) {
							amISelected = true;
						}
					}
				}
			}
		}
	}
	
	public bool getAmISelected () {
		if (amISelected) {
			amISelected = false;
			return true;
		}
		return amISelected;
	}

	public void SetPosition () {
		position = new Vector2(transform.position.x, transform.position.y);
	}

	public void UseMove () {
		this.gameObject.GetComponent<SpriteRenderer>().sprite = usedMove;
	}

	public void ResetMove () {
		this.gameObject.GetComponent<SpriteRenderer>().sprite = availableMove;
	}
}