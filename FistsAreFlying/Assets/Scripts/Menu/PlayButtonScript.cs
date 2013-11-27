using UnityEngine;
using System.Collections;

public class PlayButtonScript : MonoBehaviour {

	GameObject firstChoice;
	GameObject playChoice;
	GameObject modeChoice;
	Vector3 translateCoordinate;
	// Use this for initialization
	void Start () {
		playChoice= GameObject.Find("PlayChoice");
		firstChoice= GameObject.Find("FirstChoice");
		modeChoice= GameObject.Find("ModeChoice");
		translateCoordinate=new Vector3(-20f,0f,0f);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnMouseDown(){
		firstChoice.GetComponent<Transform>().Translate(translateCoordinate);
		playChoice.GetComponent<Transform>().Translate(translateCoordinate);
		modeChoice.GetComponent<Transform>().Translate(translateCoordinate);
	}
}
