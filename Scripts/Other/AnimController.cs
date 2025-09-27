using Godot;
using System;

public partial class AnimController : AnimatedSprite3D
{
	[Export] private AnimController _otherController;

	public void PlayAnimByString(string s)
	{
		this.Play(s);
		if (_otherController != null)
		{
			_otherController.PlayAnimByString(s);
		}
	}
}
