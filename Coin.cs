using Godot;
using System;

public partial class Coin : Area2D
{
	[Signal]
	public delegate void CoinCollectedEventHandler();
	
	public override void _Ready()
	{
		Connect("body_entered", new Callable(this, nameof(OnBodyEntered)));
	}

	private void OnBodyEntered(Node body) 
	{
		if (body is Main_Character)
		{
			EmitSignal(nameof(CoinCollected));
			QueueFree();
		}
	}
	
}
