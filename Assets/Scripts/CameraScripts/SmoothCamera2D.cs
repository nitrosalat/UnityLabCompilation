using UnityEngine;
using System.Collections;

public class SmoothCamera2D : MonoBehaviour {
	
	public float dampTime = 0.15f;
	private Vector3 velocity = Vector3.zero;
	public Transform target;

	public float zoomSensitivity= 15.0f;
	public float zoomSpeed= 5.0f;
	public float zoomMin= 5.0f;
	public float zoomMax= 80.0f;

	private float zoom;


	private bool mouseClicked = false;
	private Vector3 startPoint;
	// Update is called once per frame
	void Start()
	{
		zoom = camera.orthographicSize;
	}
	void Update () 
	{
		if (target)
		{
			Vector3 point = camera.WorldToViewportPoint(target.position);
			Vector3 delta = target.position - camera.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, point.z)); //(new Vector3(0.5, 0.5, point.z));
			Vector3 destination = transform.position + delta;
			transform.position = Vector3.SmoothDamp(transform.position, destination, ref velocity, dampTime);
			transform.up = -((Planet)GameObject.Find("Planet").GetComponent(typeof(Planet))).GetGravityVectorForPoint(transform.position);
		}
		zoom -= Input.GetAxis("Mouse ScrollWheel") * zoomSensitivity;
		zoom = Mathf.Clamp(zoom, zoomMin, zoomMax);
		//debug code
		if(Input.GetMouseButtonDown(0))
		{
			mouseClicked = true;
			startPoint = Input.mousePosition;
		} 
		if(Input.GetMouseButtonUp(0))
		{
			mouseClicked = false;
		}
		if(mouseClicked)
		{
			Vector3 endPoint = Input.mousePosition;
			Vector3 offset = endPoint-startPoint;
			transform.position = Vector3.Lerp(transform.position,transform.position + offset, Time.deltaTime);
		}


	}

	void LateUpdate() {
		camera.orthographicSize = Mathf.Lerp (camera.orthographicSize, zoom, Time.deltaTime * zoomSpeed);
	}
}