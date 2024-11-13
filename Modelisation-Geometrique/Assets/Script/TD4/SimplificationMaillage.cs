using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;
using UnityEngine;

public class SimplificationMaillage : MonoBehaviour
{
	[SerializeField] 
	private int precision;
	
	[SerializeField]
	private Material material;
	
	[SerializeField] 
	private string fileName;
	
	private Vector3[] verticesStart;
	private List<int> trianglesStart = new();
	private Vector3[] normals;
	private Vector3 pointCenter = new();
	private Octree octree = new();
	private Vector3 pos;

	private void Start()
	{
		pos = transform.position;
		transform.position = Vector3.zero;
		LoadMaillage();
		NormalizeMaillage();
		float distMax = float.MinValue;
		foreach (Vector3 v in verticesStart)
		{
			foreach (Vector3 v2 in verticesStart)
			{
				if (Vector3.Distance(v, v2) > distMax)
				{
					distMax = Vector3.Distance(v, v2);
				}
			}
		}
		octree.CreateRegularOctree(transform.position, distMax, precision);
		Vector3[] vertices;
		List<int> triangles;

		(vertices, triangles) = CreateNewMaillage();
		TraceMaillagePrecision(vertices, triangles);
		transform.position = pos;
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
				trianglesStart.Add(int.Parse(values[j]));
			}
		}
		verticesStart = points.ToArray();
	}

	private (Vector3[], List<int>) CreateNewMaillage()
	{
		List<Vector3> vertices = new();
		List<int> triangles = new();
		
		triangles.AddRange(trianglesStart);
		
		List<Voxel> voxels = new();
		voxels = octree.GetAllVoxels();

		Dictionary<int, int> newIndice = new();

		for (int i = 0; i < verticesStart.Length; i++)
		{
			newIndice[i] = -1;
		}
		
		foreach (Voxel voxel in voxels)
		{
			List<(Vector3, int)> pointsInBox = new();
			for (int i = 0; i  < verticesStart.Length; i++)
			{
				Vector3 v = verticesStart[i];
				if (PointInBox(v, voxel.PointMin, voxel.PointMax))
				{
					pointsInBox.Add((v, i));
				}
			}
			
			if(pointsInBox.Count == 0)
				continue;

			Vector3 center = new();
			foreach ((Vector3, int) v in pointsInBox)
			{
				center += v.Item1;
			}
			center /= pointsInBox.Count;
			
			int ind = vertices.Count;
			vertices.Add(center);

			for (int i = 0; i < verticesStart.Length; i++)
			{
				foreach ((Vector3, int) v in pointsInBox)
				{
					if (i == v.Item2)
					{
						newIndice[i] = ind;
					}
				}
			}
		}

		for (int i = 0; i < triangles.Count; i++)
		{
			foreach (int j in newIndice.Keys)
			{
				if (triangles[i] == j)
				{
					triangles[i] = newIndice[j];
					break;
				}
			}
		}

		List<int> trianglesToReturn = new();
		for (int i = 0; i < triangles.Count; i+=3)
		{
			if (triangles[i] == triangles[i + 1] || triangles[i] == triangles[i + 2] || triangles[i + 1] == triangles[i + 2])
			{
				continue;
			}

			int count = 0;
			for (int j = 0; j < trianglesToReturn.Count; j += 3)
			{
				if ((triangles[i] == trianglesToReturn[j] || triangles[i] == trianglesToReturn[j + 1] ||
				     triangles[i] == trianglesToReturn[j + 2]) &&
				    (triangles[i + 1] == trianglesToReturn[j] || triangles[i + 1] == trianglesToReturn[j + 1] ||
				     triangles[i + 1] == trianglesToReturn[j + 2]) &&
				    (triangles[i + 2] == trianglesToReturn[j] || triangles[i + 2] == trianglesToReturn[j + 1] ||
				     triangles[i + 2] == trianglesToReturn[j + 2]))
				{
					count++;
				}
			}

			if (count == 0)
			{
				trianglesToReturn.Add(triangles[i]);
				trianglesToReturn.Add(triangles[i + 1]);
				trianglesToReturn.Add(triangles[i + 2]);
			}
			
		}
		
		return (vertices.ToArray(), trianglesToReturn);
	}

	private bool PointInBox(Vector3 point, Vector3 pointMinBox, Vector3 pointMaxBox)
	{
		if(point.x <= pointMinBox.x || point.x > pointMaxBox.x)
			return false;
		
		if(point.y <= pointMinBox.y || point.y > pointMaxBox.y)
			return false;
		
		if(point.z <= pointMinBox.z || point.z > pointMaxBox.z)
			return false;
		
		return true;
	}

	private void NormalizeMaillage()
	{
		for (int i = 0; i < verticesStart.Length; i++)
		{
			pointCenter += verticesStart[i];
		}
		pointCenter /= verticesStart.Length;
		
		for (int i = 0; i < verticesStart.Length; i++)
		{
			verticesStart[i] -= pointCenter;
		}
		pointCenter -= pointCenter;
		
		Vector3 max = new(0,0,0);
		float maxF = float.MinValue;
		for (int i = 0; i < verticesStart.Length; i++)
		{
			if (maxF < verticesStart[i].magnitude)
			{
				max = verticesStart[i];
				maxF = verticesStart[i].magnitude;
			}
		}
		for (int i = 0; i < verticesStart.Length; i++)
		{
			verticesStart[i] /= max.magnitude;
		}
	}

	private void TraceMaillagePrecision(Vector3[] vertices, List<int> triangles)
	{
		MeshFilter filter = gameObject.AddComponent<MeshFilter>();
		Mesh mesh = filter.mesh;
		
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
}