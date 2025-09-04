using Godot;
using System;

public partial class Propeller : Engine
{
	[Export] float speed = 8.0f;

	public override void _Process(double delta)
	{
		base._Process(delta);

		RotateY(-speed * (float)delta);
	}
}
