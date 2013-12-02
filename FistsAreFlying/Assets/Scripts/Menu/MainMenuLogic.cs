using UnityEngine;
using System.Collections;

public class MainMenuLogic : MonoBehaviour {


	GameObject globalObject;
	// Use this for initialization
	void Start () {
		globalObject=GameObject.Find("GlobalObject");
		globalObject.GetComponent<GlobalObject>().round=1;
	}
	
	// Update is called once per frame
	void Update () {
		globalObject.GetComponent<GlobalObject>().round=1;
		globalObject.GetComponent<GlobalObject>().roundWinPlayer1=0;
		globalObject.GetComponent<GlobalObject>().roundWinPlayer2=0;
	}
}
