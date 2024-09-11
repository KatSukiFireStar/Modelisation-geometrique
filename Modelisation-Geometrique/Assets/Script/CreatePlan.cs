using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreatePlan : MonoBehaviour
{
    [SerializeField] 
    private int nbLignes, nbColonnes;
    
    [SerializeField]
    private Material material;
    
    private List<Vector3> points = new();
    
    private List<int> triangles = new();

    void Start()
    {
        for (int i = 0; i < nbColonnes; i++)
        {
            for (int j = 0; j < nbLignes; j++)
            {
                points.Add(new(i, j, 0));
                points.Add(new(i+1, j, 0));
                points.Add(new(i, j+1, 0));
                points.Add(new(i+1,j+1,0));
                
                triangles.AddRange(new int[]{0,1,2});
                triangles.AddRange(new int[]{3,2,1});
            }
        }
        
        MeshFilter filter = gameObject.AddComponent<MeshFilter>();
        Mesh mesh = filter.mesh;
        mesh.vertices = points.ToArray();
        mesh.triangles = triangles.ToArray();
        
        MeshRenderer meshRenderer = gameObject.AddComponent<MeshRenderer>();
        meshRenderer.material = material;
    }
}
