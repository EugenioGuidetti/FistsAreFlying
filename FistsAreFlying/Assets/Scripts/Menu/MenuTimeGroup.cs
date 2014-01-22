using UnityEngine;
using System.Collections;

public class MenuTimeGroup : MonoBehaviour {

	private GameObject global;
	public GameObject time1;
	public GameObject time2;
	public GameObject time3; 
	public GameObject check1;
	public GameObject check2;
	public GameObject check3;

	// Use this for initialization
	void Start () {
		global = GameObject.Find("GlobalObject");
		if (global.GetComponent<Global>().GetTime() == 15) {
			check1.SetActive(true);	
		}
		if (global.GetComponent<Global>().GetTime() == 10) {
			check2.SetActive(true);	
		}
		if (global.GetComponent<Global>().GetTime() == 5) {
			check3.SetActive(true);	
		}
	
	}
	
	// Update is called once per frame
	void Update () {
		if (time1.GetComponent<MenuButton> ().GetAmISelected()) {
			global.GetComponent<Global>().SetTime(15);
			Debug.Log(global.GetComponent<Global>().GetTime().ToString());
			check1.SetActive(true);
			check2.SetActive(false);
			check3.SetActive(false);

		}
		if (time2.GetComponent<MenuButton> ().GetAmISelected()) {
			global.GetComponent<Global>().SetTime(10);
			Debug.Log(global.GetComponent<Global>().GetTime().ToString());
			check1.SetActive(false);
			check2.SetActive(true);
			check3.SetActive(false);
		}
		if (time3.GetComponent<MenuButton> ().GetAmISelected()) {
			global.GetComponent<Global>().SetTime(5);
			Debug.Log(global.GetComponent<Global>().GetTime().ToString());
			check1.SetActive(false);
			check2.SetActive(false);
			check3.SetActive(true);
		}
	}
}
