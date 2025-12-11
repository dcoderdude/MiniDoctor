using Godot;
using System;

public partial class Main : Node
{
   [Signal]
   public delegate void MiniStoryStartQueEventHandler();
   
   [Signal]
   public delegate void MiniStoryEndOrReplayQueEventHandler();
   
   private int currentRound = 0;
   
   private const int maxRound = 1;
   
   public override void _Ready()
   {
      var patient = GetNode<Patient>("Patient");
      patient.Connect("MiniRoundCompleteQue", new Callable(this, "MiniRoundGameCounter"));
      
      var replay = GetNode<ReplayButton>("Controls/ReplayButton");
      replay.Connect("ReplayQue", new Callable(this, "OnReplayMiniStoryRound"));
   }
   
   private void MiniRoundGameCounter()
   {  
      if(currentRound <= maxRound)
      {
         OnMiniStoryNewRound();
         currentRound += 1;
      } 
      else
      {
         OnMiniStoryEndRound();
      }
   }
   
   private void OnReplayMiniStoryRound()
   {
      currentRound = 0;
      OnMiniStoryNewRound();
   }
   
   private void OnMiniStoryNewRound()
   {
      EmitSignal("MiniStoryStartQue");
   }
   
   private void OnMiniStoryEndRound()
   {
      EmitSignal("MiniStoryEndOrReplayQue");
   }
}
