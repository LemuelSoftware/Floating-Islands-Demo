using Godot;
using System;

public partial class World : Node3D
{
	private Node3D drones;

	private Elevator1 elevator1;
	private Elevator1 elevator2;
	private Elevator1 elevator3;
	private Elevator1 elevator4;
	private Elevator1 elevator5;

	private Node3D carPaths;
	private DayNightCycle dayNightCycle;

	public override void _Ready()
	{
		base._Ready();

		elevator1 = GetNode<Elevator1>("%Elevator1");
		elevator2 = GetNode<Elevator1>("%Elevator2");
		elevator3 = GetNode<Elevator1>("%Elevator3");
		elevator4 = GetNode<Elevator1>("%Elevator4");
		elevator5 = GetNode<Elevator1>("%Elevator5");

		drones = GetNode<Node3D>("%Drones");
		carPaths = GetNode<Node3D>("%CarPaths");

		dayNightCycle = GetNode<DayNightCycle>("%DayNightCycle");
	}

	public override void _Process(double delta)
	{
		base._Process(delta);

		moveElevators();
	}

	private void moveElevators()
	{
		elevator1.UpdateProgress();
		elevator2.UpdateProgress();
		elevator3.UpdateProgress();
		elevator4.UpdateProgress();
		elevator5.UpdateProgress();
	}

	public void rotateDrones(long index, bool flag)
	{
		switch (index)
		{
			case 0:
				drones.GetChild<Drone>(0).setRotating(flag);
				drones.GetChild<Drone>(1).setRotating(flag);
				break;
			case 1:
				drones.GetChild<Drone>(2).setRotating(flag);
				drones.GetChild<Drone>(3).setRotating(flag);
				break;
			case 2:
				drones.GetChild<Drone>(4).setRotating(flag);
				drones.GetChild<Drone>(5).setRotating(flag);
				break;
		}
	}

	public void EnableFlatShading(bool toggled)
	{
		Global.Instance.EmitSignal(Global.SignalName.FlatShadingEnabled, toggled);
	}

	public void EnableGouraudShading(bool toggled)
	{
		Global.Instance.EmitSignal(Global.SignalName.GouraudShadingEnabled, toggled);
	}

	public void EnablePhongShading(bool toggled)
	{
		Global.Instance.EmitSignal(Global.SignalName.PhongShadingEnabled, toggled);
	}

	public void StopCars(bool flag)
	{
		foreach (Path3D path in carPaths.GetChildren())
		{
			for (int i = 0; i < path.GetChildCount(); i++)
			{
				if (path.GetChild(i) is Vehicle)
				{
					Vehicle car = (Vehicle)path.GetChild(i);
					car.StopCar(flag);
				}
				else
				{
					break;
				}
			}
		}
	}

	public void DayNightCycle(double value)
	{
		dayNightCycle.Rotate((float)value);
	}
}
