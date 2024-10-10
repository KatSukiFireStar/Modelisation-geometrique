using System;
using System.Collections.Generic;
using UnityEngine;

public class VolumicSphere : MonoBehaviour
{
    [SerializeField] private GameObject cube;

    [SerializeField] private bool adaptiveOctree;
    
    [SerializeField] private List<int> radius = new();

    [SerializeField] private List<Vector3> centers = new();

    [SerializeField] private int precision;
    
    private void Start()
    {
        if (radius.Count != centers.Count)
        {
            Debug.LogError("Radius and Centers must have the same number of elements.");
        }
        
        for (int i = 0; i < centers.Count; i++)
        {
            Octree octree = new Octree();
            if (adaptiveOctree)
            {
                octree.CreateAdaptiveOctree(centers[i], radius[i] * 2, precision, centers[i], radius[i]);
            }
            else
            {
                octree.CreateRegularOctree(centers[i], radius[i] * 2, precision);
            }
            
            FiledOctree(centers[i], radius[i], octree);
        }
    }


    public void FiledOctree(Vector3 center, int radius, Octree octree)
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