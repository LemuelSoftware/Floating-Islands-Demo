using Godot;
using System;

public partial class SignBase : StaticBody3D
{
	[Export] ShaderMaterial material;
	[Export] MeshInstance3D[] meshList;

	MeshInstance3D coneMesh;

	public override void _Ready()
	{
		base._Ready();

		Global.Instance.FlatShadingEnabled += OnFlatShadingEnabled;

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
