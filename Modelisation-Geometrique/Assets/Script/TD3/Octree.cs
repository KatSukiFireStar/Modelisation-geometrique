using System.Collections.Generic;
using UnityEngine;

public class Octree
{
	public List<Octree> Octrees {get; set;}
	public List<Voxel> Voxels {get; set;}
	
	public bool isOctreeParent => Octrees.Count >= 0 && Voxels.Count == 0;
	public bool isVoxelParent => Octrees.Count == 0 && Voxels.Count >= 0;

	public Octree()
	{
		Octrees = new List<Octree>();
		Voxels = new List<Voxel>();
	}

	public void CreateRegularOctree(Vector3 center, float size, int precision)
	{
		if (precision == 1)
		{
			bool add = AddVoxel(new(new(center.x - size / 2, center.y - size / 2, center.z - size / 2), new(center.x + size / 2, center.y + size / 2, center.z + size / 2)));
			if (!add)
			{
				Debug.LogError("This octree can't add voxels");
			}
			return;
		}
		
		List<Vector3> vertices = new List<Vector3>();
		vertices.Add(new Vector3(center.x + size / 2, center.y + size / 2, center.z + size / 2));
		vertices.Add(new Vector3(center.x + size / 2, center.y + size / 2, center.z - size / 2));
		vertices.Add(new Vector3(center.x + size / 2, center.y - size / 2, center.z + size / 2));
		vertices.Add(new Vector3(center.x + size / 2, center.y - size / 2, center.z - size / 2));
		vertices.Add(new Vector3(center.x - size / 2, center.y + size / 2, center.z + size / 2));
		vertices.Add(new Vector3(center.x - size / 2, center.y + size / 2, center.z - size / 2));
		vertices.Add(new Vector3(center.x - size / 2, center.y - size / 2, center.z + size / 2));
		vertices.Add(new Vector3(center.x - size / 2, center.y - size / 2, center.z - size / 2));
		if (precision == 2)
		{
			foreach (Vector3 vertex in vertices)
			{
				Vector3 cen = new((vertex.x + center.x)/2, (vertex.y + center.y)/2, (vertex.z + center.z)/2);
				bool add = AddVoxel(new(new(cen.x - size / 4, cen.y - size / 4, cen.z - size / 4), new(cen.x + size / 4, cen.y + size / 4, cen.z + size / 4)));
				if (!add)
				{
					Debug.LogError("This octree can't add voxels");
					return;
				}
			}
		}
		else
		{
			foreach (Vector3 vertex in vertices)
			{
				Octree newOctree = new();
				bool add = AddOctree(newOctree);
				if (!add)
				{
					Debug.LogError("This octree can't add octree");
					return;
				}
				newOctree.CreateRegularOctree(new((vertex.x + center.x) / 2, (vertex.y + center.y) / 2, (vertex.z + center.z) / 2), size / 2, precision - 1);
			}
		}
	}

	public void CreateAdaptiveOctree(Vector3 center, float size, int precision)
	{
		
	}

	public bool AddOctree(Octree octree)
	{
		if (Voxels.Count == 0)
		{	
			Octrees.Add(octree);
			return true;
		}

		return false;
	}

	public bool AddVoxel(Voxel voxel)
	{
		if (Octrees.Count == 0)
		{
			Voxels.Add(voxel);
			return true;
		}

		return false;
	}
	
}