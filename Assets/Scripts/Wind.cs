using UnityEngine;
using System.Collections;

public class Wind : MonoBehaviour {

	public static Vector3 wind = new Vector2(Random.Range(-0.5f,0.5f),Random.Range(-0.5f,0.5f)).normalized;

	void Start () {

	}
	// Update is called once per frame
	void Update () {
	
	}
}
