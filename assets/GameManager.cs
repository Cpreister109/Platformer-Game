using Godot;
using System;

public partial class GameManager : Node
{
    private int _playerScore;

    public void add_point()
    {
        _playerScore = Convert.ToInt32(GetNode<Label>("player_label"));

        
        
        
    }
}
