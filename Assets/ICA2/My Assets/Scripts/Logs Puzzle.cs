using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogsPuzzle : MonoBehaviour
{
    public GameObject[] logs;
    public InteractableData normalLogs;
    public InteractableData susLogs;
    
    private int _suslogsIndex = 0;

    private void Start()
    {
        _suslogsIndex = UnityEngine.Random.Range(0, logs.Length);
        
        for (int i = 0; i < logs.Length; i++)
        {
            logs[i].GetComponent<InteractionHolder>().interactableData = i == _suslogsIndex ? susLogs : normalLogs;
        }
    }

    public void AfterShieldObtained()
    {
        logs[_suslogsIndex].GetComponent<InteractionHolder>().interactableData = normalLogs;
    }
}
