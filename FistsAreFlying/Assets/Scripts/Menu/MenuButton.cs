using UnityEngine;
using System.Collections;

public class MenuButton : MonoBehaviour {

	private bool amISelected;

	// Use this for initialization
	void Start () {
		amISelected = false;
	}
	
	// Update is called once per frame
	void Update () {}

	private void OnMouseDown () {
		amISelected = true;
	}

	public bool GetAmISelected () {
		if (amISelected) {
			amISelected = false;
			return true;		
		}
		return amISelected;
	}
}
