using Godot;
using System;

public partial class Platform5 : Platform
{
	public override void OnFlatShadingEnabled(bool enabled)
	{
		base.OnFlatShadingEnabled(enabled);

		if (mesh == null)
			return;

		ShaderMaterial mat = (ShaderMaterial)mesh.GetSurfaceOverrideMaterial(0);

		if (mat == null)
			return;

		mat.SetShaderParameter("enable", enabled);
		mesh.SetSurfaceOverrideMaterial(0, mat);
	}
}
