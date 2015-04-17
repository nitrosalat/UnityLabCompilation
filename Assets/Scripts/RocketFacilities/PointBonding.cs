using UnityEngine;
using System.Collections;

public class PointBonding : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		float defaultDistance = 5;
		
		//Make a raycast from the mouse position
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		RaycastHit hit;
		
		Vector3 pos;
		
		//If the ray hits something, get the position of the hit
		if (Physics.Raycast(ray, out hit, defaultDistance)){
			pos = hit.point;
			
		}else{
			//If it doesn't hit anything, the Ray object will let you get the point it would have hit at a specified distance
			pos = ray.GetPoint(defaultDistance);
			
		}
		
		//Set the position
		transform.position = pos;
	}
	void OnTriggerEnter2D(Collider2D collider)
	{


	}
}
