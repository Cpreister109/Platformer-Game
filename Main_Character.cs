using Godot;
using System;

public partial class Main_Character : CharacterBody2D
{
	public const float Speed = 180f;
	public const float JumpVelocity = -350.0f;

	private Vector2 _startPosition;

	// Get the gravity from the project settings to be synced with RigidBody nodes.
	public float gravity = ProjectSettings.GetSetting("physics/2d/default_gravity").AsSingle();
	
	public override void _Ready() 
	{
		_startPosition = Position + new Vector2(0, -20);

		foreach (Coin coin in GetTree().GetNodesInGroup("Coins"))
		{
			coin.Connect("CoinCollected", new Callable(this, nameof(OnCoinCollected)));
		}
	}
	
	private void OnCoinCollected()
	{
		GD.Print("+1");
	}

	public override void _PhysicsProcess(double delta)
	{
		Vector2 velocity = Velocity;

		// Add the gravity.
		if (!IsOnFloor())
			velocity.Y += gravity * (float)delta;

		// Handle Jump.
		if (Input.IsActionJustPressed("ui_accept") && IsOnFloor())
			velocity.Y = JumpVelocity;

		// Get the input direction and handle the movement/deceleration.
		// As good practice, you should replace UI actions with custom gameplay actions.
		Vector2 direction = Input.GetVector("ui_left", "ui_right", "ui_up", "ui_down");
		if (direction != Vector2.Zero)
		{
			velocity.X = direction.X * Speed;
		}
		else
		{
			velocity.X = Mathf.MoveToward(Velocity.X, 0, Speed);
		}

		Velocity = velocity;
		MoveAndSlide();
	}

	public void Restart() 
	{
		Position = _startPosition;
		Velocity = Vector2.Zero;
	}
}
