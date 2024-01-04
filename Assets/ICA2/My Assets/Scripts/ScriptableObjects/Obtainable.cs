using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using Image = UnityEngine.UI.Image;

[CreateAssetMenu(fileName = "Obtainable", menuName = "ScriptableObjects/Obtainable", order = 0)]
public class Obtainable : ScriptableObject
{
    public string name;
    public Image image;
    public string description;
    public DialogueData dialogueData;
}
