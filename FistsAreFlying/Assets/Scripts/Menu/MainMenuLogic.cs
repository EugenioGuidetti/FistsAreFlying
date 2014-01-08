using UnityEngine;
using System.Collections;

public class MainMenuLogic : MonoBehaviour {


	GameObject globalObject;
	// Use this for initialization
	void Start () {
		globalObject=GameObject.Find("GlobalObject");
		globalObject.GetComponent<Global>().SetTimeGame(false);
		globalObject.GetComponent<Global>().SetOnlineGame(false);
	}
	
	// Update is called once per frame
	void Update (){
	}
}
