using Godot;
using System;

public partial class Roof3 : BuildingPart
{
	[Export] float rotateSpeed = 2.0f;

	[Export] private Node3D generator;

	public override void _Process(double delta)
	{
		base._Process(delta);

		if (generator != null)
			generator.RotateY(rotateSpeed * (float)delta);
	}
}
