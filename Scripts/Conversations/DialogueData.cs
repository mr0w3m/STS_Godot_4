using Godot;
using System;

[GlobalClass]
public partial class DialogueData : Resource
{
	[Export] public string speakingCharacter;
	[Export] public string dialogue;
}
