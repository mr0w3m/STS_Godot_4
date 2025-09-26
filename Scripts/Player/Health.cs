using Godot;
using System;

public partial class Health : Node
{
	[Export] private int _startingHP;

	private bool _dead;
	public bool dead
	{
		get { return _dead; }
	}

	[Export] private int _currentHP;
	public int currentHP
	{
		get { return _currentHP; }
	}

	public event Action HPChanged;
	public event Action Dead;

	private void OnHPChanged()
	{
		if (HPChanged != null)
		{
			HPChanged.Invoke();
		}
	}

	private void OnDead()
	{
		if (Dead != null)
		{
			Dead.Invoke();
		}
	}

	public override void _Ready()
	{
		_currentHP = _startingHP;
	}


	public void GainHP(int amt)
	{
		_currentHP += amt;
		OnHPChanged();
	}

	public void LoseHP(int amt)
	{
		_currentHP -= amt;
		OnHPChanged();

		if (_currentHP <= 0)
		{
			_currentHP = 0;

			_dead = true;
			OnDead();
        }
    }
}
