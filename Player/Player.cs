using Godot;
using System;
using System.Numerics;
using System.Text.RegularExpressions;

/*
	KEY BINDINGS:
	W - forward
	S - backward
	A - left
	D - right
	CTRL - down
	Spacebar - up
	Shift - increase speed

	MOUSE:
	Middle button - reset zoom
	Left button - interact with UI
	Right button - toggle mouse catpured or released
*/

public partial class Player : CharacterBody3D
{
	[Export] private int mouseSensitivity = 5;
	[Export] private float zoomSpeed = 1.0f;
	[Export] private float minZoom = -50.0f;
	[Export] private float maxZoom = 0.0f;
	[Export] private float verticalSpeed = 800.0f;
	[Export] private float horizontalSpeed = 800.0f;
	[Export] private float speedBoost = 800.0f;

	private float addBoost = 0.0f;
	private Camera3D camera;
	private Godot.Vector3 cameraPosition;

	public override void _Ready()
	{
		base._Ready();
		camera = GetNode<Camera3D>("Camera");
		cameraPosition = camera.Position;
		CaptureMouse(true);
	}

	// Get user input every physics frame
	public override void _Process(double delta)
	{
		base._Process(delta);

		Godot.Vector3 velocity = Velocity;
		Godot.Vector2 inputDir = Input.GetVector("left", "right", "forward", "backward");
		Godot.Vector3 direction = (Transform.Basis * new Godot.Vector3(inputDir.X, 0, inputDir.Y)).Normalized();

		if (direction != Godot.Vector3.Zero)
		{
			velocity.X = direction.X * (horizontalSpeed + addBoost);
			velocity.Z = direction.Z * (horizontalSpeed + addBoost);
		}
		else
		{
			velocity.X = Mathf.MoveToward(direction.X, 0, horizontalSpeed + addBoost);
			velocity.Z = Mathf.MoveToward(direction.Z, 0, horizontalSpeed + addBoost);
		}

		if (Input.IsActionPressed("up"))
			velocity.Y = verticalSpeed + addBoost;
		else if (Input.IsActionPressed("down"))
			velocity.Y = -verticalSpeed - addBoost;
		else
			velocity.Y = Mathf.MoveToward(velocity.Y, 0, verticalSpeed + addBoost);

		if (Input.IsActionPressed("shift"))
			addBoost = speedBoost;
		else
			addBoost = 0;

		if (Input.IsActionJustPressed("ui_cancel"))
		{
			CaptureMouse(!IsMouseCaptured());
		}

		if (Input.IsActionJustPressed("mouse_left_click"))
		{

		}

		if (Input.IsActionJustPressed("mouse_right_click"))
		{
			CaptureMouse(!IsMouseCaptured());
		}

		Velocity = velocity * (float)delta;
		MoveAndSlide();
	}

	// Rotates the player by moving the mouse
	public override void _Input(InputEvent @event)
	{
		base._Input(@event);

		if (!IsMouseCaptured())
			return;

		if (@event is InputEventMouseMotion)
		{
			InputEventMouseMotion motion = (InputEventMouseMotion)@event;
			Rotation = new Godot.Vector3(
					Mathf.Clamp(Rotation.X - motion.Relative.Y / 1000 * mouseSensitivity, -1.40f, 0.85f),
					Rotation.Y - motion.Relative.X / 1000 * mouseSensitivity,
					0
				);
		}

		if (@event is InputEventMouseButton mouseWheel && mouseWheel.Pressed)
		{
			if (mouseWheel.ButtonIndex == MouseButton.WheelUp)
			{
				UpdateZoom(-1.0f);
			}
			else if (mouseWheel.ButtonIndex == MouseButton.WheelDown)
			{
				UpdateZoom(1.0f);
			}
		}

		if (@event is InputEventMouseButton mouseButton)
		{
			if (mouseButton.ButtonIndex == MouseButton.Middle)
			{
				cameraPosition.Z = maxZoom;
				camera.Position = cameraPosition;
			}
		}
	}

	private void UpdateZoom(float direction)
	{
		cameraPosition.Z = Mathf.Clamp(cameraPosition.Z + direction * zoomSpeed, minZoom, maxZoom);
		camera.Position = cameraPosition;
	}

	// Set mouse mode to captured or visible
	public void CaptureMouse(bool flag)
	{
		if (flag)
		{
			Input.MouseMode = Input.MouseModeEnum.Captured;
		}
		else
		{
			Input.MouseMode = Input.MouseModeEnum.Visible;
		}
	}

	public bool IsMouseCaptured()
	{
		switch (Input.MouseMode)
		{
			case Input.MouseModeEnum.Captured:
				return true;

			case Input.MouseModeEnum.Visible:
				return false;
		}

		return false;
	}
}
