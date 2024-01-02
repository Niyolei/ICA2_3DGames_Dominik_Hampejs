using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName = "InteractableData", menuName = "ScriptableObjects/InteractableData", order = 0)]
public class InteractableData : ScriptableObject
{
    public ConditionedDialogue[] conditionedDialogues;
    public bool hasObtainable;
    [ShowIf("hasObtainable")] public Obtainable requiredItem;
    [ShowIf("hasObtainable")] public Obtainable item;

}

[System.Serializable]
public class ConditionedDialogue
{
    public bool hasCondition; 
    [ShowIf("hasCondition")]
    public Obtainable requiredItem;
    public DialogueData dialogueData;
}
