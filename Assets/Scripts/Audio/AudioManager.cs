using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

// Code brought to you by terran byte.
public class AudioManager : MonoBehaviour
{
    public Sound[] Sounds;
    public static AudioManager current;

    private List<AudioSource> sources;

    public void Awake()
    {
        sources = new List<AudioSource>();
        current = this;
    }

    private void Update()
    {
        for (var i = 0; sources.Count > 0 && i < sources.Count; i++)
        {
            AudioSource source = sources[i];
            if (!source.isPlaying)
            {
                sources.Remove(source);
                Destroy(source);
            }
        }
    }

    public void PlaySound(string name, double delay = 0)
    {
        Sound sound = Array.Find(Sounds, x => x.Name == name);
        
        AudioSource source = gameObject.AddComponent<AudioSource>();
        sources.Add(source);
        source.clip = sound.Clip;
        source.volume = sound.Volume;
        source.pitch = sound.Pitch;
        
        source.Play((ulong)(delay * sound.Clip.frequency));
    }
}
