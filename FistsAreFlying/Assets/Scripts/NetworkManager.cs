using UnityEngine;
using System.Collections;

public class NetworkManager : MonoBehaviour {

	private GameObject global;

	private const string gameName = "FistsAreFlying";

	// Use this for initialization
	void Start () {
		global = GameObject.Find("GlobalObject");
	}
	
	// Update is called once per frame
	void Update () {}

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
		Debug.Log("Client connected.");
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
	
	
	[RPC] void ComunicateTimeGame () {
		global.GetComponent<Global>().SetTimeGame(true);
	}
	
	[RPC] void ComunicateTime (int time) {
		global.GetComponent<Global>().SetOnlineTime(time);
	}
}
