using Godot;
using System;
using System.Diagnostics;

public partial class Enemy_Imp : Enemy_Base
{
	[Export] private CollisionChecker _playerCheck;


	[Export] private bool _playerFound;
	private Node3D playerReference;
	[Export]private float _attackRange = 0.5f;

    [Export] private int _attackDamage;
    [Export] private float _timeBetweenAttacks;
	private float _attackTimer;

    public override void _Ready()
    {
        base._Ready();
		_playerCheck.Collided += FoundPlayer;
		_playerCheck.ExitedCollider += NoMorePlayer;
    }

	//process enemy inputs
	public override void _Process(double delta)
	{
		//check for player locally through collision check
		//if player is local, move towards player by getting a vector between the two and normalizing
		//we can figure out animations after this for attacking when we get close enough
		if (_playerFound && playerReference != null)
		{
			Vector3 directionToPlayer = (playerReference.GlobalPosition - _movement.GlobalPosition);
			if (directionToPlayer.Length() <= _attackRange)
			{
				//Debug.Print(this.Name + " is in attack range, and attacking player");
				_movement.StopMovement();
				ProcessAttack(delta);

            }
			else
			{
				//move towards player
				//Debug.Print("MovingTowardsPlayer");
				_movement.MoveInDirection(directionToPlayer.Normalized());
				_attackTimer = _timeBetweenAttacks;
			}
		}
		else
		{
			_movement.StopMovement();
		}
	}
	
	private void ProcessAttack(double delta)
	{
		if (_attackTimer > 0)
		{
			_attackTimer -= (float)delta;
		}
		else
		{
			//attack
			Attack();
			_attackTimer = _timeBetweenAttacks;
		}
	}

	private void Attack()
	{
		//Debug.Print("Attacking Player!");
		if (playerReference != null)
		{
			foreach(Node n in playerReference.GetChildren())
			{
				Health hp = n as Health;
				if (hp != null)
				{
					hp.LoseHP(_attackDamage);
				}
			}
		}
	}

	private void FoundPlayer(Node3D body)
	{
		if (body.IsInGroup("Player"))
		{
            playerReference = body;
            _playerFound = true;
        }
    }

	private void NoMorePlayer(Node3D body)
	{
        if (body.IsInGroup("Player"))
		{
            _playerFound = false;
            playerReference = null;
        }
    }
}
