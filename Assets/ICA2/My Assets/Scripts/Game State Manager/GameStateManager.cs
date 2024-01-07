using System.Collections;
using System.Collections.Generic;
using GD;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;

public class GameStateManager : MonoBehaviour
{
    public enum GameState
    {
        Menu,
        Paused,
        Moving,
        Fighting,
        FightDialogue,
        Dialogue,
        ItemObtained,
        AnEvent,
        GameOver
    }
    
    public GameState currentGameState = GameState.Menu;
    private GameState previousGameState = GameState.Menu;
    
    private OnHoverBehaviour hoverBehaviour;
    private InteractionBehaviour interactionBehaviour;
    private DialogueFilter dialogueFilter;
    private InventorySystem inventorySystem;
    
    private float _checkForHoverRate = 0.1f;
    private float _checkForHoverTimer = 0.0f;
    
    public EmptyGameEvent interactAnimationEvent;
    public Vector3GameEvent moveEvent;
    public FightDataEvent fightEvent;
    public ObtainableEvent itemToUIEvent;
    
    private InteractableData currentData;
    
    private bool fightEnd = false;
    
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
            case GameState.FightDialogue:
                ObtainItem();
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

    public void StartGame()
    {
        currentGameState = GameState.Moving;
    }
    
    public void OnPause(bool isPaused)
    {
        if (isPaused)
        {
            previousGameState = currentGameState;
            currentGameState = GameState.Paused;
        }
        else
        {
            currentGameState = previousGameState;
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
            if (currentData.conditionedFight.hasCondition)
            {
                if (inventorySystem.GetInventory().Contains(currentData.conditionedFight.requiredItem))
                {
                    fightEvent.Raise(currentData.conditionedFight.fightData);
                    currentGameState = GameState.Fighting;
                    return true;
                }
            }
            else
            {
                fightEvent.Raise(currentData.conditionedFight.fightData);
                currentGameState = GameState.Fighting;
                return true;
            }
        }
        return false;
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
                    currentGameState = GameState.ItemObtained;
                    dialogueFilter.AddItemDialogue(currentData.item.dialogueData);
                    itemToUIEvent.Raise(currentData.item);
                    return true;
                }

                return false;
            }
            else
            {
                inventorySystem.AddItem(currentData.item);
                currentGameState = GameState.ItemObtained;
                dialogueFilter.AddItemDialogue(currentData.item.dialogueData);
                itemToUIEvent.Raise(currentData.item);
                return true;
            }
            
        }

        return false;
    }
    
    private bool CheckForEvent()
    {
        if (currentData.hasEvent)
        {
            if (currentData.conditionedEvent.hasCondition)
            {
                if (inventorySystem.GetInventory().Contains(currentData.conditionedEvent.requiredItem))
                {
                    currentData.conditionedEvent.gameEvent.Raise(new Empty());
                    currentGameState = GameState.AnEvent;
                    return true;
                }

                return false;
            }
            else
            {
                currentData.conditionedEvent.gameEvent.Raise(new Empty());
                currentGameState = GameState.AnEvent;
                return true;
            }
            
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
    
    
    

    public void Fighting(bool outcome)
    {
        if (outcome)
        {
            if (currentData.conditionedFight.winItem != null)
            {
                inventorySystem.AddItem(currentData.conditionedFight.winItem);
                dialogueFilter.AddItemDialogue(currentData.conditionedFight.winItem.dialogueData);
                itemToUIEvent.Raise(currentData.conditionedFight.winItem);
                currentGameState = GameState.ItemObtained;
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
        else
        {
            currentGameState = GameState.FightDialogue;
            dialogueFilter.AddItemDialogue(currentData.conditionedFight.loseDialogue);
        }
        
    }
    
    private void GameOver()
    {
        
    }
}
