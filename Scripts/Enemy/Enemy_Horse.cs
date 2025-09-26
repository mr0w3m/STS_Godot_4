using Godot;
using System;

/// <summary>
/// WIP
/// </summary>
public partial class Enemy_Horse : Enemy_Base
{
    [Export] private CollisionChecker _playerCollisionCheck;
    [Export] private CollisionChecker _playerCollisionCheck_Damage;

    [Export] private bool _playerFound;
    private Node3D _playerReference;


    [Export] private int _attackDamage = 1;
    [Export] private float _attackRange = 4f;
    [Export] private float _baseMovementSpeed = 50f;
    [Export] private float _chargeMovementSpeed = 200f;

    [Export] private float _chargeTimeSet = 2f;
    [Export] private float _chargeTimer;

    [Export] private float _chargeMoveTimeSet = 2f;
    [Export] private float _chargeMoveTimer;

    [Export] private float _chargeEndTimeSet = 1f;
    [Export] private float _chargeEndTimer;

    private int _chargeState = 0;

    private Vector3 _cachedChargeDirection;

    private bool _dealtDamage = false;


    public override void _Ready()
    {
        base._Ready();
        _playerCollisionCheck.Collided += FoundPlayer;
        _playerCollisionCheck.ExitedCollider += NoMorePlayer;

        _playerCollisionCheck_Damage.Collided += TryDamage;
    }

    public override void _Process(double delta)
    {
        if (_playerFound && _playerReference != null)
        {
            Vector3 directionToPlayer = _playerReference.GlobalPosition - _movement.GlobalPosition;

            
            if (directionToPlayer.Length() <= _attackRange)
            {
                if (_chargeState == 0)
                {
                    //charge up
                    _movement.StopMovement();
                    if (_chargeTimer > 0)
                    {
                        _chargeTimer -= (float)delta;
                    }
                    else
                    {
                        _chargeState = 1;
                        _chargeTimer = _chargeTimeSet;
                        _cachedChargeDirection = directionToPlayer.Normalized();
                    }
                }
                else if (_chargeState == 1)
                {
                    //run in direction for x seconds
                    _movement.OverrideMovementSpeed(_chargeMovementSpeed);
                    _movement.MoveInDirection(_cachedChargeDirection);

                    if (_chargeMoveTimer >0)
                    {
                        _chargeMoveTimer -= (float)delta;
                    }
                    else
                    {
                        //ChargeEnd
                        _movement.StopMovement();
                        _chargeMoveTimer = _chargeMoveTimeSet;
                        _chargeState = 2;
                    }
                }
                else if (_chargeState == 2)
                {
                    _movement.StopMovement();

                    if (_chargeEndTimer > 0)
                    {
                        _chargeEndTimer -= (float)delta;
                    }
                    else
                    {
                        //ChargeEndEnd
                        _chargeEndTimer = _chargeEndTimeSet;
                        _chargeState = 0;
                        _dealtDamage = false;
                    }
                }
            }
            else
            {
                _movement.OverrideMovementSpeed(_baseMovementSpeed);
                _movement.MoveInDirection(directionToPlayer.Normalized());
            }
        }
    }

    private void ResetChargeState()
    {
        _chargeState = 0;
        _chargeTimer = _chargeTimeSet;
        _chargeMoveTimer = _chargeMoveTimeSet;
        _chargeEndTimer = _chargeEndTimeSet;
        _movement.OverrideMovementSpeed(_baseMovementSpeed);
        _dealtDamage = false;
    }

    private void TryDamage(Node3D body)
    {
        if (body.IsInGroup("Player") && _chargeState == 1 && !_dealtDamage)
        {
            if (_playerReference != null)
            {
                foreach (Node n in _playerReference.GetChildren())
                {
                    Health hp = n as Health;
                    if (hp != null)
                    {
                        hp.LoseHP(_attackDamage);
                        _dealtDamage = true;
                    }
                }
            }
        }
    }

    private void FoundPlayer(Node3D body)
    {
        if (body.IsInGroup("Player"))
        {
            _playerReference = body;
            _playerFound = true;
        }
    }

    private void NoMorePlayer(Node3D body)
    {
        if (body.IsInGroup("Player"))
        {
            _playerFound = false;
            _playerReference = null;
        }
    }
}
