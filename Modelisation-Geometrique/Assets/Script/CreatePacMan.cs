using UnityEngine;
using System.Collections.Generic;

public class CreatePacMan : MonoBehaviour
{
    [SerializeField] private int radius, nbMeridiens, nbParalleles, radiusTronquage;

    [SerializeField] private Material material;

    private List<Vector3> points = new();

    private List<int> triangles = new();

    void Start()
    {
        if (nbParalleles == 0 || nbMeridiens == 0)
        {
            Debug.LogError("Nb paralleles ou Nb meridiens est vide.");
            return;
        }

        float theta = 360 / nbMeridiens;
        float phi = 180 / nbParalleles;

        int indNorthPole = points.Count;
        points.Add(new(0, radius, 0));
        int indSouthPole = points.Count;
        points.Add(new(0, -radius, 0));

        for (float i = -90 + phi; i < 90 - phi; i += phi)
        {
            for (float j = radiusTronquage / 2; j < 360; j += theta)
            {
                if ((360 - radiusTronquage / 2) > j)
                {
                    int t1 = points.Count;
                    points.Add(new Vector3(Mathf.Cos((j * Mathf.PI) / 180) * Mathf.Cos((i) * Mathf.PI / 180) * radius,
                        Mathf.Sin(i * Mathf.PI / 180) * radius,
                        Mathf.Sin((j * Mathf.PI) / 180) * Mathf.Cos((i) * Mathf.PI / 180) * radius));
                    int t2 = points.Count;
                    points.Add(new Vector3(Mathf.Cos((j * Mathf.PI) / 180) * Mathf.Cos((i + phi) * Mathf.PI / 180) * radius,
                        Mathf.Sin((i + phi) * Mathf.PI / 180) * radius,
                        Mathf.Sin((j * Mathf.PI) / 180) * Mathf.Cos((i + phi) * Mathf.PI / 180) * radius));
                    int t3 = points.Count;
                    points.Add(new Vector3(
                        Mathf.Cos(((j + theta) * Mathf.PI) / 180) * Mathf.Cos((i) * Mathf.PI / 180) * radius,
                        Mathf.Sin(i * Mathf.PI / 180) * radius,
                        Mathf.Sin(((j + theta) * Mathf.PI) / 180) * Mathf.Cos((i) * Mathf.PI / 180) * radius));
                    int t4 = points.Count;
                    points.Add(new Vector3(
                        Mathf.Cos(((j + theta) * Mathf.PI) / 180) * Mathf.Cos((i + phi) * Mathf.PI / 180) * radius,
                        Mathf.Sin((i + phi) * Mathf.PI / 180) * radius,
                        Mathf.Sin(((j + theta) * Mathf.PI) / 180) * Mathf.Cos((i + phi) * Mathf.PI / 180) * radius));

                    if (i != 90 - phi)
                    {
                        triangles.AddRange(new int[] { t1, t2, t3 });
                        triangles.AddRange(new int[] { t4, t3, t2 });
                    }

                    if (i == (-90 + phi))
                    {
                        triangles.AddRange(new int[] { t1, t3, indSouthPole });
                    }

                    if (i >= 90 - (2 * phi))
                    {
                        triangles.AddRange(new int[] { t2, indNorthPole, t4 });
                    }
                }
            }
        }

        float startAngle = (360 - radiusTronquage / 2);
        float endAngle = radiusTronquage / 2;

        for (float i = -90 + phi; i < 90 - phi; i += phi)
        {
            int t1 = points.Count;
            points.Add(new Vector3(0, Mathf.Sin(i * Mathf.PI / 180) * radius, 0));
            int t2 = points.Count;
            points.Add(new Vector3(0, Mathf.Sin((i + phi) * Mathf.PI / 180) * radius, 0));
            int t3 = points.Count;
            points.Add(new Vector3(
                Mathf.Cos(((startAngle) * Mathf.PI) / 180) * Mathf.Cos((i) * Mathf.PI / 180) * radius,
                Mathf.Sin(i * Mathf.PI / 180) * radius,
                Mathf.Sin(((startAngle) * Mathf.PI) / 180) * Mathf.Cos((i) * Mathf.PI / 180) * radius));
            int t4 = points.Count;
            points.Add(new Vector3(
                Mathf.Cos(((startAngle) * Mathf.PI) / 180) * Mathf.Cos((i + phi) * Mathf.PI / 180) * radius,
                Mathf.Sin((i + phi) * Mathf.PI / 180) * radius,
                Mathf.Sin(((startAngle) * Mathf.PI) / 180) * Mathf.Cos((i + phi) * Mathf.PI / 180) * radius));
            
            int t5 = points.Count;
            points.Add(new Vector3(
                Mathf.Cos(((endAngle) * Mathf.PI) / 180) * Mathf.Cos((i) * Mathf.PI / 180) * radius,
                Mathf.Sin(i * Mathf.PI / 180) * radius,
                Mathf.Sin(((endAngle) * Mathf.PI) / 180) * Mathf.Cos((i) * Mathf.PI / 180) * radius));
            int t6 = points.Count;
            points.Add(new Vector3(
                Mathf.Cos(((endAngle) * Mathf.PI) / 180) * Mathf.Cos((i + phi) * Mathf.PI / 180) * radius,
                Mathf.Sin((i + phi) * Mathf.PI / 180) * radius,
                Mathf.Sin(((endAngle) * Mathf.PI) / 180) * Mathf.Cos((i + phi) * Mathf.PI / 180) * radius));
            
            triangles.AddRange(new int[] { t1, t3, t2 });
            triangles.AddRange(new int[] { t4, t2, t3 });
            
            triangles.AddRange(new int[] { t1, t2, t5 });
            triangles.AddRange(new int[] { t6, t5, t2 });
        }

        MeshFilter filter = gameObject.AddComponent<MeshFilter>();
        Mesh mesh = filter.mesh;
        mesh.vertices = points.ToArray();
        mesh.triangles = triangles.ToArray();

        MeshRenderer meshRenderer = gameObject.AddComponent<MeshRenderer>();
        meshRenderer.material = material;
    }
}