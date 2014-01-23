using UnityEngine;
using System.Collections;

public class NetworkManager : MonoBehaviour {

	private GameObject global;
	private GameObject gameLogic;
	private GameObject forcedPause;
	private bool inGame = false;
	private bool connectionEnded = false;

	private const string gameName = "FistsAreFlying";
	//se un giocatore crea il match deve avere la possibilità di scegliere gameName
	//private const string roomName = "";

	// Use this for initialization
	void Start () {
		global = GameObject.Find("GlobalObject");
		GameObject.DontDestroyOnLoad(this);
	}
	
	// Update is called once per frame
	void Update () {
		if (!inGame) {
			if (Application.loadedLevelName.Equals("TGame")) {
				inGame = true;
				gameLogic = GameObject.Find("GameLogic");
				forcedPause = GameObject.Find("ConnectionEnded");
			}
		}
		if (inGame) {
			if (Application.loadedLevelName.Equals("MainMenu")) {
				inGame = false;
			}
		}
		if (connectionEnded) {
			if (forcedPause.GetComponent<MenuConnectionEndedGroup>().GetQuitSelected() || Input.GetKeyDown(KeyCode.Escape)) {
				connectionEnded = false;
				inGame = false;
				gameLogic = null;
				forcedPause = null;
				Application.LoadLevel("MainMenu");
			}
		}
	}

	public void StartServer (string roomName) {
		Network.InitializeServer(2, 25000, !Network.HavePublicAddress());
		MasterServer.RegisterHost(gameName, roomName);
	}	
	
	void OnServerInitialized () {
		Debug.Log("Server initialized.");
		
	}

	public void RefreshHostList () {
		MasterServer.RequestHostList(gameName);
	}

	public void JoinServer (HostData hostData) {
		Network.Connect(hostData);
	}

	void OnConnectedToServer () {
		Debug.Log("Server joined.");
		Application.LoadLevel("TGame");
	}

	void OnPlayerConnected () {
		Debug.Log("Opponent connected.");
		Unregister();
		if (global.GetComponent<Global>().GetTimeGame()) {
			networkView.RPC("ComunicateTimeGame", RPCMode.Others);
			networkView.RPC("ComunicateTime", RPCMode.Others, global.GetComponent<Global>().GetTime());
		}
		Application.LoadLevel("TGame");
	}

	public void Unregister () {
		MasterServer.UnregisterHost();
	}

	void OnDisconnectedFromServer () {
		if (Network.isClient && inGame) {
			gameLogic.SetActive(false);
			connectionEnded = true;
			forcedPause.GetComponent<Transform>().position = new Vector3(Camera.main.GetComponent<Transform>().position.x, Camera.main.GetComponent<Transform>().position.y, 0);
		}
	}

	void OnPlayerDisconnected () {
		if (inGame) {
			gameLogic.SetActive(false);
			connectionEnded = true;
			forcedPause.GetComponent<Transform>().position = new Vector3(Camera.main.GetComponent<Transform>().position.x, Camera.main.GetComponent<Transform>().position.y, 0);
		}
	}
	
	
	[RPC] void ComunicateTimeGame () {
		global.GetComponent<Global>().SetTimeGame(true);
	}
	
	[RPC] void ComunicateTime (int time) {
		global.GetComponent<Global>().SetOnlineTime(time);
	}
}
