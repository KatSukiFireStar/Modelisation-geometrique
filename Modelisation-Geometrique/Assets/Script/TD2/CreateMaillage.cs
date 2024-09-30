using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;

public class CreateMaillage : MonoBehaviour
{
	[SerializeField] 
	private string fileName;

	[SerializeField]
	private Material material;
    
	private List<Vector3> points = new();
    
	private List<int> triangles = new();
	
	private Vector3 pointCenter = new();
	private float maxPointX = float.MinValue;
	private float maxPointY = float.MinValue;
	private float maxPointZ = float.MinValue;
	
	private float minPointY = float.MaxValue;

	void Start()
	{
		LoadMaillage();
		TraceMaillage();
	}

	private void LoadMaillage()
	{
		StreamReader reader = new(Path.Combine("Assets/Maillage", fileName), Encoding.ASCII);
		reader.ReadLine();
		string line = reader.ReadLine();
		string[] values = line.Split(' ');
		int nbPoints = int.Parse(values[0]);
		int nbTriangles = int.Parse(values[1]);
		
		for (int i = 0; i < nbPoints; i++)
		{
			line = reader.ReadLine();
			line = line.Replace(".", ",");
			values = line.Split(' ');
			Vector3 point = new(float.Parse(values[0]), float.Parse(values[1]), float.Parse(values[2]));
			
			points.Add(point);
		}
		
		for (int i = 0; i < nbTriangles; i++)
		{
			line = reader.ReadLine();
			values = line.Split(' ');
			for (int j = 1; j <= int.Parse(values[0]); j++)
			{
				triangles.Add(int.Parse(values[j]));
			}
		}
	}

	private void TraceMaillage()
	{
		MeshFilter filter = gameObject.AddComponent<MeshFilter>();
		Mesh mesh = filter.mesh;
		Vector3[] vertices = points.ToArray();

		// for (int i = 0; i < vertices.Length; i++)
		// {
		// 	if (vertices[i].y < minPointY)
		// 	{
		// 		minPointY = vertices[i].y;
		// 	}
		// }
		
		// vertices[i] -= new Vector3(0, minPointY, 0);
		
		for (int i = 0; i < vertices.Length; i++)
		{
			pointCenter += vertices[i];
		}
		pointCenter /= vertices.Length;
		
		for (int i = 0; i < vertices.Length; i++)
		{
			vertices[i] -= pointCenter;
		}
		pointCenter -= pointCenter;
		//
		// for (int i = 0; i < vertices.Length; i++)
		// {
		// 	if (Mathf.Abs(vertices[i].x) > maxPointX)
		// 	{
		// 		maxPointX = Mathf.Abs(vertices[i].x);
		// 	}
		//
		// 	if (Mathf.Abs(vertices[i].y) > maxPointY)
		// 	{
		// 		maxPointY = Mathf.Abs(vertices[i].y);
		// 	}
		//
		// 	if (Mathf.Abs(vertices[i].z) > maxPointZ)
		// 	{
		// 		maxPointZ = Mathf.Abs(vertices[i].z);
		// 	}
		// }
		//
		//
		// for (int i = 0; i < vertices.Length; i++)
		// {
		// 	vertices[i].x /= maxPointX;
		// 	vertices[i].y /= maxPointY;
		// 	vertices[i].z /= maxPointZ;
		// }
		
		mesh.vertices = vertices;
		mesh.triangles = triangles.ToArray();
		mesh.bounds = new Bounds(pointCenter, mesh.bounds.size);
        
		MeshRenderer meshRenderer = gameObject.AddComponent<MeshRenderer>();
		meshRenderer.material = material;
	}

}