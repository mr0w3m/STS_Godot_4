using Godot;
using System;
using System.Diagnostics;

public partial class PlayerNode : Node3D
{
    [Export] public Movement movement;
    [Export] public Stars stars;

    [Export] public AnimController animController;

    private bool _gettingDirectionInput;

	public override void _Process(double delta)
	{
        ReadMovementInput(delta);
        ReadAttackInput();
    }

    private void ReadMovementInput(double delta)
    {
        Vector3 targetMovementVector = Vector3.Zero;
        if (Input.IsActionPressed("move_left"))
        {
            //movement.MoveInDirection(Vector3.Right);
            targetMovementVector += Vector3.Right;
            _gettingDirectionInput = true;
        }

        if (Input.IsActionPressed("move_right"))
        {
            //movement.MoveInDirection(Vector3.Left);
            targetMovementVector += Vector3.Left;
            _gettingDirectionInput = true;
        }

        if (Input.IsActionPressed("move_forward"))
        {
            //movement.MoveInDirection(Vector3.Back);
            targetMovementVector += Vector3.Back;
            _gettingDirectionInput = true;
        }

        if (Input.IsActionPressed("move_backward"))
        {
            //movement.MoveInDirection(Vector3.Forward);
            targetMovementVector += Vector3.Forward;
            _gettingDirectionInput = true;
        }

        if (!_gettingDirectionInput)
        {
            //if there's no input this frame, stop movement.
            movement.StopMovement();
            animController.PlayAnimByString("IdleAnim");
        }
        else
        {
            movement.MoveInDirection(targetMovementVector.Normalized());
            _gettingDirectionInput = false;
            //set false at the end of each pass

            animController.PlayAnimByString("WalkAnim");
        }

    }

    private void ReadAttackInput()
    {
        if (Input.IsActionPressed("mainAttack"))
        {
            stars.MainAttack();
        }
    }
}
