using Godot;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

public partial class UI_Health : Node
{
	[Export] private Health _health;
    [Export] private PackedScene _uiHPPrefab;

    private List<Node> _uiHPs = new List<Node>();

    public override void _Ready()
    {
        _health.HPChanged += RefreshUI;
        RefreshUI();
    }

    private void RefreshUI()
    {
        Debug.Print("RefreshingUI");
        CleanList();

        for (int i = 0; i < _health.currentHP; i++)
        {
            Node uiHPNode = (Node)_uiHPPrefab.Instantiate();//spawn class
            //AddChild(uiHPNode);//add to current scene
            this.AddChild(uiHPNode); //make child of this class
            _uiHPs.Add(uiHPNode);
        }
    }

    private void CleanList()
    {
        if (_uiHPs.Count > 0)
        {
            _uiHPs.ForEach(ui => ui.QueueFree());
        }
        _uiHPs.Clear();
    }
}
