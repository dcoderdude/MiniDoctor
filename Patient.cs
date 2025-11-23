using Godot;
using System;

public partial class Patient : Node2D
{
   [Signal]
   public delegate void AudioQueEventHandler();
   
   [Signal]
   public delegate void MiniRoundCompleteQueEventHandler();
   
   [Export] private NodePath neutralPath;
   [Export] private NodePath discomfortPath;
   [Export] private NodePath joyPath;
   
   private Sprite2D neutralSprite;
   private Sprite2D discomfortSprite;
   private Sprite2D joySprite;
   private Sprite2D _currentBodyPart;
   
   private AudioStreamPlayer _problemPromptEnglish;
   private AudioStreamPlayer _problemPromptSpanish;
   private AudioStreamPlayer _reliefEnglish;
   private AudioStreamPlayer _reliefSpanish;
   
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
      _currentBodyPart = bodyPart;
      if(_currentBodyPart == GetNode<Sprite2D>("ExaminationSection/ExaminationContainer/BodyPartContainer/EarSprite"))
      {
         _problemPromptEnglish = GetTree().CurrentScene.GetNode<AudioStreamPlayer>("Audio/EarPainEnglish");
         _problemPromptSpanish = GetTree().CurrentScene.GetNode<AudioStreamPlayer>("Audio/EarPainSpanish");
         
         PlayBilingualDiscomfortVoiceLines();
      }
      else if(_currentBodyPart == GetNode<Sprite2D>("ExaminationSection/ExaminationContainer/BodyPartContainer/MouthSprite"))
      {
         _problemPromptEnglish = GetTree().CurrentScene.GetNode<AudioStreamPlayer>("Audio/MouthPainEnglish");
         _problemPromptSpanish = GetTree().CurrentScene.GetNode<AudioStreamPlayer>("Audio/MouthPainSpanish");
         
         PlayBilingualDiscomfortVoiceLines();
      }
      else if(_currentBodyPart == GetNode<Sprite2D>("ExaminationSection/ExaminationContainer/BodyPartContainer/EyeSprite"))
      {
         _problemPromptEnglish = GetTree().CurrentScene.GetNode<AudioStreamPlayer>("Audio/EyePainEnglish");
         _problemPromptSpanish = GetTree().CurrentScene.GetNode<AudioStreamPlayer>("Audio/EyePainSpanish");
         
         PlayBilingualDiscomfortVoiceLines();
      }
   }
   
   private void PlayBilingualDiscomfortVoiceLines()
   {
      Timer discomfortTimer = new Timer();
      AddChild(discomfortTimer);
      
      _problemPromptEnglish.Play();
      discomfortTimer.WaitTime = 5.0f;
      discomfortTimer.OneShot = true;
      discomfortTimer.Timeout += () =>
      {
         if (!_problemPromptSpanish.IsConnected("finished", new Callable(this, "OnBilingualVoiceLinesComplete")))
         {
            _problemPromptSpanish.Connect("finished", new Callable(this, "OnBilingualVoiceLinesComplete"));
         }
         _problemPromptSpanish.Play();
      };
      discomfortTimer.Start();
   }
   
   private void OnObjectRemoved()
   {
      neutralSprite.Visible = false;
      discomfortSprite.Visible = false;
      joySprite.Visible = true;
      
      _reliefEnglish = GetTree().CurrentScene.GetNode<AudioStreamPlayer>("Audio/ReliefEnglish");
      _reliefSpanish = GetTree().CurrentScene.GetNode<AudioStreamPlayer>("Audio/ReliefSpanish");
      
      Timer toNeutraltimer = new Timer();
      AddChild(toNeutraltimer);
      
      toNeutraltimer.WaitTime = 1.0f;
      toNeutraltimer.OneShot = true;
      toNeutraltimer.Timeout += () =>
      {
         joySprite.Visible = false;
         neutralSprite.Visible = true;
         
         Timer reliefTimer = new Timer();
         AddChild(reliefTimer);
         
         reliefTimer.OneShot = true;
         reliefTimer.WaitTime = 0.5f;
         reliefTimer.Timeout += () =>
         {
            _reliefEnglish?.Play();
            
            Timer reliefSpanishTimer = new Timer();
            AddChild(reliefSpanishTimer);
            
            reliefSpanishTimer.OneShot = true;
            reliefSpanishTimer.WaitTime = 2.5f;
            reliefSpanishTimer.Timeout += () =>
            {
               if(!_reliefSpanish.IsConnected("finished", new Callable(this, "OnMiniStoryRoundComplete")))
               {
                  _reliefSpanish.Connect("finished", new Callable(this, "OnMiniStoryRoundComplete"));
               }
               _reliefSpanish?.Play();
               reliefSpanishTimer.QueueFree();
            };
            reliefSpanishTimer.Start();
         };
         reliefTimer.Start();
         toNeutraltimer.QueueFree();
      };
      toNeutraltimer.Start();
   }
   
   private void OnBilingualVoiceLinesComplete()
   {
      EmitSignal("AudioQue");
   }
   
   private void OnMiniStoryRoundComplete()
   {
      EmitSignal("MiniRoundCompleteQue");
   }
}
