using UnityEngine;

public class Voxel
{
	public Vector3 Center => new((PointMin.x + PointMax.x) / 2, (PointMin.y + PointMax.y) / 2, (PointMin.z + PointMax.z) / 2);
	
	public Vector3 PointMin { get; set; }
	public Vector3 PointMax { get; set; }
	
	public int Potentiel { get; set; }

	public Voxel(Vector3 min, Vector3 max)
	{
		PointMin = min;
		PointMax = max;
		Potentiel = 255;
	}

	public void addPotentiel(int potentiel)
	{
		if (Potentiel + potentiel <= 255)
		{
			Potentiel += potentiel;
		}
	}

	public void removePotentiel(int potentiel)
	{
		if (Potentiel - potentiel >= 0)
		{
			Potentiel -= potentiel;
		}
	}
}