using Godot;
using System;

public abstract partial class Light : StaticBody3D
{
	[Export] protected ShaderMaterial material;
	[Export] protected MeshInstance3D mesh;
	[Export] private MeshInstance3D[] shadingMeshList;

	protected float glow_power;

	public override void _Ready()
	{
		base._Ready();

		if (material != null && mesh != null)
		{
			mesh.MaterialOverride = material;
			glow_power = (float)material.GetShaderParameter("glow_power");
		}

		Global.Instance.FlatShadingEnabled += OnFlatShadingEnabled;
	}

	public void SetMaterial(ShaderMaterial newMaterial)
	{
		material = newMaterial;
		mesh.MaterialOverride = material;
	}

	public void OnFlatShadingEnabled(bool enabled)
	{
		if (shadingMeshList == null)
			return;

		foreach (MeshInstance3D m in shadingMeshList)
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

	public abstract void EnableLight(bool flag);
}
