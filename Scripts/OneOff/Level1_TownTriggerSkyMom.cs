using Godot;
using System;
using System.Diagnostics;

public partial class Level1_TownTriggerSkyMom : Node
{
	[Export] private CollisionChecker _collisionChecker;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
        //Debug.Print("TOWNTRIGGERSKYMOM");
		_collisionChecker.Collided += CheckForPlayer;

    }

	private void CheckForPlayer(Node3D node)
	{
        //Debug.Print("SKYMOM TOWN TRIGGER Checking");
		if (node.IsInGroup("Player"))
		{
            //trigger logic


            foreach (Node n in node.GetChildren())
            {
                Stars stars = n as Stars;
                if (stars != null)
                {
                    stars.AddStar("star1");
                    this.QueueFree();
                }
            }
        }
	}
}
