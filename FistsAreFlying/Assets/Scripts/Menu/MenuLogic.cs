using UnityEngine;
using System.Collections;

public class MenuLogic : MonoBehaviour {

	private GameObject global;
	public GameObject networkManager;

	public GameObject menuLoop;
	public GameObject back;
	public GameObject mainGroup;
	public GameObject optionsGroup;
	public GameObject modeGroup;
	public GameObject onlineGroup;
	public GameObject createMatchGroup;
	public GameObject searchMatchGroup;
	public GameObject creditsGroup;

	public GameObject opponentWaitingText;
	public GameObject matchCreationFailedText;
	public GameObject connectingText;
	public GameObject noHostText;
	public GameObject connectionFailedText;

	private bool masterServerError = false;
	private HostData[] hostList = null;

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
		if (global.GetComponent<Global>().GetSound()){
			menuLoop.SetActive(true);
		}
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
		if (actualGroup.Equals(creditsGroup)) {
			ManageCredits();
		}
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
			if (lastSelection.Equals("credits")) {
				actualGroup = creditsGroup;
			}
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
				Application.LoadLevel("TGame");
			} else {
				string roomName = "";
				networkManager.GetComponent<NetworkManager>().RefreshHostList();
				if (lastSelection.Equals("timePlay")) {
					global.GetComponent<Global>().SetTimeGame(true);
					global.GetComponent<Global>().SetOnlineTime(global.GetComponent<Global>().GetTime());
					roomName = roomName + "Time Play: " + global.GetComponent<Global>().GetTime().ToString();
				}
				if (lastSelection.Equals("standardPlay")) {
					roomName = roomName + "Standard Play";
				}
				actualGroup = createMatchGroup;
				previousGroup.SetActive(false);
				networkManager.GetComponent<NetworkManager>().StartServer(roomName);
				opponentWaitingText.SetActive(true);
				actualGroup.SetActive(true);
			}
		} else if (back.GetComponent<MenuButton>().GetAmISelected() || Input.GetKeyDown(KeyCode.Escape)) {
			if (global.GetComponent<Global>().GetOnlineGame()) {
				networkManager.GetComponent<NetworkManager>().Unregister();
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
				actualGroup = searchMatchGroup;
				previousGroup.SetActive(false);
				networkManager.GetComponent<NetworkManager>().RefreshHostList();
				connectingText.SetActive(true);
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
		if (masterServerError) {
			opponentWaitingText.SetActive(false);
			matchCreationFailedText.SetActive(true);
			masterServerError = false;
		}
		if (back.GetComponent<MenuButton>().GetAmISelected() || Input.GetKeyDown(KeyCode.Escape)) {
			MasterServer.UnregisterHost();
			Network.Disconnect();
			global.GetComponent<Global>().SetTimeGame(false);
			actualGroup.SetActive(false);
			opponentWaitingText.SetActive(false);
			matchCreationFailedText.SetActive(false);
			previousGroup.SetActive(true);
			actualGroup = previousGroup;
		}
	}

	private void ManageSearchGroup () {
		if (back.GetComponent<MenuButton>().GetAmISelected() || Input.GetKeyDown(KeyCode.Escape)) {
			actualGroup.SetActive(false);			
			connectingText.SetActive(false);
			noHostText.SetActive(false);
			connectionFailedText.SetActive(false);
			hostList = null;
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

	private void ManageCredits () {
		if (back.GetComponent<MenuButton>().GetAmISelected() || Input.GetKeyDown(KeyCode.Escape)) {
			actualGroup.SetActive(false);
			back.SetActive(false);
			previousGroup.SetActive(true);
			actualGroup = previousGroup;
		}
	}

	void OnFailedToConnect () {
		connectingText.SetActive(false);
		connectionFailedText.SetActive(true);
	}

	void OnFailedToConnectToMasterServer () {
		if (Network.isServer) {
			masterServerError = true;
		} else {
			connectingText.SetActive(false);
			noHostText.SetActive(true);
			return;
		}
	}

	void OnMasterServerEvent (MasterServerEvent msEvent) {
		if (msEvent == MasterServerEvent.HostListReceived && !Network.isServer) {
			hostList = MasterServer.PollHostList();
			if (hostList.Length > 0) {
				networkManager.GetComponent<NetworkManager>().JoinServer(hostList[0]);
			} else {
				connectingText.SetActive(false);
				noHostText.SetActive(true);
			}
		}
	}
	                      
}
