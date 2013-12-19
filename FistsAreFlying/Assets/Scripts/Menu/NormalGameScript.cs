using UnityEngine;
using System.Collections;

public class NormalGameScript : MonoBehaviour {
	GameObject globalObject;
	GameObject playChoice;
	GameObject modeChoice;
	Vector3 translateCoordinate;
	// Use this for initialization
	void Start () {
		globalObject=GameObject.Find("GlobalObject");
		playChoice= GameObject.Find("PlayChoice");
		modeChoice= GameObject.Find("ModeChoice");
		translateCoordinate=new Vector3(-20f,0f,0f);
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnMouseDown(){
		playChoice.GetComponent<Transform>().Translate(translateCoordinate);
		modeChoice.GetComponent<Transform>().Translate(translateCoordinate);
	}

}
