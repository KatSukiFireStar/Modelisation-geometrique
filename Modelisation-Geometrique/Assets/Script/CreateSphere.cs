using UnityEngine;
using System.Collections.Generic;

public class CreateSphere : MonoBehaviour
{
	[SerializeField] 
	private int radius, nbMeridiens, nbParalleles;
    
	[SerializeField]
	private Material material;
    
	private List<Vector3> points = new();
    
	private List<int> triangles = new();
    
	void Start()
	{
		if (nbParalleles == 0 || nbMeridiens == 0)
		{
			Debug.LogError("Nb paralleles ou Nb meridiens est vide.");
			return;
		}
		
		float theta = 360 / nbMeridiens;
		float phi = 180 / nbParalleles;

		int indNorthPole = points.Count;
		points.Add(new(0, radius, 0));
		int indSouthPole = points.Count;
		points.Add(new(0, -radius, 0));
        
		for (float i = 0; i < 180; i += phi)
		{
			for (float j = 0; j < 360; j += theta)
			{
				if (nbParalleles % 2 == 1)
				{
					int t1 = points.Count;
					points.Add(new Vector3(radius, 0, 0));
					int t2 = points.Count;
					points.Add(new Vector3(Mathf.Cos((phi * Mathf.PI)/180), Mathf.Sin((theta * Mathf.PI)/180), 0));
					int t3 = points.Count;
					points.Add(new Vector3(Mathf.Sin(((i + theta) * Mathf.PI)/180)*radius, 0, Mathf.Cos(((i + theta) * Mathf.PI)/180)*radius));
					int t4 = points.Count;
					points.Add(new Vector3(Mathf.Cos(((i + theta) * Mathf.PI)/180)*radius, radius, Mathf.Sin(((j+phi) * Mathf.PI)/180)));
            
					triangles.AddRange(new int[] { t1, t2, t3 });
					//triangles.AddRange(new int[] { t4, t3, t2 });
					break;
				}
			}
			break;
		}
        
		MeshFilter filter = gameObject.AddComponent<MeshFilter>();
		Mesh mesh = filter.mesh;
		mesh.vertices = points.ToArray();
		mesh.triangles = triangles.ToArray();
        
		MeshRenderer meshRenderer = gameObject.AddComponent<MeshRenderer>();
		meshRenderer.material = material;
	}
}