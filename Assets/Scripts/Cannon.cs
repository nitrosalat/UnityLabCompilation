using UnityEngine;
using System.Collections;

public class Cannon : MonoBehaviour {

	public Transform pointOfSpawnBalls;
	public GameObject ball;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.Space))
		{
			ball = (GameObject)Instantiate(ball, pointOfSpawnBalls.position, Quaternion.identity);
			ball.rigidbody2D.velocity = transform.right * 100;

		}
		if(Input.GetKey(KeyCode.LeftArrow))
		{
			transform.Rotate(0,0,1);
		}
		if(Input.GetKey(KeyCode.RightArrow))
		{
			transform.Rotate(0,0,-1);
		}
	}
}
