using UnityEngine;
using System.Collections;

public class Networking : MonoBehaviour {

	public string connectionIP = "87.237.116.242";
	public int connectionPort = 25001;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	
	void OnGUI ()
	{
		
		if (Network.peerType == NetworkPeerType.Disconnected){
			//We are currently disconnected: Not a client or host
			GUILayout.Label("Connection status: Disconnected");
			
			connectionIP = GUILayout.TextField(connectionIP, GUILayout.MinWidth(100));
			connectionPort = int.Parse(GUILayout.TextField(connectionPort.ToString()));
			
			GUILayout.BeginVertical();
			if (GUILayout.Button ("Подключиться как клиент"))
			{
				//Connect to the "connectToIP" and "connectPort" as entered via the GUI
				//Ignore the NAT for now
				Network.useNat = true;
				Network.Connect(connectionIP, connectionPort);
			}
			
			if (GUILayout.Button ("Сервер(не трогать)"))
			{
				//Start a server for 32 clients using the "connectPort" given via the GUI
				//Ignore the nat for now	
				Network.useNat = true;
				Network.InitializeServer(32, connectionPort);
			}
			GUILayout.EndVertical();
			
			
		}else{
			//We've got a connection(s)!
			switch(Network.peerType)
			{
				case NetworkPeerType.Connecting:
					GUILayout.Label("Connection status: Connecting");
					break;
				case NetworkPeerType.Client:
					GUILayout.Label("Connection status: Client!");
					GUILayout.Label("Ping to server: "+Network.GetAveragePing(  Network.connections[0] ) );	
					break;
				case NetworkPeerType.Server:
					GUILayout.Label("Connection status: Server!");
					GUILayout.Label("Connections: "+Network.connections.Length);
					break;
			}
			
			if (GUILayout.Button ("Disconnect"))
			{
				Network.Disconnect(200);
			}
		}
		
		
	}
}
