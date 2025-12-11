using Godot;
using System;

public partial class Controls : ColorRect
{
   public override void _Ready()
   {
      Hide();
      
      var main = GetNode<Main>("..");
      main.Connect("MiniStoryEndOrReplayQue", new Callable(this, "ShowControls"));
      
      var replayButton = GetNode<ReplayButton>("ReplayButton");
      replayButton.Connect("ReplayQue", new Callable(this, "HideControls"));
   }
   
   private void ShowControls()
   {
      Show();
   }
   
   private void HideControls()
   {
      Hide();
   }
}
