using Godot;
using System;

public partial class CheckpointManager : Node
{
	public static CheckpointManager instance;

	public Vector3 cachedPosition;

	public override void _Ready()
	{
        instance = this;
		cachedPosition = instance.cachedPosition;
	}

	

	public void SetCheckpoint(Vector3 position)
	{
		cachedPosition = position;
	}
}
