using Godot;
using System;

public partial class Engine : StaticBody3D
{
	[Export] MeshInstance3D mesh;
	public override void _Ready()
	{
		base._Ready();

		Global.Instance.GouraudShadingEnabled += OnGouraudShadingEnabled;
	}

	public void OnGouraudShadingEnabled(bool enabled)
	{
		if (mesh == null)
			return;

		StandardMaterial3D mat = (StandardMaterial3D)mesh.GetSurfaceOverrideMaterial(0);

		if (mat == null)
			return;

		if (enabled)
			mat.ShadingMode = StandardMaterial3D.ShadingModeEnum.PerVertex;
		else
			mat.ShadingMode = StandardMaterial3D.ShadingModeEnum.PerPixel;

		mesh.SetSurfaceOverrideMaterial(0, mat);


		ShaderMaterial m = (ShaderMaterial)mesh.MaterialOverlay;
	}

	public override void _ExitTree()
	{
		base._ExitTree();

		Global.Instance.GouraudShadingEnabled -= OnGouraudShadingEnabled;
	}
}
