using Godot;
using System;

public partial class RemovableObjectContainer : ColorRect
{
	[Signal]
	public delegate void RemovedObjectEventHandler();
	
	private AudioStreamPlayer _fixedSound;
	
	public override void _Ready()
	{
		GuiInput += OnGuiInput;
		_fixedSound = GetTree().CurrentScene.GetNode<AudioStreamPlayer>("Audio/FixedSound");
	}
	
	private void OnGuiInput(InputEvent @event)
	{
		if (@event is InputEventMouseButton mouseEvent && mouseEvent.Pressed)
		{
			Hide();
			_fixedSound.Play();
			
			EmitSignal("RemovedObject");
		}
	}
}
