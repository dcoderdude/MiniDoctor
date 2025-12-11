using Godot;
using System;

public partial class ReplayButton : Button
{
   [Signal]
   public delegate void ReplayQueEventHandler();
   
   public override void _Ready()
   {
      Pressed += OnReplayButtonPress;
   }
   
   private void OnReplayButtonPress()
   {
      EmitSignal("ReplayQue");
   }
}
