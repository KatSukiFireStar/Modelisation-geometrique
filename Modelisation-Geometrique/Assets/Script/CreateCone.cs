using UnityEngine;
using System.Collections.Generic;

public class CreateCone : MonoBehaviour
{
	[SerializeField] 
	private int radius, hauteur, nbMeridiens;

	[SerializeField]
	[Tooltip("Si hauteur le cone ne sera pas tronqué")]
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
		
		int indCenterTop = points.Count;
		points.Add(new(0, topY, 0));
		int indCenterBot = points.Count;
		points.Add(new(0, bottomY, 0));
		
		for (float i = 0; i < 360; i += theta)
		{
			int t1 = points.Count;
			points.Add(new Vector3(Mathf.Cos((i * Mathf.PI)/180)*radius, bottomY, Mathf.Sin((i * Mathf.PI)/180)*radius));
			int t2 = points.Count;
			points.Add(new Vector3(Mathf.Cos((i * Mathf.PI)/180)*radius, topY, Mathf.Sin((i * Mathf.PI)/180)*radius));
			int t3 = points.Count;
			points.Add(new Vector3(Mathf.Cos(((i + theta) * Mathf.PI)/180)*radius, bottomY, Mathf.Sin(((i + theta) * Mathf.PI)/180)*radius));
			int t4 = points.Count;
			points.Add(new Vector3(Mathf.Cos(((i + theta) * Mathf.PI)/180)*radius, topY, Mathf.Sin(((i + theta) * Mathf.PI)/180)*radius));

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