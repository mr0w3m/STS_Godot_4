using Godot;
using System;

/// <summary>
/// a way to delete something after a set period of time
/// </summary>
public partial class FreeAfterTime : Node
{
	[Export] private float _timeToFree;

	private bool _queued = false;

	public override void _Process(double delta)
	{
		_timeToFree -= (float)delta;
		if (_timeToFree <= 0 && !_queued)
        {
			_queued = true;
			this.QueueFree();
		}
	}
}
