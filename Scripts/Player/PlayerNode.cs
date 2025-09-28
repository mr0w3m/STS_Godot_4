using Godot;
using System;
using System.Diagnostics;

public partial class PlayerNode : Node3D
{
    [Export] public Movement movement;
    [Export] public Stars stars;
    [Export] public Health health;
    [Export] public AudioStream _walkingClip;
    [Export] public JumpStar jump;

    [Export] private AudioStream _playerHurtSFX;

    [Export] public AnimController animController;


    private bool _gettingDirectionInput;

    public override void _Ready()
    {
        health.HPLost += TakeDamage;
    }

	public override void _Process(double delta)
	{
        ReadMovementInput(delta);
        ReadAttackInput();
        ReadJumpInput();
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
            AudioControllerS.instance.StopLoopingAudio("walk");
        }
        else
        {
            movement.MoveInDirection(targetMovementVector.Normalized());
            _gettingDirectionInput = false;
            //set false at the end of each pass

            animController.PlayAnimByString("WalkAnim");

            AudioControllerS.instance.PlayLoopingAudio(_walkingClip, (float)_walkingClip.GetLength(), true, "walk", 0.3f);
        }

    }

    private void TakeDamage()
    {
        AudioControllerS.instance.PlayClip(_playerHurtSFX);
    }

    private void ReadAttackInput()
    {
        if (Input.IsActionPressed("mainAttack"))
        {
            stars.MainAttack();
        }
    }

    private void ReadJumpInput()
    {
        if (Input.IsActionPressed("interactjump"))
        {
            jump.Jump();
        }
    }
}
