using Godot;
using System;

[GlobalClass]
public partial class ConvoManager : Node
{
    public static ConvoManager instance;

    [Export] private UI_ConvoView _view;

	public event Action ConversationComplete;

    private Resource _convoData;
    private int _currentConversationStep;

    public override void _Ready()
    {
        instance = this;
    }

    public override void _Process(double delta)
    {
        //check for A input
        if (Input.IsActionPressed("interactjump"))
        {
            //if ()
        }
    }

	private void OnConversationComplete()
	{
        if (ConversationComplete != null)
        {
            ConversationComplete.Invoke();
        }
    }

    public void StartConversation(Resource convo)
    {
        _currentConversationStep = 0;
        _view.Show();


        /*
        if (Actor.i != null)
        {
            Actor.i.input.ADown += ProgressDialogue;
        }
        else
        {
            if (_alternateInput != null)
            {
                _alternateInput.ADown += ProgressDialogue;
            }
        }
        */
        ProgressDialogue();
    }

    private void ProgressDialogue()
    {
        /*
        if (_currentConversationStep < (GD.WholeConversationData)_convoData.dialogues.Count)
        {
            //Debug.Log("StepConversation: " + _currentConversationStep);
            //LoadDialogue(_currentConversation.dialogues[_currentConversationStep]);
            _currentConversationStep++;

        }
        else
        {
            CloseDialogue();
            //Debug.Log("current conversation step is less than the dialogues left");
        }]
        */
    }

    private void CloseDialogue()
    {
        //Actor.i.input.ADown -= ProgressDialogue;
        _view.Hide();
        _currentConversationStep = 0;
        _convoData = null;
        OnConversationComplete();
    }

    private void LoadDialogue(DialogueData data)
    {
        _view.SetTitleText(data.speakingCharacter);
        _view.SetDialogueText(data.dialogue);
    }
}
