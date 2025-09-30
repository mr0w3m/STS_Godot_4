using Godot;
using System;

public partial class LoopingAudioModule : Node
{
	private AudioControllerS _audioController;

	private AudioStreamPlayer2D _audioPlayer;
	private Timer _timer;

	private double _waitTime;
	private float _volume;
	private AudioStream _clip;
	private bool _randomPitch;

	public string ID;

    public override void _Ready()
    {
		_audioPlayer = new AudioStreamPlayer2D();
		AddChild(_audioPlayer);

		_timer = new Timer();
		_timer.OneShot = true;
		_timer.Timeout += Loop;
		AddChild(_timer);
    }


	public void Init(AudioStream clip, AudioControllerS ac, float timeBetweenLoop, bool randomPitch, string id, float volume)
	{
        _clip = clip;
        _audioController = ac;
        _waitTime = timeBetweenLoop;
        _randomPitch = randomPitch;
        ID = id;
        _volume = volume;
        Loop();
    }

	public void ChangeClip(AudioStream newClip)
	{
		_clip = newClip;
	}

	private void Loop()
	{
		if (_clip == null || _audioController == null)
		{
			return;
		}

		float pitch = _randomPitch ? (float)GD.RandRange(0.9f, 1.05f) : 1f;

		//_audioController.PlayClip(_clip, pitch, _volume);
		_audioPlayer.Stream = _clip;
		_audioPlayer.VolumeDb = Mathf.LinearToDb(_volume);
		_audioPlayer.PitchScale = pitch;
		_audioPlayer.Play();

		_timer.Start(_waitTime);
	}

	public void TerminateModule()
	{
		_timer.Stop();
		_audioPlayer.Stop();
		QueueFree();
	}
}
