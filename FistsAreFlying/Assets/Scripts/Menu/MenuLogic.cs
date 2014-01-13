﻿using UnityEngine;
using System.Collections;

public class MenuLogic : MonoBehaviour {

	private GameObject global;

	public GameObject back;
	public GameObject mainGroup;
	public GameObject optionsGroup;
	public GameObject modeGroup;
	public GameObject onlineGroup;

	private GameObject actualGroup;
	private GameObject previousGroup;
	private string lastSelection;

	//temporanee
	private float width = 0;
	private float heigth = 0;
	private float inches = 0;

	// Use this for initialization
	void Start () {
		global = GameObject.Find("GlobalObject");
		global.GetComponent<Global>().SetOnlineGame(false);
		global.GetComponent<Global>().SetAmIPlayer1(false);
		global.GetComponent<Global>().SetTimeGame(false);
		actualGroup = mainGroup;
		lastSelection = "";
	}
	
	// Update is called once per frame
	void Update () {
		if (actualGroup.Equals(mainGroup)) {
			ManageMain();
			return;
		}
		if (actualGroup.Equals(modeGroup)) {
			ManageMode ();
			return;
		}
		if (actualGroup.Equals(onlineGroup)) {
			ManageOnline();
			return;
		}
		if (actualGroup.Equals(optionsGroup)) {
			ManageOptions();
			return;
		}
		/*
		if (actualGroup.Equals(creditsGroup)) {
			//gestione return
		}
		*/
	}

	private void ManageMain () {
		if (mainGroup.GetComponent<MenuMainGroup>().GetHaveISelected()) {
			lastSelection = mainGroup.GetComponent<MenuMainGroup>().GetSelectedButton();
			previousGroup = actualGroup;
			if (lastSelection.Equals("play")) {
				actualGroup = modeGroup;
			}
			if (lastSelection.Equals("playOnline")) {
				actualGroup = onlineGroup;
				global.GetComponent<Global>().SetOnlineGame(true);
			}
			if (lastSelection.Equals("options")) {
				actualGroup = optionsGroup;
			}
			/*
			if (lastSelection.Equals("credits")) {
				actualGroup = creditsGroup;
			}
			*/
			previousGroup.SetActive(false);
			actualGroup.SetActive(true);
			back.SetActive(true);
		}
	}

	private void ManageMode () {
		if (modeGroup.GetComponent<MenuModeGroup>().GetHaveISelected()) {
			lastSelection = modeGroup.GetComponent<MenuModeGroup>().GetSelectedButton();
			previousGroup = actualGroup;
			if (!global.GetComponent<Global>().GetOnlineGame()) {
				if (lastSelection.Equals("timePlay")) {
					global.GetComponent<Global>().SetTimeGame(true);
				}
				if (Screen.dpi <= 0) {
					Debug.Log(Screen.dpi.ToString());
					Application.LoadLevel("GameS");
					return;
				}
				width= Screen.width/ Screen.dpi;
				heigth= Screen.height/Screen.dpi;
				inches= Mathf.Sqrt(Mathf.Pow(width,2f)+Mathf.Pow(heigth,2f));
				Debug.Log(inches.ToString());
				if (inches>=6.5){
					Application.LoadLevel("Game");
				} else {
					Application.LoadLevel("GameS");
				}
			} else {
				//actualGroup settato ad attesa sfidante
				previousGroup.SetActive(false);
				actualGroup.SetActive(true);
			}
		} else if (back.GetComponent<MenuButton>().GetAmISelected()) {
			if (global.GetComponent<Global>().GetOnlineGame()) {
				global.GetComponent<Global>().SetAmIPlayer1(false);
				actualGroup.SetActive(false);
				actualGroup = onlineGroup;
				actualGroup.SetActive(true);
				previousGroup = mainGroup;

			} else {
				actualGroup.SetActive(false);
				back.SetActive(false);
				actualGroup = mainGroup;
				actualGroup.SetActive(true);
			}
		}
	}

	private void ManageOnline () {
		if (onlineGroup.GetComponent<MenuOnlineGroup>().GetHaveISelected()) {
			lastSelection = onlineGroup.GetComponent<MenuOnlineGroup>().GetSelectedButton();
			previousGroup = actualGroup;
			if (lastSelection.Equals("createMatch")) {
				actualGroup = modeGroup;
				global.GetComponent<Global>().SetAmIPlayer1(true);
			}
			if (lastSelection.Equals("searchMatch")) {
				//actualGroup = matchGroup;
			}
			previousGroup.SetActive(false);
			actualGroup.SetActive(true);
		} else if (back.GetComponent<MenuButton>().GetAmISelected()) {
			global.GetComponent<Global>().SetOnlineGame(false);
			actualGroup.SetActive(false);
			back.SetActive(false);
			actualGroup = mainGroup;
			actualGroup.SetActive(true);
		}
	}

	private void ManageOptions () {
		if (back.GetComponent<MenuButton>().GetAmISelected()) {
			actualGroup.SetActive(false);
			back.SetActive(false);
			actualGroup = mainGroup;
			actualGroup.SetActive(true);
		}
	}


}
