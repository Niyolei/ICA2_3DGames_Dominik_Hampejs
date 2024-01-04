using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using Unity.VisualScripting;
using UnityEngine;

public class checkInteractiveRange : MonoBehaviour
{
  public float range;
  private Collider[] collisions;
  
  public void SwitchRange(bool on)
  {
    if (on)
    {
      EnableInRage();
    }
    else
    {
      DisableInRange();
    }
  }
  
  private void EnableInRage()
  {
    collisions = Physics.OverlapSphere(transform.position, range);
    foreach (Collider coll in collisions)
    {
      if (coll.CompareTag("Interactable"))
      {
        coll.GetComponent<InteractionHolder>().playerInRange = true;
      }
    }
  }

  private void DisableInRange()
  {
    foreach (Collider coll in collisions)
    {
      if (coll.CompareTag("Interactable"))
      {
        coll.GetComponent<InteractionHolder>().playerInRange = false;
      }
    }
    
  }
  
  
}
