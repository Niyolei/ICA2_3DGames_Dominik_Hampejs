using System.Collections;
using System.Collections.Generic;
using GD;
using UnityEngine;

public class TitleScreen : MonoBehaviour
{
    public EmptyGameEvent onPlay;
    
    public void Play()
    {
        onPlay.Raise(new Empty());
    }
}
