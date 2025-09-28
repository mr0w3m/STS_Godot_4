using Godot;
using System;
using System.Diagnostics;

public partial class NewMovement : RigidBody3D
{
    [Export]
    private float _moveSpeed;


    private Direction _currentDirection;


    [Export] private Vector3 _targetMovementVector;
    private Vector3 _targetJumpVector;

    public Direction currentDirection
    {
        get { return _currentDirection; }
    }

    public override void _Ready()
    {
        LinearDamp = 10f;
    }

    public void MoveInDirection(Vector3 direction)
    {
        _targetMovementVector = direction.Normalized();


        if (direction.X > 0.1f)
        {
            _currentDirection = Direction.left;
        }
        else if (direction.X < -0.1f)
        {
            _currentDirection = Direction.right;
        }
    }

    public void StopMovement()
    {
        LinearVelocity = Vector3.Zero;
    }

    public void OverrideMovementSpeed(float overrideValue)
    {
        _moveSpeed = overrideValue;
    }

    public override void _PhysicsProcess(double delta)
    {
        base._PhysicsProcess(delta);

        if (_targetMovementVector.LengthSquared() > 0.001f)
        {
            this.AddConstantCentralForce(_targetMovementVector * _moveSpeed);
        }
        else
        {
            GD.Print($"[DEBUG] **STOP PHASE**. LinearVelocity: {LinearVelocity.Length():0.00}");
        }
        
        //Debug.Print(_targetMovementVector.ToString());
        if (LinearVelocity.Length() > 3)
        {
            LinearVelocity = LinearVelocity.Normalized() * 3;
        }
    }
    public void AddToVelocity(Vector3 direction)
    {
        LinearVelocity += direction;
    }

    public void TeleportCharacter(Vector3 position)
    {
        this.GlobalPosition = position;
    }
}

