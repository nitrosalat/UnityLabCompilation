using UnityEngine;
using System.Collections;

public class GravityObjects : MonoBehaviour {

	// Use this for initialization
	public Planet[] planets;
	Vector2 startingVelocity = Vector2.up;
	private Vector2 dir;
	private Vector2[] orbitPoints;
	public int maxCount = 10000;
	public int simplify = 5;
	private int privateMaxCount;
	private LineRenderer lineRenderer;
	private float gravity;
	void Start () {
		privateMaxCount = maxCount;
		orbitPoints = new Vector2[privateMaxCount];
		lineRenderer = (LineRenderer)gameObject.AddComponent(typeof(LineRenderer));
		lineRenderer.SetWidth(0.2f,0.2f);
		lineRenderer.material.color = Color.red; //(Material)Resources.Load("Assets/Materials/TrajectoryLine", typeof(Material));
		ComputeTrajectory();
	}
	void Update(){
		ComputeTrajectory();
	}
	void ComputeTrajectory () {
		float angle  = 0;
		float dt  = Time.fixedDeltaTime;
		Vector2 s = transform.position;
		Vector2 lastS = s;
		Vector2 v = rigidbody2D.velocity;
		Vector2 a = AccelerationCalc(planets, s);
		float tempAngleSum = 0;
		int step = 0;
		while(angle < 360 && step < privateMaxCount*simplify){
			if(step % simplify == 0){
				orbitPoints[step/simplify] = s;
				angle += tempAngleSum;
				tempAngleSum = 0;
			}
			a = AccelerationCalc(planets, s);
			v += a*dt;
			s += v*dt;
			if(planets.Length == 1){
				tempAngleSum += Mathf.Abs(Vector3.Angle(s, lastS));
			}
			lastS = s;
			step ++;
		}
		lineRenderer.SetVertexCount(step/simplify);
		for(int i = 0; i < step/simplify; i++){
		
			lineRenderer.SetPosition(i, orbitPoints[i]);
		}
	}
	Vector2 AccelerationCalc(Planet[] planetsArray,Vector3 simPos){
		Vector2 a  = Vector2.zero;
		for(int i = 0; i < planetsArray.Length; i++){
			Planet planet = planetsArray[i];


			dir = planet.transform.position - simPos;
			gravity = planet.gravityCoeff;
			a += (dir.normalized*gravity * this.rigidbody2D.mass*planet.rigidbody2D.mass) /dir.sqrMagnitude;
		}
		return a;
	}
}
