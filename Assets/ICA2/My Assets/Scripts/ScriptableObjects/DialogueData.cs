using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName = "DialogueData", menuName = "ScriptableObjects/DialogueData", order = 0)]
public class DialogueData : ScriptableObject
{
    public Monologue[] Monologues;

}

[System.Serializable]
public class Monologue
{
    public string Title;
    public string Line;
}
