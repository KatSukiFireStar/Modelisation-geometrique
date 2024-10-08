using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;
using UnityEngine;

public class CreateMaillage : MonoBehaviour
{
	[SerializeField] 
	private string fileName;

	[SerializeField]
	private Material material;
    
	private Vector3[] vertices;
	private List<int> triangles = new();
	private Vector3[] normals;
	
	private Vector3 pointCenter = new();

	void Start()
	{
		LoadMaillage();
		TraceMaillage();
		WriteMaillage();
	}

	private void LoadMaillage()
	{
		StreamReader reader = new(Path.Combine("Assets/Maillage", fileName+".off"), Encoding.ASCII);
		reader.ReadLine();
		string line = reader.ReadLine();
		string[] values = line.Split(' ');
		int nbPoints = int.Parse(values[0]);
		int nbTriangles = int.Parse(values[1]);
		List<Vector3> points = new();
		
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
		vertices = points.ToArray();
	}

	private void TraceMaillage()
	{
		MeshFilter filter = gameObject.AddComponent<MeshFilter>();
		Mesh mesh = filter.mesh;
		
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
		
		Vector3 max = new(0,0,0);
		float maxF = float.MinValue;
		for (int i = 0; i < vertices.Length; i++)
		{
			if (maxF < vertices[i].magnitude)
			{
				max = vertices[i];
				maxF = vertices[i].magnitude;
			}
		}
		for (int i = 0; i < vertices.Length; i++)
		{
			vertices[i] /= max.magnitude;
		}
		

		normals = new Vector3[vertices.Length];
		int[] count = new int[vertices.Length];
		for (int i = 0; i < triangles.Count; i+=3)
		{
			int[] tab = { triangles[i], triangles[i + 1], triangles[i + 2] };
			
			Vector3 a = vertices[tab[0]] - vertices[tab[1]];
			Vector3 b = vertices[tab[0]] - vertices[tab[2]];
			Vector3 surfNormal = Vector3.Cross(a, b).normalized;

			for (int j = 0; j < 3; j++)
			{
				if (normals[tab[j]].magnitude == 0)
				{
					normals[tab[j]] = surfNormal;
					count[tab[j]] = 1;
				}
				else
				{
					normals[tab[j]] += surfNormal;
					count[tab[j]] += 1;
				}
			}
		}

		for (int i = 0; i < normals.Length; i++)
		{
			normals[i] = (normals[i] / count[i]).normalized;
		}
		
		mesh.vertices = vertices;
		mesh.triangles = triangles.ToArray();
		mesh.normals = normals;
		mesh.bounds = new Bounds(pointCenter, mesh.bounds.size);
        
		MeshRenderer meshRenderer = gameObject.AddComponent<MeshRenderer>();
		meshRenderer.material = material;
	}

	private void WriteMaillage()
	{
		StreamWriter writer = new(Path.Combine("Assets/Maillage", "new" + fileName + ".obj"));
		for (int i = 0; i < vertices.Length; i++)
		{
			writer.WriteLine("v " + vertices[i].x.ToString(CultureInfo.InvariantCulture) + " " + vertices[i].y.ToString(CultureInfo.InvariantCulture) + " " + vertices[i].z.ToString(CultureInfo.InvariantCulture));
		}
		writer.WriteLine();
		for (int i = 0; i < normals.Length; i++)
		{
			writer.WriteLine("vn " + normals[i].x.ToString(CultureInfo.InvariantCulture) + " " + normals[i].y.ToString(CultureInfo.InvariantCulture) + " " + normals[i].z.ToString(CultureInfo.InvariantCulture));
		}
		writer.WriteLine();
		for (int i = 0; i < triangles.Count; i += 3)
		{
			string t1 = (triangles[i] + 1).ToString(CultureInfo.InvariantCulture);
			string t2 = (triangles[i + 1] + 1).ToString(CultureInfo.InvariantCulture);
			string t3 = (triangles[i + 2] + 1).ToString(CultureInfo.InvariantCulture);
			writer.WriteLine("f " + t1 + "//" + t1 + " " + t2 + "//" + t2 + " " + t3 + "//" + t3);
		}
		writer.Close();
		Debug.Log("Maillage written");
	}

}