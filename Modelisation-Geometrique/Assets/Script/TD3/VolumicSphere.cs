using System;
using System.Collections.Generic;
using UnityEngine;

public class VolumicSphere : MonoBehaviour
{
    [SerializeField]
    private GameObject cube;

    [Header("Octree information")]
    [SerializeField]
    private bool adaptiveOctree;

    [SerializeField]
    private int precision;

    [Header("Sphere information")]
    [SerializeField]
    private List<Sphere> spheres = new();

    [Header("Operator")]
    [SerializeField]
    private bool doIntersection;

    private void Start()
    {
        List<Octree> octrees = new();

        for (int i = 0; i < spheres.Count; i++)
        {
            Octree octree = new Octree();
            octrees.Add(octree);
            if (adaptiveOctree)
            {
                octree.CreateAdaptiveOctree(spheres[i].Position, spheres[i].Radius * 2, precision, spheres[i].Position,
                    spheres[i].Radius);
            }
            else
            {
                octree.CreateRegularOctree(spheres[i].Position, spheres[i].Radius * 2, precision);
            }

            if (!doIntersection)
            {
                FiledOctree(spheres[i].Position, spheres[i].Radius, octree);
            }
        }

        if (doIntersection)
        {
            FiledOctreeIntersection(octrees[0]);
        }
    }

    private void FiledOctreeIntersection(Octree octree)
    {
        if (octree.isOctreeParent)
        {
            foreach (Octree o in octree.Octrees)
            {
                FiledOctreeIntersection(o);
            }
        }
        else if (octree.isVoxelParent)
        {
            foreach (Voxel v in octree.Voxels)
            {
                bool add = true;
                for (int i = 0; i < spheres.Count; i++)
                {
                    if (Vector3.Distance(spheres[i].Position, v.Center) > spheres[i].Radius)
                    {
                        add = false;
                    }
                }

                if (add)
                {
                    cube.transform.localScale = Vector3.one * (v.PointMax - v.PointMin).x;
                    Instantiate(cube, v.Center, Quaternion.identity, transform);
                }
            }
        }
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
                    Instantiate(cube, v.Center, Quaternion.identity, transform);
                }
            }
        }
    }
}