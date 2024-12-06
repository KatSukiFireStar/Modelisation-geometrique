using System.Collections.Generic;
using UnityEngine;

public class BezierCurveCasteljau : MonoBehaviour
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
		
		curvePoints = new();
		Gizmos.color = curveColor;
		float current = delta;
		curvePoints.Add(startPoints[0]);
		
		while (current < 1)
		{
			List<Vector3> operationPoints = new();
			operationPoints.AddRange(startPoints);
			Vector3 p = new();
			while (operationPoints.Count > 1)
			{
				List<Vector3> newPoints = new();
				for (int i = 0; i < operationPoints.Count - 1; i++)
				{
					p = (1 - current) * operationPoints[i] + current * operationPoints[i+1];
					newPoints.Add(p);
				}
				operationPoints = new();
				operationPoints.AddRange(newPoints);
			}
			curvePoints.Add(operationPoints[0]);
			current += delta;
		}
		
		
		curvePoints.Add(startPoints[startPoints.Count - 1]);
		for (int i = 0; i < curvePoints.Count - 1; i++)
		{
			Gizmos.DrawLine(curvePoints[i] + transform.position, curvePoints[i + 1] + transform.position);
		}
	}
}