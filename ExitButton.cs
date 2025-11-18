using Godot;

public partial class ExitButton : Button
{
	public override void _Ready()
	{
		Pressed += OnExitPressed;
	}
	
	private void OnExitPressed()
	{
		GetTree().Quit();
	}
}
