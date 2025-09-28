using Godot;
using System;

public partial class PlayerDeathUI : Control
{
	[Export] private Health _health;

    public override void _Ready()
    {
        base._Ready();
        _health.Dead += ShowDeathScreen;
        _health.Revive += HideDeathScreen;
    }

    private void ShowDeathScreen()
    {
        this.Visible = true;
    }
    private void HideDeathScreen()
    {
        this.Visible = false;
    }
}
