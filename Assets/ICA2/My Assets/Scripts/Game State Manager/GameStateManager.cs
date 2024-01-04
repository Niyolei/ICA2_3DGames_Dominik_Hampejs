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
        ItemObtained,
        AnEvent,
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
    
    private InteractableData currentData;
    
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
            case GameState.ItemObtained:
                ObtainItem();
                break;
            case GameState.AnEvent:
                currentGameState = GameState.Moving;    
                break;
            case GameState.Fighting:
                
                break;
            case GameState.GameOver:
                
                break;
        }
        
    }

    private void ObtainItem()
    {
        if (Input.GetMouseButtonDown(0))
        {
            dialogueFilter.LeftClick();
            if (dialogueFilter.isEnd)
            {
                if (CheckForEvent())
                {
                    return;
                }
                currentGameState = GameState.Moving;
            }
        }
        
    }

    private void Dialogue()
    {
        if (Input.GetMouseButtonDown(0))
        {
            dialogueFilter.LeftClick();
            if (dialogueFilter.isEnd)
            {
                if (CheckForFight())
                {
                    return;
                }
                if (CheckForObtainable())
                {
                    return;
                }
                if (CheckForEvent())
                {
                    return;
                }
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
    
    private bool CheckForFight()
    {
        if (currentData.hasFight)
        {
            currentGameState = GameState.Fighting;
            return true;
        }
        else
        {
            return false;
        }
    }
    
    private bool CheckForObtainable()
    {
        if (currentData.hasObtainable)
        {
            if (currentData.hasCondition)
            {
                if (inventorySystem.GetInventory().Contains(currentData.requiredItem))
                {
                    inventorySystem.AddItem(currentData.item);
                }
            }
            else
            {
                inventorySystem.AddItem(currentData.item);
            }
            currentGameState = GameState.ItemObtained;
            dialogueFilter.AddItemDialogue(currentData.item.dialogueData);
            return true;
        }

        return false;
    }
    
    private bool CheckForEvent()
    {
        if (currentData.hasEvent)
        {
            if (currentData.hasCondition)
            {
                if (inventorySystem.GetInventory().Contains(currentData.requiredItem))
                {
                    currentData.conditionedEvent.gameEvent.Raise(new Empty());
                }
            }
            else
            {
                currentData.conditionedEvent.gameEvent.Raise(new Empty());
            }
            currentGameState = GameState.AnEvent;
            return true;
        }
        else
        {
            return false;
        }
    }
    
    private void Interact()
    {
        if (Input.GetMouseButtonDown(0))
        {
            currentData = interactionBehaviour.CheckForInteractable();
            if (currentData != null)
            {
                currentGameState = GameState.Dialogue;
                dialogueFilter.handleDialogue(currentData, inventorySystem.GetInventory());
                if (currentData.shouldPlayAnimation)
                {   
                    interactAnimationEvent.Raise(new Empty());
                    
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
