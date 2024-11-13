using System;
using System.Collections.Generic;
using UnityEngine;

public class Subdivision : MonoBehaviour
{
	[SerializeField] 
	private List<Vector3> startPoints;
	
	[SerializeField] 
	private Color baseColor;
	
	[SerializeField] 
	[Range(0, 10)]
	private int precision;
	
	[SerializeField] 
	private Color curveColor;

	private List<Vector3> curvePoints = new();

	private void OnDrawGizmos()
	{
		Gizmos.color = baseColor;
		for (int i = 0; i < startPoints.Count; i++)
		{
			if (i + 1 < startPoints.Count)
			{
				Gizmos.DrawLine(startPoints[i], startPoints[i + 1]);
			}
			else
			{
				Gizmos.DrawLine(startPoints[i], startPoints[0]);
			}
		}
	}

	private void OnDrawGizmosSelected()
	{
		Gizmos.color = curveColor;
		curvePoints = new();
		curvePoints.AddRange(startPoints);
		for (int p = 0; p < precision; p++)
		{
			List<Vector3> points = new();
			for (int i = 0; i < curvePoints.Count; i++)
			{
				if (i + 1 < curvePoints.Count)
				{
					points.Add(0.75f * curvePoints[i] + 0.25f * curvePoints[i + 1]);
					points.Add(0.25f * curvePoints[i] + 0.75f * curvePoints[i + 1]);
				}
				else
				{
					points.Add(0.75f * curvePoints[i] + 0.25f * curvePoints[0]);
					points.Add(0.25f * curvePoints[i] + 0.75f * curvePoints[0]);
				}
			}

			curvePoints = new();
			curvePoints.AddRange(points);
		}
		
		for (int i = 0; i < curvePoints.Count; i++)
		{
			if (i + 1 < curvePoints.Count)
			{
				Gizmos.DrawLine(curvePoints[i], curvePoints[i + 1]);
			}
			else
			{
				Gizmos.DrawLine(curvePoints[i], curvePoints[0]);
			}
		}
	}
}