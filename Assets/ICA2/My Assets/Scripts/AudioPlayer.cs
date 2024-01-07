using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioPlayer : MonoBehaviour
{
    public List<AudioData> audioData;
    public AudioSource audioSource;
    private Dictionary<int, AudioClip> clips;

    public void Start()
    {
        clips = new Dictionary<int, AudioClip>();
        foreach (var data in audioData)
        {
            clips.Add(data.id, data.clip);
        }
    }

    public void PlayAudio(int id)
    {
        audioSource.clip = clips[id];
        audioSource.Play();
    }
    
}

[System.Serializable]
public class AudioData
{
    public int id;
    public AudioClip clip;
}
