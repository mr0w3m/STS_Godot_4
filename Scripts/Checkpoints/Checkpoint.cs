using Godot;
using System;

public partial class Checkpoint : Node
{
    [Export] private CollisionChecker _collisionChecker;
    [Export] private Node3D _targetPosition;
    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        
        _collisionChecker.Collided += CheckForPlayer;

    }

    private void CheckForPlayer(Node3D node)
    {
        //Debug.Print("SKYMOM TOWN TRIGGER Checking");
        if (node.IsInGroup("Player"))
        {
            //trigger logic

            //schedule a position to respawn at
            CheckpointManager.instance.SetCheckpoint(_targetPosition.GlobalPosition);

            //destroy this
            this.QueueFree();
        }
    }
}
