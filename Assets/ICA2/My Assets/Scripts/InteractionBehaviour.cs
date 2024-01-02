using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionBehaviour : MonoBehaviour
{
    private LayerMask interactive;
    
    
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
            return hit.collider.gameObject.GetComponent<InteractionHolder>().interactableData;
        }
        return null;
    }
}
