using Godot;
using System;

public partial class Vehicle : PathFollow3D
{
	[Export] bool stop = false;
	[Export] float speed = 16;
	[Export] float frontLightPower = 0.8f;
	[Export] float rearLightPower = 0.6f;

	[Export] MeshInstance3D[] meshList;

	[ExportGroup("Front Light")]
	[Export] ShaderMaterial frontLightShaderMaterial;
	[Export] ShaderMaterial frontConeShaderMaterial;

	[ExportSubgroup("Left Light")]
	[Export] MeshInstance3D frontLeftLightMesh;
	[Export] MeshInstance3D frontLeftConeMesh;
	[Export] SpotLight3D frontLeftSpotLight;

	[ExportSubgroup("Right Light")]
	[Export] MeshInstance3D frontRightLightMesh;
	[Export] MeshInstance3D frontRightConeMesh;
	[Export] SpotLight3D frontRightSpotLight;

	[ExportGroup("Rear Light")]
	[Export] ShaderMaterial RearLightShaderMaterial;
	[Export] ShaderMaterial RearConeShaderMaterial;

	[ExportSubgroup("Left Light")]
	[Export] MeshInstance3D rearLeftLightMesh;
	[Export] MeshInstance3D rearLeftConeMesh;
	[Export] SpotLight3D rearLeftSpotLight;

	[ExportSubgroup("Right Light")]
	[Export] MeshInstance3D rearRightLightMesh;
	[Export] MeshInstance3D rearRightConeMesh;
	[Export] SpotLight3D rearRightSpotLight;


	public override void _Ready()
	{
		base._Ready();

		Global.Instance.CarLightsEnabled += enableLights;
		Global.Instance.FlatShadingEnabled += OnFlatShadingEnabled;

		SetMeshMaterial(frontLeftLightMesh, frontLightShaderMaterial);
		SetMeshMaterial(frontRightLightMesh, frontLightShaderMaterial);
		SetMeshMaterial(rearLeftLightMesh, RearLightShaderMaterial);
		SetMeshMaterial(rearRightLightMesh, RearLightShaderMaterial);

		SetMeshMaterial(frontLeftConeMesh, frontConeShaderMaterial);
		SetMeshMaterial(frontRightConeMesh, frontConeShaderMaterial);
		SetMeshMaterial(rearLeftConeMesh, RearConeShaderMaterial);
		SetMeshMaterial(rearRightConeMesh, RearConeShaderMaterial);
	}

	public override void _Process(double delta)
	{
		base._Process(delta);

		if (!stop)
		{
			Progress += speed * (float)delta;
		}
	}

	public void StopCar(bool flag)
	{
		stop = flag;
	}

	public void OnFlatShadingEnabled(bool enabled)
	{
		if (meshList == null)
			return;

		foreach (MeshInstance3D m in meshList)
		{
			if (m == null)
				return;

			ShaderMaterial mat;

			if (m.GetSurfaceOverrideMaterialCount() > 1)
			{
				for (int i = 0; i < m.GetSurfaceOverrideMaterialCount(); i++)
				{
					mat = (ShaderMaterial)m.GetSurfaceOverrideMaterial(i);

					if (mat == null)
						return;

					mat.SetShaderParameter("enable", enabled);
					m.SetSurfaceOverrideMaterial(i, mat);
				}
				return;
			}

			mat = (ShaderMaterial)m.GetSurfaceOverrideMaterial(0);

			if (mat == null)
				return;

			mat.SetShaderParameter("enable", enabled);
			m.SetSurfaceOverrideMaterial(0, mat);
		}
	}

	public void enableLights(bool flag)
	{

		SetShaderParameter(frontLeftLightMesh, "glow_power", flag ? frontLightPower : 0);
		SetShaderParameter(frontRightLightMesh, "glow_power", flag ? frontLightPower : 0);
		SetShaderParameter(rearLeftLightMesh, "glow_power", flag ? rearLightPower : 0);
		SetShaderParameter(rearRightLightMesh, "glow_power", flag ? rearLightPower : 0);

		SetVisible(frontLeftConeMesh, flag);
		SetVisible(frontRightConeMesh, flag);
		SetVisible(rearLeftConeMesh, flag);
		SetVisible(rearRightConeMesh, flag);

		SetVisible(frontLeftSpotLight, flag);
		SetVisible(frontRightSpotLight, flag);
		SetVisible(rearLeftSpotLight, flag);
		SetVisible(rearRightSpotLight, flag);
	}

	private void SetShaderParameter(MeshInstance3D mesh, StringName parameter, Variant value)
	{
		if (mesh == null || mesh.MaterialOverride == null)
			return;

		ShaderMaterial mat = (ShaderMaterial)mesh.MaterialOverride;
		mat.SetShaderParameter(parameter, value);
	}

	private void SetMeshMaterial(MeshInstance3D mesh, ShaderMaterial shaderMaterial)
	{
		if (mesh == null || shaderMaterial == null)
			return;

		mesh.MaterialOverride = shaderMaterial;
	}

	private void SetVisible(Node3D node, bool flag)
	{
		if (node == null)
			return;

		node.Visible = flag;
	}

	public override void _ExitTree()
	{
		base._ExitTree();

		Global.Instance.CarLightsEnabled -= enableLights;
		Global.Instance.FlatShadingEnabled -= OnFlatShadingEnabled;
	}
}
