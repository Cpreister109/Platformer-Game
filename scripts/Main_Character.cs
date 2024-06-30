using Godot;
using System;

public partial class Main_Character : CharacterBody2D
{
	public const float Speed = 180f;
	public const float JumpVelocity = -350.0f;
	public CollisionShape2D _collisionShape2D;
	private Vector2 _startPosition;
	public AnimatedSprite2D _animatedSprite2D;
	private Label _playerLabel;
	public float gravity = ProjectSettings.GetSetting("physics/2d/default_gravity").AsSingle();
	
	public override void _Ready() 
	{
		_collisionShape2D = GetNode<CollisionShape2D>("Player_Collision");
		_animatedSprite2D = GetNode<AnimatedSprite2D>("Player_Sprite");
		_playerLabel = GetNode<Label>("player_label");
		_startPosition = Position + new Vector2(0, -20);

		foreach (Coin coin in GetTree().GetNodesInGroup("Coins"))
		{
			coin.Connect("CoinCollected", new Callable(this, nameof(OnCoinCollected)));
		}
	}

	public void DisabledCollision()
	{
		_collisionShape2D.QueueFree();
	}
	
	private void OnCoinCollected()
	{
		GD.Print("+1");
	}

	public void UpdateCoinLabel(int score)
	{
		_playerLabel.Text = score.ToString();
	}

	public override void _PhysicsProcess(double delta)
	{
		Vector2 velocity = Velocity;

		if (!IsOnFloor())
		{
			velocity.Y += gravity * (float)delta;
		}

		if (Input.IsActionJustPressed("jump") && IsOnFloor())
			{
			velocity.Y = JumpVelocity;
			_animatedSprite2D.Play("jump");
			}

		Vector2 direction = Input.GetVector("move_left", "move_right", "jump", "drop");
		if (direction != Vector2.Zero)
		{

			velocity.X = direction.X * Speed;

			if (direction.X != 0)
			{
				_animatedSprite2D.FlipH = direction.X < 0;
			}
		
		}
		else
		{
			velocity.X = Mathf.MoveToward(Velocity.X, 0, Speed);

		}

		if (IsOnFloor())
		{
			if (direction.X != 0)
			{
				_animatedSprite2D.Play("run");
			}
			else
			{
				_animatedSprite2D.Play("idle");
			}
		}
		else
		{
			_animatedSprite2D.Play("jump");
		}

		Velocity = velocity;
		MoveAndSlide();
	}

	public void Restart() 
	{
		GetTree().ReloadCurrentScene();
		
		Position = _startPosition;
		Velocity = Vector2.Zero;
		GD.Print("YOU DIED!");
	}
}
