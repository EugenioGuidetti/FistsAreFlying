using UnityEngine;
using System.Collections;

public class Shield : MonoBehaviour {


	public float speed = 0.02f; 
	private Vector3 screenPoint;
	private Vector3 offset;
	// Use this for initialization
	void Start () {
	
	}

	void OnMouseDrag()
	{
		screenPoint = Camera.main.WorldToScreenPoint(transform.position);
		
		offset = transform.position - Camera.main.ScreenToWorldPoint(new Vector3(0, Input.mousePosition.y, 0));

			if (offset.y >0 && transform.position.y >-4) 
				transform.Translate (Vector3.down * Time.deltaTime *speed);
			if (offset.y < 0 && transform.position.y<4)

				transform.Translate (Vector3.up * Time.deltaTime * speed);
	}


}

