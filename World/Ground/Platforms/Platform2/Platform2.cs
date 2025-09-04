using Godot;
using System;

public partial class Platform2 : Platform
{
	[Export] ShaderMaterial shader;

	public override void _Ready()
	{
		base._Ready();

		mesh.SetSurfaceOverrideMaterial(0, shader);
	}

	public override void OnPhongShadingEnabled(bool enabled)
	{
		base.OnPhongShadingEnabled(enabled);

		if (mesh == null)
			return;

		if (shader == null)
			return;

		if (enabled)
			mesh.SetSurfaceOverrideMaterial(0, shader);
		else
			mesh.SetSurfaceOverrideMaterial(0, null);

		GD.Print("phong platform ", shader == null);
	}
}
