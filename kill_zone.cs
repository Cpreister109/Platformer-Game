using Godot;
using System;
using System.Runtime.CompilerServices;

public partial class kill_zone : Area2D
{

    private Timer _restartTimer;

    public override void _Ready()
    {
        _restartTimer = GetNode<Timer>("kill_timer");
        _restartTimer.Connect("timeout", new Callable(this, nameof(OnRestartTimeout)));
        Connect("body_entered", new Callable(this, nameof(OnBodyEntered)));
    }

    private void OnBodyEntered(Node body) 
    {
        if (body is Main_Character mainCharacter)
        {
            Engine.TimeScale = 0.5f;
            _restartTimer.Start();
        }
    }

    private void OnRestartTimeout()
    {
        Engine.TimeScale = 1.0f;
        GetTree().ReloadCurrentScene();
    }

}
