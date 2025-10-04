using Godot;
using System;

public partial class Patient : Node2D
{
	[Export] private NodePath neutralPath;
	[Export] private NodePath discomfortPath;
	[Export] private NodePath joyPath;

	private Sprite2D neutralSprite;
	private Sprite2D discomfortSprite;
	private Sprite2D joySprite;

	public override void _Ready()
	{
		RemovableObjectContainer removableObjects = GetNode<RemovableObjectContainer>("ExaminationSection/ExaminationContainer/RemovableObjectContainer");
		removableObjects.Connect("RemovedObject", new Callable(this, "OnObjectRemoved"));
		
		neutralSprite = GetNode<Sprite2D>(neutralPath);
		discomfortSprite = GetNode<Sprite2D>(discomfortPath);
		joySprite = GetNode<Sprite2D>(joyPath);

		neutralSprite.Visible = false;
		discomfortSprite.Visible = true;
		joySprite.Visible = false;
	}

	private void OnObjectRemoved()
	{
		neutralSprite.Visible = false;
		discomfortSprite.Visible = false;
		joySprite.Visible = true;

		Timer timer = new Timer();
		AddChild(timer);
		timer.WaitTime = 1.0f;
		timer.OneShot = true;
		timer.Timeout += () =>
		{
			joySprite.Visible = false;
			neutralSprite.Visible = true;
			timer.QueueFree();
		};
		timer.Start();
	}
}
