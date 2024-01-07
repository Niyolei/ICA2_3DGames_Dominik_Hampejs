using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    
    private AudioPlayer audioPlayer;
    // Start is called before the first frame update
    void Start()
    {
        audioPlayer = GetComponent<AudioPlayer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void FightMusic(bool fight)
    {
        audioPlayer.PlayAudio(fight ? 1 : 0);
    }
}
