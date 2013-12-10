using UnityEngine;
using System.Collections;

public class Shield1 : MonoBehaviour {

	
	
	public float speed = 0.02f; 
	private Vector3 offset;
	private float shieldPosition;
	private float wallUpPosition;
	private float wallDownPosition;
	private float wallOffset;
	private GameObject shield;
	private Camera currentCamera;
	
	// Use this for initialization
	void Start () {
		currentCamera=GameObject.Find("DefenseGameCamPlayer1").GetComponent<Camera>();
		wallUpPosition= GameObject.Find("Wall_up1").GetComponent<Transform>().position.y;
		wallDownPosition= GameObject.Find("Wall_down1").GetComponent<Transform>().position.y;
		wallOffset= GameObject.Find("Wall_down1").GetComponent<Transform>().localScale.y*2;
	}
	
	void OnMouseDrag()
	{
		offset = currentCamera.ScreenToWorldPoint(new Vector3(0, Input.mousePosition.y, 0));
		if (offset.y >transform.position.y && transform.position.y >wallDownPosition+wallOffset) 
			transform.Translate (Vector3.up * Time.deltaTime *speed);
		if (offset.y < transform.position.y && transform.position.y<wallUpPosition-wallOffset)
			transform.Translate (Vector3.down * Time.deltaTime * speed);
	}

}
