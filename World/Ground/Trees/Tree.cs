using Godot;
using System;

public partial class Tree : StaticBody3D
{
	[Export] MeshInstance3D[] meshList;

	public override void _Ready()
	{
		base._Ready();

		Global.Instance.GouraudShadingEnabled += OnGouraudShadingEnabled;
	}

	public void OnGouraudShadingEnabled(bool enabled)
	{
		if (meshList == null)
			return;

		foreach (MeshInstance3D m in meshList)
		{
			if (m == null)
				return;

			StandardMaterial3D mat = (StandardMaterial3D)m.GetSurfaceOverrideMaterial(0);

			if (mat == null)
				return;

			if (enabled)
				mat.ShadingMode = StandardMaterial3D.ShadingModeEnum.PerVertex;
			else
				mat.ShadingMode = StandardMaterial3D.ShadingModeEnum.PerPixel;

			m.SetSurfaceOverrideMaterial(0, mat);
		}
	}

	public override void _ExitTree()
	{
		base._ExitTree();

		Global.Instance.GouraudShadingEnabled -= OnGouraudShadingEnabled;
	}
}
