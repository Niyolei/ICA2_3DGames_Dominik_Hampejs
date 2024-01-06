using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using GD;
using ICA2.My_Assets.Scripts.ScriptableObjects;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class FightManager : MonoBehaviour
{
    public GameObject playerPosition;
    public Vector3 enemyPositionOffset;
    public Vector3GameEvent PlayerPositionEvent;
    public SwordMovement swordMovement;
    public ShieldMovement shieldMovement;
    public TextMeshPro textMeshPro;
    public EmptyGameEvent HitAnimationEvent;
    
    public GameObject virtualCamera;
    public GameObject fightingObjects;
    
    private FightData currentFightData;
    
    public Animator playerAnimator;
    private int fightHash = Animator.StringToHash("Fight");
    
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

    public void StartFight(FightData fightData)
    {
        PlayerPositionEvent.Raise(fightData.playerPosition.transform.position);
        currentFightData = fightData;
    }
    
    public void InitiateFight()
    {
        fightingObjects.SetActive(true);
        currentFightData.enemyPosition.GetComponent<ShowHighlight>().DescriptionOff();
        currentFightData.enemyPosition.GetComponent<EnemyAnimation>().SetFight(true);
        playerAnimator.SetBool(fightHash, true);
        playerPosition.transform.rotation = Quaternion.LookRotation(currentFightData.enemyPosition.transform.position - playerPosition.transform.position);
        virtualCamera.gameObject.SetActive(true);
        textMeshPro.text = "";
        
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
