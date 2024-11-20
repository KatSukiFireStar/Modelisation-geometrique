using System;
using System.Collections.Generic;
using UnityEngine;

public class BezierCurve : MonoBehaviour
{
	[SerializeField] 
	private Color baseColor;
	[SerializeField] 
	private Color curveColor;
	[SerializeField] 
	private float delta = 0.1f;
	
	[SerializeField] 
	private List<Vector3> startPoints = new();

	private List<Vector3> curvePoints;
	
	private int degree => startPoints.Count;
	
	private void OnDrawGizmos()
	{
		Gizmos.color = baseColor;
		for (int i = 0; i < startPoints.Count - 1; i++)
		{
			Gizmos.DrawLine(startPoints[i] + transform.position, startPoints[i + 1] + transform.position);
		}
	}

	private void OnDrawGizmosSelected()
	{
		curvePoints = new();
		Gizmos.color = curveColor;
		float current = delta;
		curvePoints.Add(startPoints[0]);
		while (current < 1)
		{
			Vector3 p = new();
			for (int i = 0; i < startPoints.Count; i++)
			{
				p += startPoints[i] * Bernstein(i, degree - 1, current);
			}
			curvePoints.Add(p);
			current += delta;
		}
		curvePoints.Add(startPoints[startPoints.Count - 1]);
		for (int i = 0; i < curvePoints.Count - 1; i++)
		{
			Gizmos.DrawLine(curvePoints[i] + transform.position, curvePoints[i + 1] + transform.position);
		}
	}

	private float Factoriel(int n)
	{
		return n > 1?n * Factoriel(n-1):1;
	}

	private float Bernstein(int i, int n, float t)
	{
		return Factoriel(n) / (Factoriel(i) * Factoriel(n-i)) * Mathf.Pow(t, i) * Mathf.Pow(1-t, n-i);
	}
}