using UnityEngine;
using System.Collections;

public class Move : MonoBehaviour {
	PerlinNoise noise;

	public int octNum;
	public float frq;
	public float amp;
	// Use this for initialization
	void Start () {
		noise = new PerlinNoise(3497459);
	}

	// Update is called once per frame
	void Update () {
		float n1 = noise.FractalNoise1D(transform.position.x,octNum,frq,amp);
		float n2 = noise.FractalNoise1D(transform.position.x,octNum,frq+10,amp);
		//float t = noise.FractalNoise1D(transform.position.x,octNum+2,frq,amp);
		Vector2 offset = new Vector2(transform.position.x + 0.1f,n1+n2);
		transform.position = offset;
	}
}
