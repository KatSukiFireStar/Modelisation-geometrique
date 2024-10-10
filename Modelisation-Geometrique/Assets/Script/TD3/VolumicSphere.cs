using System;
using UnityEngine;

public class VolumicSphere : MonoBehaviour
{
	[SerializeField] 
	private GameObject cube;
	
	[SerializeField] 
	private int radius;

	[SerializeField] 
	private Vector3 center;
	
	[SerializeField]
	private int precision;

	private void Start()
	{
		Octree octree = new Octree();
		octree.CreateRegularOctree(center, radius * 2, precision);
		FiledOctree(octree);
	}
	
	
	public void FiledOctree(Octree octree)
	{
		if (octree.isOctreeParent)
		{
			foreach (Octree o in octree.Octrees)
			{
				FiledOctree(o);
			}
		}else if (octree.isVoxelParent)
		{
			foreach (Voxel v in octree.Voxels)
			{
				if (Vector3.Distance(center, v.Center) < radius)
				{
					cube.transform.localScale = Vector3.one * (v.PointMax - v.PointMin).x;
					Instantiate(cube, v.Center, Quaternion.identity, transform);
				}
			}
		}
	}
}