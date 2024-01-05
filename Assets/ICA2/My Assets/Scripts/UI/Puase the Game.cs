using System.Collections;
using System.Collections.Generic;
using GD;
using UnityEngine;

public class PuasetheGame : MonoBehaviour
{
    public BoolGameEvent onPause;
    
    public void Pause()
    {
        onPause.Raise(true);
        Time.timeScale = 0;
    }
    
    public void Resume()
    {
        Time.timeScale = 1;
        onPause.Raise(false);
    }
}
