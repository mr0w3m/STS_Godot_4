using Godot;
using System;
using System.Collections.Generic;
using System.Diagnostics;

public partial class Stars : Node
{
	//[Export] private List<string> _starStrings;
	[Export] private Node3D _projectileLaunchPos;
	[Export] private PackedScene _starProjectilePrefab; // replace this with from the data we load from a string through some database
    [Export] private PackedScene _starFriend_star1;
    [Export] private PackedScene _starFriend_star2;
    [Export] private PackedScene _starFriend_star3;
    [Export] private Node3D _starFriendPosition_star1;
    [Export] private Node3D _starFriendPosition_star2;
    [Export] private Node3D _starFriendPosition_star3;

    private string _selectedStarAbility = "mini";

	private List<string> _starAbilities = new();

	[Export] private AudioStream _starClip;

	public event Action newStarPower;

	private void OnNewStarPower()
	{
		if (newStarPower != null)
		{
			newStarPower.Invoke();
		}
	}

    [Export] private float _timeBetweenAttacks = 0.2f;
    private float _attackTimer;
	private bool _canAttack;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
	{

	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		if (_attackTimer > 0)
		{
			_attackTimer -= (float)delta;
		}
		else
		{
			_canAttack = true;
		}
	}

	public bool HasStar(string s)
	{
		return _starAbilities.Contains(s);
	}

	public void AddStar(string s)
	{
		Debug.Print("Added Star Ability: " + s);
		OnNewStarPower();

        _starAbilities.Add(s);

		SpawnNewStarFriend(s);

		if (s == "star2")
		{
			_timeBetweenAttacks = _timeBetweenAttacks / 2;
		}
	}

	public void MainAttack()
	{
		if (!_canAttack)
		{
			return;
		}
		//trigger ability that is currently selected
		//
		if (_selectedStarAbility == "mini")
		{
			FireMini();
			_attackTimer = _timeBetweenAttacks;
			_canAttack = false;
		}
	}

	private void FireMini()
	{
		AudioControllerS.instance.PlayClip(_starClip, (float)GD.RandRange(0.8f, 1.1f), 0.3f);

		Node3D projectileNode = (Node3D)_starProjectilePrefab.Instantiate();//spawn class
		AddChild(projectileNode);//add to current scene
		projectileNode.GlobalTransform = _projectileLaunchPos.GlobalTransform;
        StarProjectile_Mini starProj = (StarProjectile_Mini)projectileNode;
		if (starProj != null)
		{
			starProj.Initialize();
		}
	}

	private void SpawnNewStarFriend(string s)
	{
		if (s == "star1")
		{
			Node f = _starFriend_star1.Instantiate();
			_starFriendPosition_star1.AddChild(f);
		}
		else if (s == "star2")
		{
            Node f = _starFriend_star2.Instantiate();
            _starFriendPosition_star2.AddChild(f);
        }
		else if (s == "star3")
		{
            Node f = _starFriend_star3.Instantiate();
            _starFriendPosition_star3.AddChild(f);
        }
	}
}
