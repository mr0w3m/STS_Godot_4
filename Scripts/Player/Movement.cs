using Godot;
using System;
using System.Diagnostics;

public partial class Movement : RigidBody3D
{
    [Export]
    private float _moveSpeed;
    

    private Direction _currentDirection;

    private Vector3 _targetMovementVector;
    private Vector3 _targetJumpVector;

    public Direction currentDirection
    {
        get { return _currentDirection; }
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
        _targetMovementVector = Vector3.Zero;
    }

    public void OverrideMovementSpeed(float overrideValue)
    {
        _moveSpeed = overrideValue;
    }

    public override void _PhysicsProcess(double delta)
    {
        base._PhysicsProcess(delta);

        this.SetAxisVelocity(_targetMovementVector * _moveSpeed * (float)delta);
        //Debug.Print(_targetMovementVector.ToString());
        
    }
    public void AddToVelocity(Vector3 direction)
    {
        _targetMovementVector += direction;
    }

    public void TeleportCharacter(Vector3 position)
    {
        this.GlobalPosition = position;
    }
}
