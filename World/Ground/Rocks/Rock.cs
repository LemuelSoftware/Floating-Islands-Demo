using Godot;
using System;

public partial class Rock : StaticBody3D
{
	[Export] MeshInstance3D mesh;

	public override void _Ready()
	{
		base._Ready();

		Global.Instance.FlatShadingEnabled += OnFlatShadingEnabled;
	}

	public void OnFlatShadingEnabled(bool enabled)
	{
		if (mesh == null)
			return;
		
		ShaderMaterial mat = (ShaderMaterial)mesh.GetSurfaceOverrideMaterial(0);

		if (mat == null)
			return;

		mat.SetShaderParameter("enable", enabled);
		mesh.SetSurfaceOverrideMaterial(0, mat);
	}

	public override void _ExitTree()
	{
		base._ExitTree();

		Global.Instance.FlatShadingEnabled -= OnFlatShadingEnabled;
	}
}
