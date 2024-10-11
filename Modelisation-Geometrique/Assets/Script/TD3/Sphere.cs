using System;
using UnityEngine;

[Serializable]
public class Sphere
{
	[SerializeField]
	private Vector3 position;
	public Vector3 Position { get => position; set => position = value; }

	[SerializeField]
	private float radius;
	public float Radius { get => radius; set => radius = value; }
}