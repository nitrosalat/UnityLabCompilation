using UnityEngine;
using System.Collections;

public class Bird : MonoBehaviour {
	
	public float speed = 10.0f;
	public float maxAngularSpeed = 1.0f;

	public float gravityofTouch = 1.0f;
	private Transform sprite;

	public Planet planet;
	// Use this for initialization
	void Start () {
		sprite = GameObject.Find("Sprite").transform;
	}
	
	// Update is called once per frame
	void FixedUpdate()
	{
		if(Input.GetKey(KeyCode.Space))
		{
			Vector2 dirGrav = planet.GetGravityVectorForPoint(transform.position);


				rigidbody2D.AddForce(dirGrav * gravityofTouch);

			if(rigidbody2D.angularVelocity < maxAngularSpeed)
			{
				rigidbody2D.AddTorque(-speed);
			}
		}
	}
	void Update () {
		sprite.right = Vector2.Lerp(rigidbody2D.velocity.normalized,sprite.right,Time.deltaTime);

	}
}
