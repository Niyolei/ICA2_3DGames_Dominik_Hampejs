using System.Collections;
using System.Collections.Generic;
using GD;
using UnityEngine;

public class InteractionBehaviour : MonoBehaviour
{
    private LayerMask interactive;
    public BoolGameEvent playerInRange;
    
    
    
    // Start is called before the first frame update
    void Start()
    {
        interactive = LayerMask.GetMask("Interactive");
    }
    
    public InteractableData CheckForInteractable()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        

        if (Physics.Raycast(ray, out hit, Mathf.Infinity, interactive))
        {
            playerInRange.Raise(true);
            if (hit.collider.gameObject.GetComponent<InteractionHolder>().playerInRange)
            {
                playerInRange.Raise(false);
                return hit.collider.gameObject.GetComponent<InteractionHolder>().interactableData;
            }
            else
            {
                
            }
            
        }
        return null;
    }
}
