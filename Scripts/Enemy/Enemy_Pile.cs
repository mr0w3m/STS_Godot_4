using Godot;
using System;

public partial class Enemy_Pile : Enemy_Base
{
	[Export] private CollisionChecker _playerCollisionCheck;

    [Export] private bool _playerFound;
    private Node3D _playerReference;
    [Export] private float _spawnAttackRange = 3f;

    [Export] private PackedScene _pileEnemySinglePrefab;
    [Export] private float _timeBetweenEnemySpawn;
    [Export] private float _spawnTimer;

    public override void _Ready()
	{
		base._Ready(); 
        _playerCollisionCheck.Collided += FoundPlayer;
        _playerCollisionCheck.ExitedCollider += NoMorePlayer;
    }

	public override void _Process(double delta)
	{
        //if we see player, stay away from them. but also, update our spawn timer
        if (_playerFound && _playerReference != null)
        {
            Vector3 directionToPlayer = _playerReference.GlobalPosition - _movement.GlobalPosition;


            if (directionToPlayer.Length() >= _spawnAttackRange)
            {
                PlayerCloseState(delta);
                _movement.StopMovement();
            }
            else
            {
                _movement.MoveInDirection(-directionToPlayer.Normalized());
            }
        }
    }

    private void PlayerCloseState(double delta)
    {
        if (_spawnTimer > 0)
        {
            _spawnTimer -= (float)delta;
        }
        else
        {
            SpawnEnemy();
            _spawnTimer = _timeBetweenEnemySpawn;
        }
    }

    private void SpawnEnemy()
    {
        //pick a location around
        RandomNumberGenerator random = new RandomNumberGenerator();
        Vector3 location = new Vector3(random.RandfRange(0.5f, 2), 0, random.RandfRange(0.5f, 2));
        location = location + _movement.GlobalPosition;
        Node3D n = (Node3D)_pileEnemySinglePrefab.Instantiate();
        AddChild(n);
        Enemy_Base enemy = n as Enemy_Base;
        if (enemy != null)
        {
            enemy._movement.GlobalPosition = location;
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
