using UnityEngine;
using System.Collections;

public class NoiseCurveTest : MonoBehaviour {
	private PerlinNoise noise;
	private LineRenderer renderer;

	public int octaves;
	public float frequency;
	public float amplitude;

	public float offsetAmp;

	// Use this for initialization
	void Start () {
		noise = new PerlinNoise(Random.Range(-10000,10000));
		renderer = (LineRenderer)GetComponent(typeof(LineRenderer));

	}
	
	// Update is called once per frame
	void Update () {
		renderer.SetVertexCount(50);
		for(int i = 0;i<50;i++)
		{
			float n1 = noise.FractalNoise1D(i,octaves,frequency,amplitude);
			float n2 = noise.FractalNoise1D(i,octaves,frequency,amplitude+offsetAmp);
			this.renderer.SetPosition(i, new Vector2(i,n1+n2));
		}
	}
}
