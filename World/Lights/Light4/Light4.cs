using Godot;
using System;

public partial class Light4 : Light
{
	[Export] OmniLight3D omniLight;

	public override void _Ready()
	{
		base._Ready();

		omniLight = GetNode<OmniLight3D>("%OmniLight");

		Global.Instance.FloatingStreetLightsEnabled += EnableLight;
	}

	public override void _ExitTree()
	{
		base._ExitTree();

		Global.Instance.FloatingStreetLightsEnabled -= EnableLight;
	}

	public override void EnableLight(bool flag)
	{
		ShaderMaterial mat = (ShaderMaterial)mesh.MaterialOverride;
		mat.SetShaderParameter("glow_power", flag ? glow_power : 0);
		omniLight.Visible = flag;
	}
}
