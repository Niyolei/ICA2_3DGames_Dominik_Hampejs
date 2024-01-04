using System.Collections;
using System.Collections.Generic;
using GD;
using UnityEngine;

public class DialogueFilter : MonoBehaviour
{
    public DialogueEvent dialogueEvent;
    public EmptyGameEvent leftClickEvent;
    private InteractableData currentData;
    private HashSet<Obtainable> possessedItems;
    private int dialogueIndex = 0;
    
    private DialogueData itemDialogue;
    
    private bool obtainItem = false;
    private bool otherOption = false;
    public bool isEnd = false;
    
    public void handleDialogue(InteractableData interactableData, HashSet<Obtainable> possessedItems)
    {
        currentData= interactableData;
        this.possessedItems = possessedItems;
        dialogueIndex = 0;
        isEnd = false;
        obtainItem = false;
        if(currentData.conditionedDialogues.Length == 0)
           OnDialogueEnd();
        else
            handleCondition();
    }

    private void handleCondition()
    {
        if (!currentData.conditionedDialogues[dialogueIndex].hasCondition)
        {
            dialogueEvent.Raise(currentData.conditionedDialogues[dialogueIndex].dialogueData);
        }
        else
        {
            if (possessedItems.Contains(currentData.conditionedDialogues[dialogueIndex].requiredItem) || (otherOption && currentData.conditionedDialogues[dialogueIndex].requiredItem == null))
            {
                dialogueEvent.Raise(currentData.conditionedDialogues[dialogueIndex].dialogueData);
                otherOption = false;
            }
            else
            {
                otherOption = true;
                OnDialogueEnd();
            }
        }
        
    }

    public void OnDialogueEnd()
    {
            dialogueIndex++;
            if (dialogueIndex >= currentData.conditionedDialogues.Length)
            {
                if (!obtainItem)
                {
                    obtainItem = false;
                    isEnd = true;
                }
            }
            else
            {
                handleCondition();
            }
    }

    public void LeftClick()
    {
        leftClickEvent.Raise(new Empty());
    }
    
    public void AddItemDialogue(DialogueData dialogueData)
    {
        obtainItem = true;
        dialogueEvent.Raise(dialogueData);
    }
}
