using Godot;
using System;

public partial class GainNewStarPower : Node
{
    [Export] private CollisionChecker _collisionChecker;
    [Export] private string _starIDToGive;

    public override void _Ready()
    {
        _collisionChecker.Collided += CheckForPlayer;

    }

    private void CheckForPlayer(Node3D node)
    {
        if (node.IsInGroup("Player"))
        {

            foreach (Node n in node.GetChildren())
            {
                Stars stars = n as Stars;
                if (stars != null)
                {
                    stars.AddStar(_starIDToGive);
                    this.QueueFree();
                }
            }
        }
    }
}
