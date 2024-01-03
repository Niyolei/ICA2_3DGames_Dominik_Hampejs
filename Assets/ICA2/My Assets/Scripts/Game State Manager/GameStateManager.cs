using System.Collections;
using System.Collections.Generic;
using GD;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;

public class GameStateManager : MonoBehaviour
{
    public enum GameState
    {
        Menu,
        Moving,
        Fighting,
        Dialogue,
        GameOver
    }
    
    public GameState currentGameState = GameState.Menu;
    
    private OnHoverBehaviour hoverBehaviour;
    private InteractionBehaviour interactionBehaviour;
    private DialogueFilter dialogueFilter;
    private InventorySystem inventorySystem;
    
    private float _checkForHoverRate = 0.1f;
    private float _checkForHoverTimer = 0.0f;
    
    public EmptyGameEvent interactAnimationEvent;
    public Vector3GameEvent moveEvent;
    
    void Start()
    {
        hoverBehaviour = GetComponent<OnHoverBehaviour>();
        interactionBehaviour = GetComponent<InteractionBehaviour>();
        dialogueFilter = GetComponent<DialogueFilter>();
        inventorySystem = GetComponent<InventorySystem>();
    }

    // Update is called once per frame
    void Update()
    {
        switch (currentGameState)
        {
            case GameState.Menu:
                Menu();
                break;
            case GameState.Moving:
                Moving();
                break;
            case GameState.Dialogue:
                Dialogue();
                break;
            case GameState.Fighting:
                
                break;
            case GameState.GameOver:
                
                break;
        }
        
    }

    private void Dialogue()
    {
        if (Input.GetMouseButtonDown(0))
        {
            bool isEnd = dialogueFilter.LeftClick();
            if (isEnd)
            {
                currentGameState = GameState.Moving;
            }
        }
    }

    private void Menu()
    {
        if (Input.GetMouseButtonDown(0))
        {
            currentGameState = GameState.Moving;
        }
    }
    
    private void Moving()
    {
        Hover();
        Interact();
        Move();
    }

    private void Move()
    {
        if (Input.GetMouseButton(1))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            Vector3 targetPosition;

            if (Physics.Raycast(ray, out hit, Mathf.Infinity, LayerMask.GetMask("Ground")))
            {
                targetPosition = hit.point;
                targetPosition.y = transform.position.y;
                
                moveEvent.Raise(targetPosition);
            }
        }
    }

    private void Hover()
    {
        _checkForHoverTimer += Time.deltaTime;
        if (_checkForHoverTimer >= _checkForHoverRate)
        {
            hoverBehaviour.CheckForHover();
            _checkForHoverTimer = 0.0f;
        }
    }
    
    private void Interact()
    {
        if (Input.GetMouseButtonDown(0))
        {
            InteractableData interactableData = interactionBehaviour.CheckForInteractable();
            if (interactableData != null)
            {
                currentGameState = GameState.Dialogue;
                dialogueFilter.handleDialogue(interactableData, inventorySystem.GetInventory());
                if (interactableData.shouldPlayAnimation)
                {   
                    interactAnimationEvent.Raise(new Empty());
                    
                }
                if (interactableData.hasObtainable)
                {
                    if (interactableData.hasCondition)
                    {
                        if (inventorySystem.GetInventory().Contains(interactableData.requiredItem))
                        {
                            inventorySystem.AddItem(interactableData.item);
                        }
                    }
                    else
                    {
                        inventorySystem.AddItem(interactableData.item);
                    }
                }
            }
        }
    }
    
    
    

    private void Fighting()
    {
        
    }
    
    private void GameOver()
    {
        
    }
}
