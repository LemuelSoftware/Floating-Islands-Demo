using Godot;
using System;

public abstract partial class Light : StaticBody3D
{
	[Export] protected ShaderMaterial material;
	[Export] protected MeshInstance3D mesh;

	protected float glow_power;

	public override void _Ready()
	{
		base._Ready();

		if (material != null && mesh != null)
		{
			mesh.MaterialOverride = material;
			glow_power = (float)material.GetShaderParameter("glow_power");
		}
	}

	public void SetMaterial(ShaderMaterial newMaterial)
	{
		material = newMaterial;
		mesh.MaterialOverride = material;
	}

	public abstract void EnableLight(bool flag);
}
