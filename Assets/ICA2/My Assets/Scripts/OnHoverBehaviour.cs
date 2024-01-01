using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class OnHoverBehaviour : MonoBehaviour
{
    public LayerMask interactive;
    private RaycastHit lastHit = default(RaycastHit);

    
    // Update is called once per frame
    void Update()
    {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, Mathf.Infinity, interactive))
            {
                lastHit = hit;
                hit.collider.gameObject.GetComponent<ShowDescription>().DescriptionOn();
            }
            else if (!RaycastHit.Equals(lastHit, default(RaycastHit)) && !Equals(lastHit, hit))
            {
                lastHit.collider.gameObject.GetComponent<ShowDescription>().DescriptionOff();
            }

    }
}
