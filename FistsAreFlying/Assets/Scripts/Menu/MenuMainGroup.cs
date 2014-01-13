using UnityEngine;
using System.Collections;

public class MenuMainGroup : MonoBehaviour {

	public GameObject play;
	public GameObject playOnline;
	public GameObject options;
	public GameObject credits;

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
			if (play.GetComponent<MenuButton>().GetAmISelected()) {
				selectedButton = "play";
				haveISelected = true;
				return;
			}
			if (playOnline.GetComponent<MenuButton>().GetAmISelected()) {
				selectedButton = "playOnline";
				haveISelected = true;
				return;
			}
			if (options.GetComponent<MenuButton>().GetAmISelected()) {
				selectedButton = "options";
				haveISelected = true;
				return;
			}
			if (credits.GetComponent<MenuButton>().GetAmISelected()) {
				selectedButton = "credits";
				haveISelected = true;
			}			
		}
	}

	public bool GetHaveISelected () {
		if (haveISelected) {
			haveISelected = false;
			return true;
		}
		return haveISelected;
	}

	public string GetSelectedButton () {
		return selectedButton;
	}
}
