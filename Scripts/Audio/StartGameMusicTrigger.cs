using Godot;
using System;

public partial class StartGameMusicTrigger : Node
{
    [Export] private CollisionChecker _collisionChecker;
    [Export] private AudioStream _musicTrackToPlay;

    public override void _Ready()
    {
        _collisionChecker.Collided += CheckForPlayer;

    }

    private void CheckForPlayer(Node3D node)
    {
        if (node.IsInGroup("Player"))
        {
            if (AudioControllerS.instance.IsTrackPlaying("music"))
            {
                AudioControllerS.instance.StopLoopingAudio("music");
            }
            AudioControllerS.instance.PlayLoopingAudio(_musicTrackToPlay, (float)_musicTrackToPlay.GetLength(), false, "music", 1);
        }
    }
}
