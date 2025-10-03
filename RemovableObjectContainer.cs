using Godot;
using System;

public partial class RemovableObjectContainer : ColorRect
{
	private AudioStreamPlayer _fixedSound;
	
	public override void _Ready()
	{
		GuiInput += OnGuiInput;
		_fixedSound = GetTree().CurrentScene.GetNode<AudioStreamPlayer>("Audio/FixedSound");
	}
	
	// TODO: create a signal that the removeable object was removed
	private void OnGuiInput(InputEvent @event)
	{
		if (@event is InputEventMouseButton mouseEvent && mouseEvent.Pressed)
		{
			Hide();
			_fixedSound.Play();
		}
	}
}
