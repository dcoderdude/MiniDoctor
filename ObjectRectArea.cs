using Godot;
using System;

public partial class ObjectRectArea : Area2D
{
	private AudioStreamPlayer _fixedSound;
	private Sprite2D _sprite;

	public override void _Ready()
	{
		_sprite = GetNode<Sprite2D>("ObjectRect");
		_fixedSound = GetTree().CurrentScene.GetNode<AudioStreamPlayer>("Audio/FixedSound");
	}

	private void InputEventHandler(Node2D viewport, InputEvent @event, long shapeIdx)
	{
		if (@event is InputEventMouseButton mouseEvent && mouseEvent.Pressed)
		{
			_sprite.Hide();
			_fixedSound.Play();
		}
	}
}
