using Godot;
using System;

public partial class Drone : StaticBody3D
{
	[Export] bool isRotating = true;
	[Export] float rotateSpeed = 0.8f;
	[Export] MeshInstance3D[] meshList;

	public override void _Ready()
	{
		base._Ready();

		Global.Instance.FlatShadingEnabled += OnFlatShadingEnabled;
	}

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

	public void OnFlatShadingEnabled(bool enabled)
	{
		if (meshList == null)
			return;

		foreach (MeshInstance3D m in meshList)
		{
			if (m == null)
				return;

			ShaderMaterial mat = (ShaderMaterial)m.GetSurfaceOverrideMaterial(0);

			if (mat == null)
				return;

			mat.SetShaderParameter("enable", enabled);
			m.SetSurfaceOverrideMaterial(0, mat);
		}
	}

	public override void _ExitTree()
	{
		base._ExitTree();

		Global.Instance.FlatShadingEnabled -= OnFlatShadingEnabled;
	}
}
