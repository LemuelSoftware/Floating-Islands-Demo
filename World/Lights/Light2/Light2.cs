using Godot;
using System;

public partial class Light2 : Light
{
	[Export] int maxRotationDegree = 45;
	[Export] int minRotationDegree = -45;
	[Export] float speed = 20.0f;
	[Export] float delay = 2.0f;

	[Export] ShaderMaterial coneMaterial;
	[Export] MeshInstance3D coneMesh;

	private StaticBody3D light;
	private Timer timer;
	private MeshInstance3D glow;
	private MeshInstance3D cone;
	private SpotLight3D spotLight;

	private bool canRotate = true;
	private int direction = 1;

	public override void _Ready()
	{
		base._Ready();

		light = GetNode<StaticBody3D>("%Light");
		timer = GetNode<Timer>("%Timer");
		glow = GetNode<MeshInstance3D>("%Glow");
		cone = GetNode<MeshInstance3D>("%Cone");
		spotLight = GetNode<SpotLight3D>("%SpotLight");

		timer.Timeout += OnTimerTimeTimeout;
		Global.Instance.SearchLightsEnabled += EnableLight;

		if (coneMaterial != null && coneMesh != null)
		{
			coneMesh.MaterialOverride = coneMaterial;
		}
	}

	public override void _Process(double delta)
	{
		base._Process(delta);

		if (canRotate)
		{
			light.RotationDegrees += new Vector3(speed * direction * (float)delta, 0, 0);
		}

		if (canRotate && (light.RotationDegrees.X >= maxRotationDegree || light.RotationDegrees.X <= minRotationDegree))
		{
			canRotate = false;
			timer.Start(delay);
		}
	}

	public override void EnableLight(bool flag)
	{
		spotLight.Visible = flag;
		cone.Visible = flag;
		ShaderMaterial mat = (ShaderMaterial)mesh.MaterialOverride;
		mat.SetShaderParameter("glow_power", flag ? glow_power : 0);
	}

	private void OnTimerTimeTimeout()
	{
		direction *= -1;
		canRotate = true;
	}

	public override void _ExitTree()
	{
		base._ExitTree();

		timer.Timeout -= OnTimerTimeTimeout;
		Global.Instance.SearchLightsEnabled -= EnableLight;
	}
}
