using System;
using System.Collections.Generic;
using UnityEngine;

public class PotentialTools : MonoBehaviour
{
	private GameObject sphere;
	[SerializeField] 
	private GameObject camera;
	[SerializeField] 
	private Material mat;
	[SerializeField] 
	private float speed;
	[SerializeField]
	private GameObject cube;
	[SerializeField]
	private int precision;
	[SerializeField] 
	private int potentiel;
	[SerializeField] 
	private int seuil;
	[Header("Sphere information")]
	[SerializeField]
	private Sphere sphereInfo;
	
	private Dictionary<GameObject, Voxel> voxels = new();
	
	private void Start()
	{
		sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
		sphere.GetComponent<MeshRenderer>().material = mat;
		sphere.transform.parent = transform;

		Octree octree = new();
		octree.CreateRegularOctree(sphereInfo.Position, sphereInfo.Radius * 2, precision);
		FiledOctree(sphereInfo.Position, sphereInfo.Radius, octree);
	}
	
	private void FiledOctree(Vector3 center, float radius, Octree octree)
	{
		if (octree.isOctreeParent)
		{
			foreach (Octree o in octree.Octrees)
			{
				FiledOctree(center, radius, o);
			}
		}
		else if (octree.isVoxelParent)
		{
			foreach (Voxel v in octree.Voxels)
			{
				if (Vector3.Distance(center, v.Center) < radius)
				{
					cube.transform.localScale = Vector3.one * (v.PointMax - v.PointMin).x;
					GameObject c = Instantiate(cube, v.Center, Quaternion.identity, transform);
					voxels[c] = v;
				}
			}
		}
	}

	private void Update()
	{
		if (Input.GetKey(KeyCode.D))
		{
			sphere.transform.Translate( Time.deltaTime * speed * Vector3.right);
		}else if (Input.GetKey(KeyCode.A))
		{
			sphere.transform.Translate(Time.deltaTime * speed * Vector3.left);
		}

		if (Input.GetKey(KeyCode.S))
		{
			sphere.transform.Translate(Time.deltaTime * speed * Vector3.back);
		}else if (Input.GetKey(KeyCode.W))
		{
			sphere.transform.Translate(Time.deltaTime * speed * Vector3.forward);
		}

		if (Input.GetKey(KeyCode.Q))
		{
			sphere.transform.Translate(Time.deltaTime * speed * Vector3.down);
		}else if (Input.GetKey(KeyCode.E))
		{
			sphere.transform.Translate(Time.deltaTime * speed * Vector3.up);
		}

		if (Input.GetMouseButton(0))
		{
			foreach (GameObject o in voxels.Keys)
			{
				if (Vector3.Distance(voxels[o].Center, sphere.transform.position) < sphere.transform.localScale.x / 2)
				{
					voxels[o].addPotentiel(potentiel);
					if (voxels[o].Potentiel >= seuil)
					{
						o.SetActive(true);
					}
				}
			}
		}else if (Input.GetMouseButtonDown(1))
		{
			foreach (GameObject o in voxels.Keys)
			{
				if (Vector3.Distance(voxels[o].Center, sphere.transform.position) < sphere.transform.localScale.x / 2)
				{
					voxels[o].removePotentiel(potentiel);
					if (voxels[o].Potentiel < seuil)
					{
						o.SetActive(false);
					}
				}
			}
		}
	}
}