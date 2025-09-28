using Godot;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

[GlobalClass]
public partial class AudioControllerS : Node
{
    public static AudioControllerS instance;

    private readonly List<AudioStreamPlayer> _audioSources = new();

    [Export] public PackedScene loopingAudioModule;
    private readonly List<LoopingAudioModule> _loopingAudios = new();


	public override void _Ready()
	{
        instance = this;

        for (int i =0; i < 10; i++)
        {
            AudioStreamPlayer player = new AudioStreamPlayer();
            //player.Finished += () => player.Playing = false;
            _audioSources.Add(player);
            AddChild(player);
            //player.Visible = false;
        }
	}

    public void PlayClip(AudioStream clip, float pitch = 1, float volume = 1, Vector3 pos = default)
    {
        AudioStreamPlayer tempSource = _audioSources.FirstOrDefault(p => !p.Playing);

        

        if (tempSource == null)
        {
            tempSource = _audioSources[0];
        }
        tempSource.Stream = clip;
        tempSource.VolumeDb = Mathf.LinearToDb(volume);
        tempSource.PitchScale = pitch;
        //tempSource.GlobalPosition = pos;
        //tempSource.Visible = true;
        tempSource.Play();
    }


    public void PlayLoopingAudio(AudioStream clip, float timeBetween, bool randomizePitch, string id, float volume)
    {

        if (IsTrackPlaying(id))
        {
            return;
        }

        Node instance = loopingAudioModule.Instantiate();
        AddChild(instance);
        
        if (instance is LoopingAudioModule tempModule)
        {
            tempModule.Init(clip, this, timeBetween, randomizePitch, id, volume);
            _loopingAudios.Add(tempModule);
        }
    }

    public void StopLoopingAudio(string id)
    {
        LoopingAudioModule module = _loopingAudios.FirstOrDefault(m => m.ID == id);
        
        if (module != null)
        {
            _loopingAudios.Remove(module);
            module.TerminateModule();
        }
    }

    public bool IsTrackPlaying(string id)
    {
        bool returnValue = false;
        foreach(LoopingAudioModule m in _loopingAudios)
        {
            if (m.ID == id)
            {
                returnValue = true;
            }
            
        }

        return returnValue;
    }
}
