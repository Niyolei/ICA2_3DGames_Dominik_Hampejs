using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


[CreateAssetMenu(fileName = "Obtainable", menuName = "ScriptableObjects/Obtainable", order = 0)]
public class Obtainable : ScriptableObject
{
    public string name;
    public Sprite sprite;
    public string description;
    public DialogueData dialogueData;
}
