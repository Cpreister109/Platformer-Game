using Godot;
using System;

public partial class Coin : Area2D
{
	[Signal]
	public delegate void CoinCollectedEventHandler();
	private CharacterBody2D _mainCharacter;
	private int score = 0;
	
	public override void _Ready()
	{
		Connect("body_entered", new Callable(this, nameof(OnBodyEntered)));
		_mainCharacter = GetNode<CharacterBody2D>("Main_Character");
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
