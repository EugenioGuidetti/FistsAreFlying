using UnityEngine;
using System.Collections;

public class MenuOnlineGroup : MonoBehaviour {
	
	public GameObject createMatch;
	public GameObject searchMatch;
	
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
			if (createMatch.GetComponent<MenuButton>().GetAmISelected()) {
				selectedButton = "createMatch";
				haveISelected = true;
				return;
			}
			if (searchMatch.GetComponent<MenuButton>().GetAmISelected()) {
				selectedButton = "searchMatch";
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
