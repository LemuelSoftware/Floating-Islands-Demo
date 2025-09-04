using Godot;
using System;

public partial class Sign : Node3D
{
	[Export] float signRotationSpeed = 1.0f;

	[Export] ShaderMaterial material;
	[Export] Node3D sign;
	[Export] MeshInstance3D signMesh;
	[Export] SignBase signBase;

	public override void _Ready()
	{
		base._Ready();

		Global.Instance.HologramsEnabled += Enable;

		if (signMesh != null && material != null)
		{
			signMesh.MaterialOverride = material;
		}
	}

	public override void _ExitTree()
	{
		base._ExitTree();

		Global.Instance.HologramsEnabled -= Enable;
	}

	public override void _Process(double delta)
	{
		base._Process(delta);

		RotateY(signRotationSpeed * (float)delta);
	}

	public void Enable(bool flag)
	{
		sign.Visible = flag;

		if (signBase != null)
			signBase.Enable(flag);
	}
}
