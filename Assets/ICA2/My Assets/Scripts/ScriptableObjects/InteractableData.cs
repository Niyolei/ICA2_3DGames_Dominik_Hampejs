using System;
using System.Collections;
using System.Collections.Generic;
using GD;
using ICA2.My_Assets.Scripts.ScriptableObjects;
using Sirenix.OdinInspector;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName = "InteractableData", menuName = "ScriptableObjects/InteractableData", order = 0)]
public class InteractableData : ScriptableObject
{
    public bool shouldPlayAnimation;
    public ConditionedDialogue[] conditionedDialogues;
    public bool hasObtainable;
    [ShowIf("hasObtainable")]
    public bool hasCondition;
    [ShowIf("hasCondition")] public Obtainable requiredItem;
    [ShowIf("hasObtainable")] public Obtainable item;
    public bool hasEvent;
    [ShowIf("hasEvent")] public ConditionedEvent conditionedEvent;
    public bool hasFight;
    [ShowIf("hasFight")] public ConditionedFight conditionedFight;

}

[System.Serializable]
public class ConditionedDialogue
{
    public bool hasCondition; 
    [ShowIf("hasCondition")]
    public Obtainable requiredItem;
    public DialogueData dialogueData;
}

[System.Serializable]
public class ConditionedEvent
{
    public bool hasCondition; 
    [ShowIf("hasCondition")]
    public Obtainable requiredItem;
    public EmptyGameEvent gameEvent;
}

[System.Serializable]
public class ConditionedFight
{
    public bool hasCondition; 
    [ShowIf("hasCondition")]
    public Obtainable requiredItem;
    public FightData fightData;
    public DialogueData winDialogue;
    public DialogueData loseDialogue;
}
