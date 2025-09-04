using Godot;
using System;

public partial class Light1 : Light
{
	public override void _Ready()
	{
		base._Ready();

		Global.Instance.BuildingLightsEnabled += EnableLight;
	}

	public override void _ExitTree()
	{
		base._ExitTree();

		Global.Instance.BuildingLightsEnabled -= EnableLight;
	}

	public override void EnableLight(bool flag)
	{
		ShaderMaterial mat = (ShaderMaterial)mesh.MaterialOverride;
		mat.SetShaderParameter("glow_power", flag ? glow_power : 0);
	}
}
