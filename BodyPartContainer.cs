using Godot;
using System;
using System.Collections.Generic;

public partial class BodyPartContainer : Sprite2D
{
	[Signal]
	public delegate void BodyPartEventHandler(Sprite2D bodyPart);
	
	public override void _Ready()
	{
		CallDeferred(nameof(BodyPartToExamine), GetNode<Sprite2D>(ChooseRandomBodyPartName()));
	}
	
	private void BodyPartToExamine(Sprite2D sprite)
	{
		EmitSignal("BodyPart", sprite);
	}
	
	private string ChooseRandomBodyPartName()
	{
		var bodyPartSprites = new List<Sprite2D>();
		foreach (Node child in GetChildren())
		{
			if (child is Sprite2D sprite)
			{
				bodyPartSprites.Add(sprite);
			}
		}
		var randomIndex = (int)(GD.Randi() % (uint)bodyPartSprites.Count);
		var chosenSprite = bodyPartSprites[randomIndex];
		foreach (var sprite in bodyPartSprites)
		{
			sprite.Visible = false;
		}
		chosenSprite.Visible = true;
	
		return chosenSprite.Name;
	}
}
