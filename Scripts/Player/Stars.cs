using Godot;
using System;
using System.Collections.Generic;
using System.Diagnostics;

public partial class Stars : Node
{
	//[Export] private List<string> _starStrings;
	[Export] private Node3D _projectileLaunchPos;
	[Export] private PackedScene _starProjectilePrefab; // replace this with from the data we load from a string through some database
	private string _selectedStarAbility = "mini";

	private List<string> _starAbilities = new();

	[Export] private AudioStream _starClip;

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
		_starAbilities.Add(s);
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
		AudioControllerS.instance.PlayClip(_starClip, (float)GD.RandRange(0.8f, 1.1f), 0.1f);

		Node3D projectileNode = (Node3D)_starProjectilePrefab.Instantiate();//spawn class
		AddChild(projectileNode);//add to current scene
		projectileNode.GlobalTransform = _projectileLaunchPos.GlobalTransform;
        StarProjectile_Mini starProj = (StarProjectile_Mini)projectileNode;
		if (starProj != null)
		{
			starProj.Initialize();
		}
	}
}
