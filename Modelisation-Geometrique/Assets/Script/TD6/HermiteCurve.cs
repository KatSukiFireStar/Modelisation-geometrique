using System;
using System.Collections.Generic;
using UnityEngine;

public class HermiteCurve : MonoBehaviour
{
	[SerializeField] 
	private Color curveColor;
	
	[SerializeField] 
	private float delta = 0.1f;
	
	[Header("Points")]
	[SerializeField] 
	private Vector3 p0;
	
	[SerializeField] 
	private Vector3 p1;
	
	[Header("Tangents")]
	[SerializeField] 
	private Vector3 v0;
	
	[SerializeField] 
	private Vector3 v1;
	
	private float F1(float u) => 2 * (u * u * u) - 3 * u * u + 1;
	private float F2(float u) => -2 * u * u * u + 3 * u * u;
	private float F3(float u) => u * u * u - 2 * u * u + u;
	private float F4(float u) => u * u * u - u * u;
	
	private List<Vector3> curvePoints = new();

	private void OnDrawGizmos()
	{
		curvePoints = new();
		Gizmos.color = curveColor;
		float current = delta;
		curvePoints.Add(p0);
		while (current < 1)
		{
			Vector3 p = F1(current) * p0 + F2(current) * p1 + F3(current) * v0 + F4(current) * v1;
			curvePoints.Add(p);
			current += delta;
		}
		curvePoints.Add(p1);
		for (int i = 0; i < curvePoints.Count - 1; i++)
		{
			Gizmos.DrawLine(curvePoints[i], curvePoints[i + 1]);
		}
	}
}