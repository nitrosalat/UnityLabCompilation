using UnityEngine;
using System.Collections;

public class TerrainGeneration : MonoBehaviour {

	public int lenght = 1000;

	private PerlinNoise noise;

	public int oct;
	public float frq;
	public float amp;

	private EdgeCollider2D collider;
	// Use this for initialization
	void Start () {
		noise = new PerlinNoise(Random.Range(-1000000,1000000));

		collider = (EdgeCollider2D)GetComponent(typeof(EdgeCollider2D));

	}
	
	// Update is called once per frame
	void Update () {
		Generate();
	}

	void Generate()
	{
		Vector2[] points = new Vector2[lenght];
		for(int i = 0;i<lenght;i++)
		{
			float h = noise.FractalNoise1D(i,oct,frq,amp);
			points[i] = new Vector2(i,h);
		}
		this.collider.points = points;
	}
}
