using Godot;
using System;
using System.Collections.Generic;

public partial class RemovableObjectContainer : ColorRect
{
	[Signal]
	public delegate void RemovedObjectEventHandler();

	private AudioStreamPlayer _fixedSound;

	public override void _Ready()
	{
		ChooseRandomObjectToRemoveName();
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

	private string ChooseRandomObjectToRemoveName()
	{
		var objectToRemoveSprites = new List<Sprite2D>();
		foreach (Node child in GetChildren())
		{
			if (child is Sprite2D sprite)
			{
				objectToRemoveSprites.Add(sprite);
			}
		}
		var randomIndex = (int)(GD.Randi() % (uint)objectToRemoveSprites.Count);
		var chosenSpriteToRemove = objectToRemoveSprites[randomIndex];
		foreach (var sprite in objectToRemoveSprites)
		{
			sprite.Visible = false;
		}
		chosenSpriteToRemove.Visible = true;
		GD.Print("ObjectToRemove=" + chosenSpriteToRemove.Name);
		return chosenSpriteToRemove.Name;
	}
}
