using Godot;
using System;

public partial class Global : Node
{
	public static Global Instance { get; private set; }

	[Signal] public delegate void StreetLightsEnabledEventHandler(bool enabled);
	[Signal] public delegate void FloatingStreetLightsEnabledEventHandler(bool enabled);
	[Signal] public delegate void SearchLightsEnabledEventHandler(bool enabled);
	[Signal] public delegate void BuildingLightsEnabledEventHandler(bool enabled);
	[Signal] public delegate void CarLightsEnabledEventHandler(bool enabled);
	[Signal] public delegate void HologramsEnabledEventHandler(bool enabled);

	[Signal] public delegate void FlatShadingEnabledEventHandler(bool enabled);
	[Signal] public delegate void GouraudShadingEnabledEventHandler(bool enabled);
	[Signal] public delegate void PhongShadingEnabledEventHandler(bool enabled);

	public override void _Ready()
	{
		base._Ready();

		Instance = this;
	}
}
