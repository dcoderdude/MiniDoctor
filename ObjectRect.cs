using Godot;
using System;

public partial class ObjectRect : ColorRect
{
	public override void _Ready()
	{
		GuiInput += OnGuiInput;
	}

	private void OnGuiInput(InputEvent @event)
	{
		if (@event is InputEventMouseButton mouseEvent && mouseEvent.Pressed)
		{
			Hide();
		}
	}
}
