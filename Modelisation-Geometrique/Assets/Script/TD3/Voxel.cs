using UnityEngine;

public class Voxel
{
	public Vector3 Center => new((PointMin.x + PointMax.x) / 2, (PointMin.y + PointMax.y) / 2, (PointMin.z + PointMax.z) / 2);
	
	public Vector3 PointMin { get; set; }
	public Vector3 PointMax { get; set; }

	public Voxel(Vector3 min, Vector3 max)
	{
		PointMin = min;
		PointMax = max;
	}
}