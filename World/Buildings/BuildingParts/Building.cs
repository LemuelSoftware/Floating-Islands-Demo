using Godot;
using System;

public partial class Building : Node3D
{
	[Export] Color glowColor;
	[Export(PropertyHint.Range, "0, 10, 0.1")] float glowPower;
	[Export] Light[] lights;

	ShaderMaterial material;

	public override void _Ready()
	{
		base._Ready();

		material = new ShaderMaterial();
		material.Shader = GD.Load("res://World/Lights/Shader/LightShader1.gdshader") as Shader;
		material.SetShaderParameter("glow_color", glowColor);
		material.SetShaderParameter("glow_power", glowPower);

		for (int i = 0; i < lights.Length; i++)
		{
			lights[i].SetMaterial(material);
		}
	}
}
