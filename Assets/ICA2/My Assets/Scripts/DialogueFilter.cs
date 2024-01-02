using System.Collections;
using System.Collections.Generic;
using GD;
using UnityEngine;

public class DialogueFilter : MonoBehaviour
{
    public DialogueEvent dialogueEvent;
    public EmptyGameEvent leftClickEvent;
    private InteractableData currentData;
    private int dialogueIndex = 0;
    
    private bool isEnd = false;
    
    public void handleDialogue(InteractableData interactableData)
    {
        currentData= interactableData;
        dialogueIndex = 0;
        isEnd = false;
        handleCondition();
    }

    private void handleCondition()
    {
        if (!currentData.conditionedDialogues[dialogueIndex].hasCondition)
        {
            dialogueEvent.Raise(currentData.conditionedDialogues[dialogueIndex].dialogueData);
        }
        
    }

    public void OnDialogueEnd()
    {
        dialogueIndex++;
        if (dialogueIndex < currentData.conditionedDialogues.Length)
        {
            handleCondition();
        }
        else
        {
            isEnd = true;
        }
    }

    public bool LeftClick()
    {
        if (isEnd)
        {
            return true;
        }
        else
        {
            leftClickEvent.Raise(new Empty());
            return false;
        }
    }
}
