using Godot;
using System;
using System.Diagnostics;

public partial class Enemy_Base : Node3D
{
	[Export] public Health health;
    [Export] public Movement _movement;

    public override void _Ready()
	{
		health.Dead += Die;
	}

	private void Die()
	{
		health.Dead -= Die;
		Debug.Print("EnemyDied: " + this.Name);
		this.QueueFree();
	}
}
