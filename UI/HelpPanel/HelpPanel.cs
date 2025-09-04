using Godot;
using System;

public partial class HelpPanel : Control
{
	private bool isOpen = false;

	private Button backgroundButton;
	private Button closeButton;

	public override void _Ready()
	{
		base._Ready();

		backgroundButton = GetNode<Button>("%BackgroundButton");
		closeButton = GetNode<Button>("%CloseButton");

		closeButton.Pressed += OnClose;
		backgroundButton.Pressed += OnClose;
	}

	public void Open()
	{
		Visible = true;
		isOpen = true;
	}

	public void Close()
	{
		Visible = false;
		isOpen = false;
	}

	public bool IsOpen()
	{
		return isOpen;
	}

	private void OnClose()
	{
		Close();
	}

	public override void _ExitTree()
	{
		base._ExitTree();

		closeButton.Pressed -= OnClose;
		backgroundButton.Pressed -= OnClose;
	}
}
