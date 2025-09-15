using Godot;
using System;

public partial class ObjectRect : ColorRect
{
	private AudioStreamPlayer _fixedSound;
	
	public override void _Ready()
	{
		GuiInput += OnGuiInput;
		_fixedSound = GetTree().CurrentScene.GetNode<AudioStreamPlayer>("Audio/FixedSound");
		GD.Print(_fixedSound != null ? "FixedSound found" : "FixedSound NOT found");
	}

	private void OnGuiInput(InputEvent @event)
	{
		if (@event is InputEventMouseButton mouseEvent && mouseEvent.Pressed)
		{
			Hide();
			_fixedSound.Play();
		}
	}
}
