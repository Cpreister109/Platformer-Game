using Godot;
using System;
using System.Dynamic;

public partial class green_enemy : Node2D
{
    [Export]
    public float Speed = 100f;

    private Vector2 _direction = new Vector2(1, 0);
    private RayCast2D _rayCastLeft;
    private RayCast2D _rayCastRight;

    public override void _Ready()
    {
        _rayCastLeft = GetNode<RayCast2D>("RayCast_Left");
        _rayCastRight = GetNode<RayCast2D>("RayCast_Right");
        _rayCastLeft.Enabled = true;
        _rayCastRight.Enabled = true;
    }

    public override void _Process(double delta)
    {
        Position += _direction * Speed * (float)delta;

        if ((_direction.X < 1 && _rayCastLeft.IsColliding()) || (_direction.X > 0 && _rayCastRight.IsColliding()))
        {
            _direction = -_direction;
            Scale = new Vector2(-Scale.X, Scale.Y);
            Position += _direction * Speed * 0.05f;
        }
    }
}