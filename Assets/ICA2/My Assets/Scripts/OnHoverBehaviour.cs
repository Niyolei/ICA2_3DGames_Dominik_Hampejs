using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class OnHoverBehaviour : MonoBehaviour
{
    private LayerMask interactive;
    private RaycastHit lastHit = default(RaycastHit);
    private bool isHovering = false;

    private void Start()
    {
        interactive = LayerMask.GetMask("Interactive");
    }
    
    
    public void CheckForHover()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, Mathf.Infinity, interactive))
        {
            lastHit = hit;
            
            if (!Equals(lastHit, default(RaycastHit)))
            {
                hit.collider.gameObject.GetComponent<ShowHighlight>().DescriptionOn();
                isHovering = true;
            }
                
        }
        else if (!RaycastHit.Equals(lastHit, default(RaycastHit)) && !Equals(lastHit, hit) && isHovering)
        {
            lastHit.collider.gameObject.GetComponent<ShowHighlight>().DescriptionOff();
            isHovering = false;
        }
    }
}
