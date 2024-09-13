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

        for (int y = 0; y < nbLignes; y++)
        {
            for (int x = 0; x < nbColonnes; x++)
            {
                int t1 = points.Count;
                points.Add(new(x, y, 0));
                int t2 = points.Count;
                points.Add(new(x, y+1, 0));
                int t3 = points.Count;
                points.Add(new(x+1, y, 0));
                int t4 = points.Count;
                points.Add(new(x+1, y+1, 0));
                triangles.AddRange(new int[] { t1, t2, t3 });
                triangles.AddRange(new int[] { t4, t3, t2 });
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
