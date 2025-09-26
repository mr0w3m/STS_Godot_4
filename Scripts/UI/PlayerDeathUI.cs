using Godot;
using System;

public partial class PlayerDeathUI : Control
{
	[Export] private Health _health;

    public override void _Ready()
    {
        base._Ready();
        _health.Dead += ShowDeathScreen;
    }

    private void ShowDeathScreen()
    {
        this.Visible = true;
    }
}
