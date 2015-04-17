using UnityEngine;
using System.Collections;

public class SpawnerShips : MonoBehaviour {

	public Transform shipPrefab;
	private ArrayList players = new ArrayList();

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnServerInitialized()
	{

	}

	void OnPlayerConnected(NetworkPlayer player)
	{
		SpawnPlayer(player);

	}

	void OnPlayerDisconnected(NetworkPlayer player)
	{
		
		foreach(Player script in players){
			if(player==script.owner){//We found the players object
				Network.RemoveRPCs(script.gameObject.networkView.viewID);//remove the bufferd SetPlayer call
				Network.Destroy(script.gameObject);//Destroying the GO will destroy everything
				players.Remove(script);//Remove this player from the list
				break;
			}
		}
		
		//Remove the buffered RPC call for instantiate for this player.
		int playerNumber = int.Parse(player+"");
		Network.RemoveRPCs(Network.player, playerNumber);
		
		
		// The next destroys will not destroy anything since the players never
		// instantiated anything nor buffered RPCs
		Network.RemoveRPCs(player);
		Network.DestroyPlayerObjects(player);
	}
	
	void SpawnPlayer(NetworkPlayer player)
	{
		int playerNum = int.Parse(player.ToString());
		Transform newTransform = (Transform)Network.Instantiate(shipPrefab, transform.position, transform.rotation,playerNum);
		NetworkView netView = newTransform.networkView;

		players.Add(newTransform.GetComponent("Player"));

		netView.RPC("SetPlayer", RPCMode.AllBuffered,player);

	}
}
