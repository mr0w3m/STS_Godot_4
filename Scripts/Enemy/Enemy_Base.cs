using Godot;
using System;
using System.Diagnostics;

public partial class Enemy_Base : Node3D
{
	[Export] public Health health;
    [Export] public Movement _movement;
	[Export] private AudioStream _deathSfx;

    public override void _Ready()
	{
		health.Dead += Die;
	}

	private void Die()
	{
        
        
        if (_deathSfx != null)
		{
            AudioControllerS.instance.PlayClip(_deathSfx, 1, 0.5f, this.GlobalPosition);
		}
		
        health.Dead -= Die;
		Debug.Print("EnemyDied: " + this.Name);
		this.QueueFree();
	}
}
