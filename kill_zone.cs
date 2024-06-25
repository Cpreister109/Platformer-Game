using Godot;
using System;
using System.Runtime.CompilerServices;

public partial class kill_zone : Area2D
{
    public override void _Ready()
    {
        Connect("body_entered", new Callable(this, nameof(OnBodyEntered)));
    }

    private void OnBodyEntered(Node body) 
    {
        if (body is Main_Character mainCharacter)
        {
            mainCharacter.Restart();
        }
    }
}
