using System;
using UnityEngine;
using System.Collections.Generic;

public class CreateCone : MonoBehaviour
{
	[SerializeField] 
	private int radius, hauteur, nbMeridiens;

	[SerializeField]
	[Tooltip("Si hauteurTronquage == hauteur le cone ne sera pas tronqué")]
	private int hauteurTronquage;
    
	[SerializeField]
	private Material material;
    
	private List<Vector3> points = new();
    
	private List<int> triangles = new();

	void Start()
	{
		if (nbMeridiens == 0)
		{
			Debug.LogError("Nb meridiens est vide.");
			return;
		}
		
		float theta = 360 / nbMeridiens;
		float topY = hauteurTronquage;
		float bottomY = 0;
		
		float z = Mathf.Sqrt(Mathf.Pow(hauteurTronquage - hauteur, 2) * Mathf.Pow(radius, 2) / Mathf.Pow(hauteur, 2));

		Vector3 point = new(0, hauteurTronquage, z);
		Vector3 point2 =  new(0, hauteurTronquage, 0);
		
		float rTop = Mathf.Sqrt(Mathf.Pow(point2.x - point.x, 2) + Mathf.Pow(point2.y - point.y, 2) + Mathf.Pow(point2.z - point.z, 2));
		
		int indCenterTop = points.Count;
		points.Add(new(0, topY, 0));
		int indCenterBot = points.Count;
		points.Add(new(0, bottomY, 0));
		
		for (float i = 0; i < 360; i += theta)
		{
			int t1 = points.Count;
			points.Add(new Vector3(Mathf.Cos((i * Mathf.PI)/180)*radius, bottomY, Mathf.Sin((i * Mathf.PI)/180)*radius));
			int t2 = points.Count;
			points.Add(new Vector3(Mathf.Cos((i * Mathf.PI)/180)*rTop, topY, Mathf.Sin((i * Mathf.PI)/180)*rTop));
			int t3 = points.Count;
			points.Add(new Vector3(Mathf.Cos(((i + theta) * Mathf.PI)/180)*radius, bottomY, Mathf.Sin(((i + theta) * Mathf.PI)/180)*radius));
			int t4 = points.Count;
			points.Add(new Vector3(Mathf.Cos(((i + theta) * Mathf.PI)/180)*rTop, topY, Mathf.Sin(((i + theta) * Mathf.PI)/180)*rTop));

			if (hauteurTronquage < hauteur)
			{
				triangles.AddRange(new int[] { t1, t2, t3 });
				triangles.AddRange(new int[] { t4, t3, t2 });
				triangles.AddRange(new int[] { t2, indCenterTop, t4 });
				triangles.AddRange(new int[] { t3, indCenterBot, t1 });
			}
			else
			{
				triangles.AddRange(new int[] { t1, indCenterTop, t3 });
			}
		}
        
		MeshFilter filter = gameObject.AddComponent<MeshFilter>();
		Mesh mesh = filter.mesh;
		mesh.vertices = points.ToArray();
		mesh.triangles = triangles.ToArray();
        
		MeshRenderer meshRenderer = gameObject.AddComponent<MeshRenderer>();
		meshRenderer.material = material;
	}
}