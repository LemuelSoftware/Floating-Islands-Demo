using Godot;
using System;

public partial class SignBase : StaticBody3D
{
	[Export] ShaderMaterial material;

	MeshInstance3D coneMesh;

	public override void _Ready()
	{
		base._Ready();

		coneMesh = GetNode<MeshInstance3D>("%ConeMesh");

		if (material != null)
		{
			coneMesh.MaterialOverride = material;
		}
	}

	public void Enable(bool flag)
	{
		coneMesh.Visible = flag;
	}

}
