using UnityEngine;
using System.Collections;

public class MenuModeGroup : MonoBehaviour {

	public GameObject standardPlay;
	public GameObject timePlay;
	
	private bool haveISelected;
	private string selectedButton;

	// Use this for initialization
	void Start () {
		haveISelected = false;
		selectedButton = "";	
	}
	
	// Update is called once per frame
	void Update () {
		if (!haveISelected) {
			if (standardPlay.GetComponent<MenuButton>().GetAmISelected()) {
				selectedButton = "standardPlay";
				haveISelected = true;
				return;
			}
			if (timePlay.GetComponent<MenuButton>().GetAmISelected()) {
				selectedButton = "timePlay";
				haveISelected = true;
			}			
		}
	}
	
	public bool GetHaveISelected () {
		haveISelected = false;
		return true;
	}
	
	public string GetSelectedButton () {
		return selectedButton;
	}
}
