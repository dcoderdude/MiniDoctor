using Godot;
using System;

public partial class BodyPartContainer : ColorRect
{
	[Signal]
	public delegate void BodyPartEventHandler(Sprite2D bodyPart);
	
	public override void _Ready()
	{
		CallDeferred(nameof(BodyPartToExamine), GetNode<Sprite2D>("EarSprite"));
	}
	
	private void BodyPartToExamine(Sprite2D sprite)
	{
		EmitSignal("BodyPart", sprite);
	}
}
