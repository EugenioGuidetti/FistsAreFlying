using UnityEngine;
using System.Collections;

public class MenuLogic : MonoBehaviour {

	private GameObject global;
	public GameObject networkManager;

	public GameObject back;
	public GameObject mainGroup;
	public GameObject optionsGroup;
	public GameObject modeGroup;
	public GameObject onlineGroup;
	public GameObject createMatchGroup;
	public GameObject searchMatchGroup;

	private GameObject actualGroup;
	private GameObject previousGroup;
	private string lastSelection;

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
		if (actualGroup.Equals(createMatchGroup)) {
			ManageCreateGroup();
			return;
		}
		if (actualGroup.Equals(searchMatchGroup)) {
			ManageSearchGroup();
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
		} else if (Input.GetKeyDown(KeyCode.Escape)) {
			Application.Quit();
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
				if (global.GetComponent<Global>().GetTablet()){
					Application.LoadLevel("TGame");
				} else {
					Application.LoadLevel("SGame");
				}
			} else {
				string roomName = "room ";
				networkManager.GetComponent<NetworkManager>().RefreshHostList();
				if (networkManager.GetComponent<NetworkManager>().GetHostList() != null) {
					roomName = roomName + (networkManager.GetComponent<NetworkManager>().GetHostList().Length + 1).ToString() + ": ";
				} else {
					roomName = roomName + "1: ";
				}
				if (lastSelection.Equals("timePlay")) {
					global.GetComponent<Global>().SetTimeGame(true);
					roomName = roomName + "Time Play " + global.GetComponent<Global>().GetTime().ToString() + "s";
				}
				if (lastSelection.Equals("standardPlay")) {
					roomName = roomName + "Standard Play";
				}
				actualGroup = createMatchGroup;
				previousGroup.SetActive(false);
				networkManager.GetComponent<NetworkManager>().StartServer(roomName);
				actualGroup.SetActive(true);
			}
		} else if (back.GetComponent<MenuButton>().GetAmISelected() || Input.GetKeyDown(KeyCode.Escape)) {
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
				previousGroup.SetActive(false);
				actualGroup.SetActive(true);
			}
			if (lastSelection.Equals("searchMatch")) {
				HostData[] hostList;
				actualGroup = searchMatchGroup;
				previousGroup.SetActive(false);
				networkManager.GetComponent<NetworkManager>().RefreshHostList();
				hostList = networkManager.GetComponent<NetworkManager>().GetHostList();
				if (hostList != null) {					
					networkManager.GetComponent<NetworkManager>().JoinServer(hostList[0]);
					//qui vanno creati i bottoni per scegliere l'host a cui collegarsi
				}
				actualGroup.SetActive(true);
			}
		} else if (back.GetComponent<MenuButton>().GetAmISelected() || Input.GetKeyDown(KeyCode.Escape)) {
			global.GetComponent<Global>().SetOnlineGame(false);
			actualGroup.SetActive(false);
			back.SetActive(false);
			actualGroup = mainGroup;
			actualGroup.SetActive(true);
		}
	}

	private void ManageCreateGroup () {
		if (back.GetComponent<MenuButton>().GetAmISelected() || Input.GetKeyDown(KeyCode.Escape)) {
			MasterServer.UnregisterHost();
			global.GetComponent<Global>().SetTimeGame(false);
			actualGroup.SetActive(false);
			previousGroup.SetActive(true);
			actualGroup = previousGroup;
		}
	}

	private void ManageSearchGroup () {
		if (back.GetComponent<MenuButton>().GetAmISelected() || Input.GetKeyDown(KeyCode.Escape)) {
			actualGroup.SetActive(false);
			back.SetActive(false);
			previousGroup.SetActive(true);
			actualGroup = previousGroup;
		}
	}

	private void ManageOptions () {
		if (back.GetComponent<MenuButton>().GetAmISelected() || Input.GetKeyDown(KeyCode.Escape)) {
			actualGroup.SetActive(false);
			back.SetActive(false);
			previousGroup.SetActive(true);
			actualGroup = previousGroup;
		}
	}
}
