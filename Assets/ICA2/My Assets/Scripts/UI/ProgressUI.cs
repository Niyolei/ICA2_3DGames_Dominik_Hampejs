using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ProgressUI : MonoBehaviour
{
    public ProgressText[] progressTextsEditor;
    private Dictionary<Obtainable, string> progressTexts;
    
    public GameObject progressText;
    
    void Start()
    {
        progressTexts = new Dictionary<Obtainable, string>();
        foreach (ProgressText progressText in progressTextsEditor)
        {
            progressTexts.Add(progressText.item, progressText.text);
        }
    }
    
    public void SetProgress(Obtainable item)
    {
        progressText.GetComponent<TextMeshProUGUI>().text = progressTexts[item];
    }
    
}

[System.Serializable]
public class ProgressText
{
    public Obtainable item;
    public string text;
}
