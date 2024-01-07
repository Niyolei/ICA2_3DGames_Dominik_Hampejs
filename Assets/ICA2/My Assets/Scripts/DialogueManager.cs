using System.Collections;
using System.Collections.Generic;
using GD;
using TMPro;
using UnityEngine;

//This code is based on the tutorial from BMo https://www.youtube.com/watch?v=8oTYabhj248. Accessed On: 01/24
//All functionality is the same, I just took everything out of the Update function and made it into a function that can be called from other scripts.
public class DialogueManager : MonoBehaviour
{
    public Camera dialogueCamera;
    
    public TextMeshProUGUI titleText;
    public TextMeshProUGUI dialogueText;
    
    private DialogueData currentDialogue;
    
    private AudioSource audioSource;
    
    public float typingSpeed = 0.02f;
    private int index = 0;
    
    [SerializeField]
    private EmptyGameEvent dialogueEndEvent;
    
    // Start is called before the first frame update
    void Start()
    {
        dialogueCamera.enabled = false;
        audioSource = GetComponent<AudioSource>();
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
            audioSource.Play();
            yield return new WaitForSeconds(typingSpeed);
        }
    }
}
