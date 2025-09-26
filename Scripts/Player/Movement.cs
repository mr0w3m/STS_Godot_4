using Godot;
using System;

public partial class Movement : RigidBody3D
{
    [Export]
    private float _moveSpeed;

    private Direction _currentDirection;

    private Vector3 _targetMovementVector;

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
    }
}
