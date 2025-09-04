using Godot;
using System;

public partial class ControlPanel : Control
{
	[Signal] public delegate void StopCarsToggledEventHandler(bool toggled);
	[Signal] public delegate void StopDronesPressedEventHandler(long index);
	[Signal] public delegate void StartDronesPressedEventHandler(long index);
	[Signal] public delegate void DayNightValueChangedEventHandler(double value);
	[Signal] public delegate void StreetLightsToggledEventHandler(bool toggled);
	[Signal] public delegate void FloatingStreetLightsToggledEventHandler(bool toggled);
	[Signal] public delegate void SearchLightsToggledEventHandler(bool toggled);
	[Signal] public delegate void BuildingLightsToggledEventHandler(bool toggled);
	[Signal] public delegate void CarLightsToggledEventHandler(bool toggled);
	[Signal] public delegate void HologramsToggledEventHandler(bool toggled);

	private CheckButton flatShadingToggle;
	private CheckButton gouraudShadingToggle;
	private CheckButton phongShadingToggle;
	private CheckButton stopCarsToggle;
	private Button stopDronesButton;
	private Button startDronesButton;
	private OptionButton droneOption;
	private HSlider dayNightSlider;
	private CheckButton streetLightsToggle;
	private CheckButton floatingStreetLightsToggle;
	private CheckButton searchLightsToggle;
	private CheckButton buildingLightsToggle;
	private CheckButton carLightsToggle;
	private CheckButton hologramsToggle;

	private long droneOptionSelected = 0;

	public override void _Ready()
	{
		base._Ready();

		flatShadingToggle = GetNode<CheckButton>("%FlatShadingToggle");
		gouraudShadingToggle = GetNode<CheckButton>("%GouraudShadingToggle");
		phongShadingToggle = GetNode<CheckButton>("%PhongShadingToggle");
		stopCarsToggle = GetNode<CheckButton>("%StopCarsToggle");
		stopDronesButton = GetNode<Button>("%StopDronesButton");
		startDronesButton = GetNode<Button>("%StartDronesButton");
		droneOption = GetNode<OptionButton>("%DroneOption");
		dayNightSlider = GetNode<HSlider>("%DayNightSlider");
		streetLightsToggle = GetNode<CheckButton>("%StreetLightsToggle");
		floatingStreetLightsToggle = GetNode<CheckButton>("%FloatingStreetLightsToggle");
		searchLightsToggle = GetNode<CheckButton>("%SearchLightsToggle");
		buildingLightsToggle = GetNode<CheckButton>("%BuildingLightsToggle");
		carLightsToggle = GetNode<CheckButton>("%CarLightsToggle");
		hologramsToggle = GetNode<CheckButton>("%HologramsToggle");

		stopCarsToggle.Toggled += OnStopCarsToggled;
		droneOption.ItemSelected += OnDroneOptionSelected;
		stopDronesButton.Pressed += OnStopDronesPressed;
		startDronesButton.Pressed += OnStartDronesPressed;
		dayNightSlider.ValueChanged += OnDayNightValueChanged;

		streetLightsToggle.Toggled += OnStreetLightsToggled;
		floatingStreetLightsToggle.Toggled += OnFloatingStreetLightsToggled;
		searchLightsToggle.Toggled += OnSearchLightsToggled;
		buildingLightsToggle.Toggled += OnBuildingLightsToggled;
		carLightsToggle.Toggled += OnCarLightsToggled;
		hologramsToggle.Toggled += OnHologramsToggled;
	}

	private void OnFlatShadingToggled(bool toggled)
	{
		GD.Print("flat shading toggled ", toggled);
		EmitSignal(SignalName.FlatShadingToggled, toggled);
	}

	private void OnGouraudShadingToggled(bool toggled)
	{
		GD.Print("gouraud shading toggled ", toggled);
		EmitSignal(SignalName.GouraudShadingToggled, toggled);
	}

	private void OnPhongShadingToggled(bool toggled)
	{
		GD.Print("phong shading toggled ", toggled);
		EmitSignal(SignalName.PhongShadingToggled, toggled);
	}

	private void OnStopCarsToggled(bool toggled)
	{
		GD.Print("cars stopped ", toggled);
		EmitSignal(SignalName.StopCarsToggled, toggled);
	}

	private void OnStopDronesPressed()
	{
		GD.Print("drones group ", droneOptionSelected + 1," stopped");
		EmitSignal(SignalName.StopDronesPressed, droneOptionSelected);
	}

	private void OnStartDronesPressed()
	{
		GD.Print("drones group ", droneOptionSelected + 1," started");
		EmitSignal(SignalName.StartDronesPressed, droneOptionSelected);
	}

	private void OnDroneOptionSelected(long index)
	{
		GD.Print("drone option selected ", index);
		droneOptionSelected = index;
	}

	private void OnDayNightValueChanged(double value)
	{
		GD.Print("day night option selected ", value);
		EmitSignal(SignalName.DayNightValueChanged, value);
	}

	private void OnStreetLightsToggled(bool toggled)
	{
		GD.Print("street lights toggled ", toggled);
		EmitSignal(SignalName.StreetLightsToggled, toggled);
	}

	private void OnFloatingStreetLightsToggled(bool toggled)
	{
		GD.Print("floating street lights toggled ", toggled);
		EmitSignal(SignalName.FloatingStreetLightsToggled, toggled);
	}

	private void OnSearchLightsToggled(bool toggled)
	{
		GD.Print("search lights toggled ", toggled);
		EmitSignal(SignalName.SearchLightsToggled, toggled);
	}

	private void OnBuildingLightsToggled(bool toggled)
	{
		GD.Print("building lights toggled ", toggled);
		EmitSignal(SignalName.BuildingLightsToggled, toggled);
	}

	private void OnCarLightsToggled(bool toggled)
	{
		GD.Print("car lights toggled ", toggled);
		EmitSignal(SignalName.CarLightsToggled, toggled);
	}

	private void OnHologramsToggled(bool toggled)
	{
		GD.Print("holograms toggled ", toggled);
		EmitSignal(SignalName.HologramsToggled, toggled);
	}

	public override void _ExitTree()
	{
		base._ExitTree();

		flatShadingToggle.Toggled -= OnFlatShadingToggled;
		gouraudShadingToggle.Toggled -= OnGouraudShadingToggled;
		phongShadingToggle.Toggled -= OnPhongShadingToggled;
		stopCarsToggle.Toggled -= OnStopCarsToggled;
		droneOption.ItemSelected -= OnDroneOptionSelected;
		stopDronesButton.Pressed -= OnStopDronesPressed;
		startDronesButton.Pressed -= OnStartDronesPressed;
		dayNightSlider.ValueChanged -= OnDayNightValueChanged;

		streetLightsToggle.Toggled -= OnStreetLightsToggled;
		floatingStreetLightsToggle.Toggled -= OnFloatingStreetLightsToggled;
		searchLightsToggle.Toggled -= OnSearchLightsToggled;
		buildingLightsToggle.Toggled -= OnBuildingLightsToggled;
		carLightsToggle.Toggled -= OnCarLightsToggled;
		hologramsToggle.Toggled -= OnHologramsToggled;
	}
}
