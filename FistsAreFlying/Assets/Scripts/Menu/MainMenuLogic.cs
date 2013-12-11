using UnityEngine;
using System.Collections;

public class MainMenuLogic : MonoBehaviour {


	GameObject globalObject;
	// Use this for initialization
	void Start () {
		globalObject=GameObject.Find("GlobalObject");
		globalObject.GetComponent<GlobalObject>().round=1;
		globalObject.GetComponent<GlobalObject>().roundWinPlayer1=0;
		globalObject.GetComponent<GlobalObject>().roundWinPlayer2=0;
		globalObject.GetComponent("GlobalObject").SendMessage("setRoundTimeGame", false);
		globalObject.GetComponent("GlobalObject").SendMessage("setTurnTimeGame", false);
	}
	
	// Update is called once per frame
	void Update (){
	}
}
