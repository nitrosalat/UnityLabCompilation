using UnityEngine;
using System.Collections;

public class Ship : MonoBehaviour {

	public GameObject sail;
	public GameObject keel;
	public GameObject rudder;
	private Vector2 wind = Wind.wind;

	private bool leftInput = false;
	private bool rightInput = false;


	private HingeJoint2D rudderJoint;
	// Use this for initialization
	void Start () {
		rudderJoint = (HingeJoint2D)rudder.GetComponent("HingeJoint2D");

		if(Network.isClient)
		{
			sail.rigidbody2D.isKinematic = true;
		}

	}
	
	// Update is called once per frame
	void Update () {
		if(leftInput)
		{
			JointMotor2D motor = new JointMotor2D();
			motor.motorSpeed = 20;
			motor.maxMotorTorque = 10000;
			rudderJoint.motor = motor;
			
		}
		else if(rightInput)
		{
			JointMotor2D motor = new JointMotor2D();
			motor.motorSpeed = -20;
			motor.maxMotorTorque = 10000;
			rudderJoint.motor = motor;
		}
		else
		{
			JointMotor2D motor = new JointMotor2D();
			motor.motorSpeed = 0;
			motor.maxMotorTorque = 10000;
			rudderJoint.motor = motor;
		}
	}
	void FixedUpdate()
	{






		Vector2 vecSail = sail.transform.right;
		float sinWind = Mathf.Abs(Mathf.Sin(Vector3.Angle(vecSail,wind)));
		sinWind *= 5;
		Vector2 forceByWind = transform.right*sinWind;
		rigidbody2D.AddForce(forceByWind);


		sail.rigidbody2D.AddForce(wind);

		KillOrthogonalVelocity(keel.rigidbody2D,0.1f);
		KillOrthogonalVelocity(rudder.rigidbody2D, 0f);
		////////////////rudder factor

		float speed = rigidbody2D.velocity.magnitude;
		float angleRudderShell = Vector3.Angle(transform.right, rudder.transform.right);

		//Vector2 rudderForce = rudder.transform.up * speed;

		//rudder.rigidbody2D.AddForce(rudderForce);

	}
	

	public void SetInput(bool left, bool right)
	{
		leftInput = left;
		rightInput = right;
	}

	public static void KillOrthogonalVelocity(Rigidbody2D body,float drift )
	{
		Vector2 forwardVelocity = body.transform.right * Vector2.Dot(body.velocity, body.transform.right);
		Vector2 rightVelocity = body.transform.right * Vector2.Dot(body.velocity, body.transform.up);
		body.velocity = forwardVelocity + rightVelocity * drift;
	}
}
