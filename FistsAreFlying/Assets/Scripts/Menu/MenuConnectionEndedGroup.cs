using UnityEngine;
using System.Collections;

public class MenuConnectionEndedGroup : MonoBehaviour {

	public GameObject quit;
	private bool quitSelected = false;

	// Use this for initialization
	void Start () {}
	
	// Update is called once per frame
	void Update () {
		if (!quitSelected) {
			if (quit.GetComponent<MenuButton>().GetAmISelected()) {
				quitSelected = true;
			}
		}
	}

	public bool GetQuitSelected () {
		if (quitSelected) {
			quitSelected = false;
			return true;
		}
		return quitSelected;
	}
}
