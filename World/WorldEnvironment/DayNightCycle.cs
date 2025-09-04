using Godot;
using System;

public partial class DayNightCycle : Node3D
{
	[Export] float start = 0.26f;
	[Export] Gradient skyTopColor;
	[Export] Gradient skyHorizonColor;
	[Export] Gradient groundBottomColor;
	[Export] Gradient groundHorizonColor;
	[Export] Gradient sunColor;
	[Export] Curve sunIntensity;
	[Export] Gradient moonColor;
	[Export] Curve moonIntensity;

	private DirectionalLight3D sun;
	private DirectionalLight3D moon;
	private WorldEnvironment environment;

	public override void _Ready()
	{
		base._Ready();

		sun = GetNode<DirectionalLight3D>("%Sun");
		moon = GetNode<DirectionalLight3D>("%Moon");
		environment = GetNode<WorldEnvironment>("%WorldEnvironment");

		Rotate(start);
	}

	public void Rotate(float value)
	{
		RotateSun(value);
		RotateMoon(value);
		UpdateEnvironment(value);
	}

	public void RotateSun(float value)
	{
		sun.RotationDegrees = new Vector3(value * 360.0f + 90.0f, 0, 0);
		sun.LightColor = sunColor.Sample(value);
		sun.LightEnergy = sunIntensity.Sample(value);
		sun.Visible = sun.LightEnergy > 0.0f;
	}

	public void RotateMoon(float value)
	{
		moon.RotationDegrees = new Vector3(value * 360.0f + 270.0f, 0, 0);
		moon.LightColor = moonColor.Sample(value);
		moon.LightEnergy = moonIntensity.Sample(value);
		moon.Visible = moon.LightEnergy > 0.0f;
	}

	public void UpdateEnvironment(float value)
	{
		environment.Environment.Sky.SkyMaterial.Set("sky_top_color", skyTopColor.Sample(value));
		environment.Environment.Sky.SkyMaterial.Set("sky_horizon_color", skyHorizonColor.Sample(value));
		environment.Environment.Sky.SkyMaterial.Set("ground_bottom_color", groundBottomColor.Sample(value));
		environment.Environment.Sky.SkyMaterial.Set("ground_horizon_color", groundHorizonColor.Sample(value));
	}
}
