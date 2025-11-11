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

	private Sprite2D _currentBodyPart;
	private AudioStreamPlayer _problemPromptEnglish;
	private AudioStreamPlayer _problemPromptSpanish;

	public override void _Ready()
	{
		RemovableObjectContainer removableObjects = GetNode<RemovableObjectContainer>("ExaminationSection/ExaminationContainer/RemovableObjectContainer");
		removableObjects.Connect("RemovedObject", new Callable(this, "OnObjectRemoved"));
		BodyPartContainer bpContainer = GetNode<BodyPartContainer>("ExaminationSection/ExaminationContainer/BodyPartContainer");
		bpContainer.Connect("BodyPart", new Callable(this, "OnBodyPartIdentified"));
		neutralSprite = GetNode<Sprite2D>(neutralPath);
		discomfortSprite = GetNode<Sprite2D>(discomfortPath);
		joySprite = GetNode<Sprite2D>(joyPath);
		neutralSprite.Visible = false;
		discomfortSprite.Visible = true;
		joySprite.Visible = false;
	}

	private void OnBodyPartIdentified(Sprite2D bodyPart)
	{
		Timer timer = new Timer();
		AddChild(timer);
		_currentBodyPart = bodyPart;
		if(_currentBodyPart == GetNode<Sprite2D>("ExaminationSection/ExaminationContainer/BodyPartContainer/EarSprite"))
		{
			_problemPromptEnglish = GetTree().CurrentScene.GetNode<AudioStreamPlayer>("Audio/EarPainEnglish");
			_problemPromptSpanish = GetTree().CurrentScene.GetNode<AudioStreamPlayer>("Audio/EarPainSpanish");
			_problemPromptEnglish.Play();
			timer.WaitTime = 5.0f;
			timer.OneShot = true;
			timer.Timeout += () => _problemPromptSpanish.Play();
			timer.Start();
		}
		else if(_currentBodyPart == GetNode<Sprite2D>("ExaminationSection/ExaminationContainer/BodyPartContainer/MouthSprite"))
		{
			_problemPromptEnglish = GetTree().CurrentScene.GetNode<AudioStreamPlayer>("Audio/MouthPainEnglish");
			_problemPromptSpanish = GetTree().CurrentScene.GetNode<AudioStreamPlayer>("Audio/MouthPainSpanish");
			_problemPromptEnglish.Play();
			timer.WaitTime = 5.0f;
			timer.OneShot = true;
			timer.Timeout += () => _problemPromptSpanish.Play();
			timer.Start();
		}
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
