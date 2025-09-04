using Godot;
using System;

public partial class Platform4 : Platform
{
	public override void OnGouraudShadingEnabled(bool enabled)
	{
		base.OnGouraudShadingEnabled(enabled);

		if (mesh == null)
			return;

		StandardMaterial3D mat = (StandardMaterial3D)mesh.GetSurfaceOverrideMaterial(0);

		if (mat == null)
			return;

		if (enabled)
			mat.ShadingMode = StandardMaterial3D.ShadingModeEnum.PerVertex;
		else
			mat.ShadingMode = StandardMaterial3D.ShadingModeEnum.PerPixel;

		mesh.SetSurfaceOverrideMaterial(0, mat);
	}
}
