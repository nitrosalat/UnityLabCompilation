using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

	private	Ship scriptShipComponent;

	public NetworkPlayer owner;

	private bool lastLeftInput;
	private bool lastRightInput;

	private bool serverCurrentLeftInput;
	private bool serverCurrentRightInput;

	
	// Use this for initialization
	void Awake()
	{
		scriptShipComponent = GetComponent("Ship") as Ship;
		if(Network.isClient)
		{
			enabled=false;
		}
		else if(Network.isServer)
		{
			networkView.RPC("SetWindVector",RPCMode.OthersBuffered, Wind.wind);
		}

	}

	void Start () {
		//(GameObject.Find("Camera").GetComponent("FollowCamera") as FollowCamera).target = scriptShipComponent.transform;
	}
	
	// Update is called once per frame
	void Update () {
	
		if(owner!=null&&Network.player == owner)
		{
			bool leftArrow = Input.GetKey(KeyCode.LeftArrow);
			bool rightArrow = Input.GetKey(KeyCode.RightArrow);

			if(lastLeftInput != leftArrow || lastRightInput != rightArrow)
			{
				lastLeftInput = leftArrow;
				lastRightInput = rightArrow;

				if(Network.isClient)
				{
					networkView.RPC("SendInput", RPCMode.Server, leftArrow,rightArrow);
				}
			}
		}
		if(Network.isServer)
		{
			scriptShipComponent.SetInput(serverCurrentLeftInput, serverCurrentRightInput);
		}
	}

	void OnNetworkInstantiate(NetworkMessageInfo info)
	{

	}

	[RPC]
	void SetPlayer(NetworkPlayer player)
	{
		owner = player;
		if(player == Network.player)
		{
			enabled = true;
			(GameObject.Find("Camera").GetComponent("FollowCamera") as FollowCamera).target = scriptShipComponent.transform;
		}


	}
	[RPC]
	void SendInput(bool left, bool right)
	{
		serverCurrentLeftInput = left;
		serverCurrentRightInput = right;
	}
	[RPC]
	void SetWindVector(Vector3 vec)
	{
		Wind.wind = vec;
	}


}
