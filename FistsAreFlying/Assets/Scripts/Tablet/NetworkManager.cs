using UnityEngine;
using System.Collections;

public class NetworkManager : MonoBehaviour {

	private const string gameName = "FistsAreFlying";
	//se un giocatore crea il match deve avere la possibilità di scegliere gameName
	private const string roomName = "RoomName";

	private HostData[] hostList;

	// Use this for initialization
	void Start () {}
	
	// Update is called once per frame
	void Update () {}

	public void StartServer () {
		Network.InitializeServer(2, 25000, !Network.HavePublicAddress());
		MasterServer.RegisterHost(gameName, roomName);
	}

	void OnServerInitialized () {
		Debug.Log("Server initialized.");
	}

	public void RefreshHostList () {
		MasterServer.RequestHostList(gameName);
	}

	public HostData[] GetHostList () {
		if (hostList != null) {
			return hostList;
		}
		return null;
	}

	void OnMasterServerEvent (MasterServerEvent msEvent) {
		if (msEvent == MasterServerEvent.HostListReceived) {
			hostList = MasterServer.PollHostList();
		}
	}

	public void JoinServer (HostData hostData) {
		Network.Connect(hostData);
	}

	void OnConnectedToServer () {
		Debug.Log("Server joined.");
	}
}
