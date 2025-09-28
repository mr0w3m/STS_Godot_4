using Godot;
using System;

/// <summary>
/// The player has 2 sprites one facing left one facing right, this script flips between the two to show the correct direction the player is animating
/// The left? sprite is just negative on the x axis to mirror the other side, ideally this will work with our animation solution
/// </summary>
public partial class DirectionFlipper : Node3D
{
    [Export]
    private Movement _movement;




    private Vector3 _leftRotation = new Vector3(0, Mathf.DegToRad(0), 0);
    private Vector3 _rightRotation = new Vector3(0, Mathf.DegToRad(180), 0);

    private Quaternion _targetRotation;
    private float _rotateSpriteSpeed = 5f;

    public override void _Process(double delta)
    {
        CheckYawDirection(delta);
    }
    
    private void CheckYawDirection(double delta)
    {
        if (_movement.currentDirection == Direction.right)
        {
            _targetRotation = Quaternion.FromEuler(_leftRotation);
        }
        else
        {
            _targetRotation = Quaternion.FromEuler(_rightRotation);
        }
        Quaternion newRotation = this.Quaternion.Slerp(_targetRotation, _rotateSpriteSpeed * (float)delta);
        
        this.Quaternion = newRotation;
    }
}
