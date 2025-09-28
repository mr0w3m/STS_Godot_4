using Godot;
using System;

public partial class NewStarPowerListener : Control
{
	[Export] private Stars _stars;

	private float _visibleTimer;

	public override void _Ready()
	{
		this.Visible = false;
		_stars.newStarPower += GotNewStarPower;
	}

	private void GotNewStarPower()
	{
		this.Visible = true;
		_visibleTimer = 2f;
	}

    public override void _Process(double delta)
    {
        if (_visibleTimer > 0)
		{
			this.Visible = true;
			_visibleTimer -= (float)delta;
		}
		else
		{
			this.Visible = false;
		}
    }
}
