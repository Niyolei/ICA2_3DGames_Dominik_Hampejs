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
    private bool isParring = false;
    private bool shouldParry = false;
    public float parryTime = 0.2f;
    private float parryTimer = 0f;
    
    

    public void Update()
    {
        if (Input.GetMouseButtonDown(0) && !isParring)
        {
            isParring = true;
            shouldParry = true;
            shieldMovement.Parry(shieldZoneType);
        }

        if (!isParring)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, Mathf.Infinity, LayerMask.GetMask("Fight")))
            {
                shieldZoneType = hit.collider.gameObject.GetComponent<ZoneHolder>().zoneType;
                shieldMovement.MoveToZone(shieldZoneType);
            }
        }
        else
        {
            parryTimer += Time.deltaTime;
            if (parryTimer >= parryTime)
            {
                shouldParry = false;
                parryTimer = 0f;
            }
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
        if (isParring && zoneType == shieldZoneType)
        {
            if (shouldParry)
            {
                textMeshPro.text = "Parry";
                swordMovement.Blocked(zoneType);
            }
            else
            {
                textMeshPro.text = "Hit";
                swordMovement.Hit();
                HitAnimationEvent.Raise(new Empty());
            }
            
        }
        else if (zoneType == shieldZoneType)
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
        
        isParring = false;
        shouldParry = false;
    }
  
    
}
