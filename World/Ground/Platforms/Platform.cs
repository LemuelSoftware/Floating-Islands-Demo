using Godot;
using System;

public partial class Platform : StaticBody3D
{
	[Export] protected MeshInstance3D mesh;

	public override void _Ready()
	{
		base._Ready();

		Global.Instance.FlatShadingEnabled += OnFlatShadingEnabled;
		Global.Instance.GouraudShadingEnabled += OnGouraudShadingEnabled;
		Global.Instance.PhongShadingEnabled += OnPhongShadingEnabled;
	}

	public virtual void OnFlatShadingEnabled(bool enabled)
	{
	}

	public virtual void OnGouraudShadingEnabled(bool enabled)
	{
	}

	public virtual void OnPhongShadingEnabled(bool enabled)
	{
	}

	public override void _ExitTree()
	{
		base._ExitTree();

		Global.Instance.FlatShadingEnabled -= OnFlatShadingEnabled;
		Global.Instance.GouraudShadingEnabled -= OnGouraudShadingEnabled;
		Global.Instance.PhongShadingEnabled -= OnPhongShadingEnabled;
	}
}
