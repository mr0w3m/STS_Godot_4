using Godot;
using System;

public partial class UI_ConvoView : Control
{
	[Export] private RichTextLabel _speakerText;
	[Export] private RichTextLabel _dialogueText;
	
	public void SetTitleText(string text)
	{
		_speakerText.Text = text;
	}

	public void SetDialogueText(string text)
	{
		_dialogueText.Text = text;
	}

	public void _Show()
	{
		this.Visible = true;
	}

	public void _Hide()
	{
		this.Visible = false;
	}
}
