using System.Collections;
using System.Collections.Generic;
using GD;
using TMPro;
using UnityEngine;

public class DialogueManager : MonoBehaviour
{
    public Camera dialogueCamera;
    
    public TextMeshProUGUI titleText;
    public TextMeshProUGUI dialogueText;
    
    private DialogueData currentDialogue;
    
    public float typingSpeed = 0.02f;
    private int index = 0;
    
    [SerializeField]
    private EmptyGameEvent dialogueEndEvent;
    
    // Start is called before the first frame update
    void Start()
    {
        dialogueCamera.enabled = false;
    }
    
    
    public void LeftClick()
    {
        if (dialogueText.text == currentDialogue.Monologues[index].Line)
        {
            NextPage();
        }
        else
        {
            StopAllCoroutines();
            dialogueText.text = currentDialogue.Monologues[index].Line;
        }
    }
    
    public void StartDialogue(DialogueData dialogue)
    {
        dialogueCamera.enabled = true;
        currentDialogue = dialogue;
        index = 0;
        TypePage();
    }
    
    private void NextPage()
    {
        if (index < currentDialogue.Monologues.Length - 1)
        {
            index++;
            TypePage();
        }
        else
        {
            dialogueCamera.enabled = false;
            dialogueEndEvent.Raise(new Empty());
        }
    }

    private void TypePage()
    {
        titleText.text = currentDialogue.Monologues[index].Title;
        StartCoroutine(TypeMonologue(currentDialogue.Monologues[index].Line));
    }
    
    IEnumerator TypeMonologue(string sentence)
    {
        dialogueText.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }
    }
}
