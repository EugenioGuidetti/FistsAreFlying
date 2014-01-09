using UnityEngine;
using System.Collections;

public class MenuTimeGroup : MonoBehaviour {

	private GameObject global;
	public GameObject time1;
	public GameObject time2;
	public GameObject time3; 

	// Use this for initialization
	void Start () {
		global = GameObject.Find("GlobalObject");
	
	}
	
	// Update is called once per frame
	void Update () {
		if (time1.GetComponent<MenuButton> ().GetAmISelected()) {
			global.GetComponent<Global>().SetTime(15);
		}
		if (time1.GetComponent<MenuButton> ().GetAmISelected()) {
			global.GetComponent<Global>().SetTime(10);
		}
		if (time1.GetComponent<MenuButton> ().GetAmISelected()) {
			global.GetComponent<Global>().SetTime(5);
		}
	}
}
