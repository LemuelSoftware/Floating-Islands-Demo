using Godot;
using System;

public partial class Drone : StaticBody3D
{
	[Export] bool isRotating = true;
	[Export] float rotateSpeed = 0.8f;

	public override void _Process(double delta)
	{
		base._Process(delta);

		if (isRotating)
		{
			RotateY(rotateSpeed * (float)delta);
		}
	}

	public void setRotating(bool flag)
	{
		isRotating = flag;
	}
}
