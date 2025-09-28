using Godot;
using System;
using System.Diagnostics;

public partial class JumpStar : Node
{
	[Export] private Movement _movement;
	[Export] private CollisionChecker _groundedCheck;
    [Export] private float _jumpSpeed;
    private bool _jumpHeld;

	private float _maxJumpTime = 0.6f;
	private float _jumpTimer = 0;
	private bool _grounded = false;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		_groundedCheck.Collided += Grounded;
		_groundedCheck.ExitedCollider += UnGrounded;
    }

	private void Grounded(Node3D n)
	{
		_grounded = true;
		_jumpTimer = _maxJumpTime;
        Debug.Print("Grounded");
    }

	private void UnGrounded(Node3D n)
	{
		_grounded = false;
		Debug.Print("Ungrounded");
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _PhysicsProcess(double delta)
	{
        if (Input.IsActionPressed("interactjump"))
		{
            _movement.AddToVelocity(Vector3.Up * _jumpSpeed * (float)delta);
        }
		
		/*
		if (_jumpHeld && _jumpTimer > 0)
		{
            Debug.Print("JumpTimer: " + _jumpTimer);
            _movement.JumpMovement(Vector3.Up * _jumpSpeed);
			_jumpTimer -= (float)delta;
        }
		else
		{
			//no jump
			_movement.JumpMovement(Vector3.Zero);
            Debug.Print("JumpEnd");
        }
		*/
	}

	public void Jump()
	{
		Debug.Print("Jump triggered");
        //_jumpHeld = true;
		//_grounded = false;
    }
}
