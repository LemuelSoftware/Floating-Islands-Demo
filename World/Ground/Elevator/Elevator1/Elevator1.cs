using Godot;
using System;
using System.Threading.Tasks;

public partial class Elevator1 : PathFollow3D
{
	[Export] float speed = 0.5f;
	[Export] double delay = 1.0;
	[Export] MeshInstance3D mesh;

	private Timer timer;

	private bool isEnd = false;

	public override void _Ready()
	{
		base._Ready();

		timer = GetNode<Timer>("%Timer");

		timer.WaitTime = delay;

		timer.Timeout += OnTimerTimeout;
		Global.Instance.FlatShadingEnabled += OnFlatShadingEnabled;
	}

	public void UpdateProgress()
	{
		if ((ProgressRatio >= 1.0f || ProgressRatio <= 0.0f) && timer.IsStopped())
			timer.Start();

		if (isEnd)
		{
			Progress -= speed;
		}
		else
		{
			Progress += speed;
		}
	}

	public void OnFlatShadingEnabled(bool enabled)
	{
		if (mesh == null)
			return;
		
		ShaderMaterial mat = (ShaderMaterial)mesh.GetSurfaceOverrideMaterial(0);

		if (mat == null)
			return;

		mat.SetShaderParameter("enable", enabled);
		mesh.SetSurfaceOverrideMaterial(0, mat);
	}

	private void OnTimerTimeout()
	{
		if (ProgressRatio >= 1.0f)
			isEnd = true;

		if (ProgressRatio <= 0.0f)
			isEnd = false;
	}

	public override void _ExitTree()
	{
		base._ExitTree();

		timer.Timeout -= OnTimerTimeout;
		Global.Instance.FlatShadingEnabled -= OnFlatShadingEnabled;
	}
}
