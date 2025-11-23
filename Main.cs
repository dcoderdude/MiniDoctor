using Godot;
using System;

public partial class Main : Node
{
   [Signal]
   public delegate void MiniStoryStartQueEventHandler();
   
   private int currentRound = 0;
   
   private const int maxRound = 3;
   
   public override void _Ready()
   {
      var patient = GetNode<Patient>("Patient");
      patient.Connect("MiniRoundCompleteQue", new Callable(this, "MiniRoundGameCounter"));
   }
   
   private void MiniRoundGameCounter()
   {  
      if(currentRound < maxRound)
      {
         OnMiniStoryNewRound();
         currentRound += 1;
      }
      
      if(currentRound >= maxRound)
      {
         // Stop sending the signal...
      }
   }
   
   private void OnMiniStoryNewRound()
   {
      EmitSignal("MiniStoryStartQue");
   }
}
