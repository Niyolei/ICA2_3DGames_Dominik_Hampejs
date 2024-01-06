using System;
using System.Collections;
using System.Collections.Generic;
using GD;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class FightManager : MonoBehaviour
{
    public SwordMovement swordMovement;
    public ShieldMovement shieldMovement;
    public TextMeshPro textMeshPro;
    public EmptyGameEvent HitAnimationEvent;
    
    private ZoneType shieldZoneType;
    
    

    public void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, LayerMask.GetMask("Fight")))
        {
            shieldZoneType = hit.collider.gameObject.GetComponent<ZoneHolder>().zoneType;
            shieldMovement.MoveToZone(shieldZoneType);
        }
        
        if (Input.GetMouseButtonDown(3))
        {
            SwingSword();
        }
    }

    private void SwingSword()
    {
        swordMovement.AttackRandom();
    }

    public void Outcome(ZoneType zoneType)
    {
        if (zoneType == shieldZoneType)
        {
            textMeshPro.text = "Blocked";
            swordMovement.Blocked(zoneType);
        }
        else
        {
            textMeshPro.text = "Hit";
            swordMovement.Hit();
            HitAnimationEvent.Raise(new Empty());
        }
        
    }
  
    
}
