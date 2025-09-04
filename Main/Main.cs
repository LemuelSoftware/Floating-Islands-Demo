using Godot;
using System;

public partial class Main : Node3D
{
	private Button panelButton;
	private Button helpButton;

	private HelpPanel helpPanel;
	private ControlPanel controlPanel;

	private Player player;
	private World world;

	private bool isMouseVisible = false;

	public override void _Ready()
	{
		base._Ready();

		panelButton = GetNode<Button>("%PanelButton");
		helpButton = GetNode<Button>("%HelpButton");

		helpPanel = GetNode<HelpPanel>("%HelpPanel");
		controlPanel = GetNode<ControlPanel>("%ControlPanel");

		player = GetNode<Player>("%Player");
		world = GetNode<World>("%World");

		helpPanel.Visible = false;

		panelButton.Pressed += OnPanelButtonPressed;
		helpButton.Pressed += OnHelpButtonPressed;

		controlPanel.StopCarsToggled += OnControlPanelStopCarsToggled;
		controlPanel.StopDronesPressed += OnControlPanelStopDronesPressed;
		controlPanel.StartDronesPressed += OnControlPanelStartDronesPressed;
		controlPanel.DayNightValueChanged += OnControlPanelDayNightValueChanged;

		controlPanel.StreetLightsToggled += OnControlPanelStreetLightsToggled;
		controlPanel.FloatingStreetLightsToggled += OnControlPanelFloatingStreetLightsToggled;
		controlPanel.SearchLightsToggled += OnControlPanelSearchLightsToggled;
		controlPanel.BuildingLightsToggled += OnControlPanelBuildingLightsToggled;
		controlPanel.CarLightsToggled += OnControlPanelCarLightsToggled;
		controlPanel.HologramsToggled += OnControlPanelHologramsToggled;
	}

	public override void _Input(InputEvent @event)
	{
		base._Input(@event);

		if (@event is InputEventKey key && key.IsPressed())
		{
			if (key.Keycode == Key.F1)
				ShowPanels();

			if (key.Keycode == Key.F2)
			{
				if (helpPanel.IsOpen())
				{
					helpPanel.Close();
					if (isMouseVisible)
					{
						player.CaptureMouse(false);
					}
					else
					{
						player.CaptureMouse(true);
					}
				}
				else
				{
					ShowHelpPanel();
				}
			}

		}
	}

	private void ShowPanels()
	{
		controlPanel.Visible = !controlPanel.Visible;

		if (helpPanel.IsOpen())
			helpPanel.Visible = !helpPanel.Visible;
		else
			helpPanel.Visible = false;
	}

	private void ShowHelpPanel()
	{
		helpPanel.Open();
		isMouseVisible = !player.IsMouseCaptured();
		player.CaptureMouse(false);
	}

	private void OnPanelButtonPressed()
	{
		ShowPanels();
	}

	private void OnHelpButtonPressed()
	{
		ShowHelpPanel();
	}

	private void OnControlPanelStopCarsToggled(bool toggled)
	{
		world.StopCars(toggled);
	}

	private void OnControlPanelStopDronesPressed(long index)
	{
		world.rotateDrones(index, false);
	}

	private void OnControlPanelStartDronesPressed(long index)
	{
		world.rotateDrones(index, true);
	}

	private void OnControlPanelDayNightValueChanged(double value)
	{
		world.DayNightCycle(value);
	}

	private void OnControlPanelStreetLightsToggled(bool toggled)
	{
		Global.Instance.EmitSignal(Global.SignalName.StreetLightsEnabled, toggled);
	}

	private void OnControlPanelFloatingStreetLightsToggled(bool toggled)
	{
		Global.Instance.EmitSignal(Global.SignalName.FloatingStreetLightsEnabled, toggled);
	}

	private void OnControlPanelSearchLightsToggled(bool toggled)
	{
		Global.Instance.EmitSignal(Global.SignalName.SearchLightsEnabled, toggled);
	}

	private void OnControlPanelBuildingLightsToggled(bool toggled)
	{
		Global.Instance.EmitSignal(Global.SignalName.BuildingLightsEnabled, toggled);
	}

	private void OnControlPanelCarLightsToggled(bool toggled)
	{
		Global.Instance.EmitSignal(Global.SignalName.CarLightsEnabled, toggled);
	}

	private void OnControlPanelHologramsToggled(bool toggled)
	{
		Global.Instance.EmitSignal(Global.SignalName.HologramsEnabled, toggled);
	}

	public override void _ExitTree()
	{
		panelButton.Pressed -= OnPanelButtonPressed;
		helpButton.Pressed -= OnHelpButtonPressed;

		controlPanel.StopCarsToggled -= OnControlPanelStopCarsToggled;
		controlPanel.StopDronesPressed -= OnControlPanelStopDronesPressed;
		controlPanel.StartDronesPressed -= OnControlPanelStartDronesPressed;
		controlPanel.DayNightValueChanged -= OnControlPanelDayNightValueChanged;

		controlPanel.StreetLightsToggled -= OnControlPanelStreetLightsToggled;
		controlPanel.FloatingStreetLightsToggled -= OnControlPanelFloatingStreetLightsToggled;
		controlPanel.SearchLightsToggled -= OnControlPanelSearchLightsToggled;
		controlPanel.BuildingLightsToggled -= OnControlPanelBuildingLightsToggled;
		controlPanel.CarLightsToggled -= OnControlPanelCarLightsToggled;
		controlPanel.HologramsToggled -= OnControlPanelHologramsToggled;
	}
}
