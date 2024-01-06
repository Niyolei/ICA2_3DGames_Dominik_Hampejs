using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using GD;
using ICA2.My_Assets.Scripts.ScriptableObjects;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class FightManager : MonoBehaviour
{
    public GameObject playerPosition;
    public Vector3 enemyPositionOffset;
    public Vector3GameEvent PlayerPositionEvent;
    public SwordMovement swordMovement;
    public ShieldMovement shieldMovement;
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
    
    public float parryResetTime = 0.7f;
    private float parryResetTimer = 0f;
    
    private bool isFighting = false;
    
    private bool shouldSwing = false;
    
    public int maxHealth = 3;
    private int currentHealth;
    public int enemyMaxHealth = 3;
    private int enemyCurrentHealth;
    
    public BoolGameEvent endOfFightEvent;
    
    

    public void Update()
    {
        if (isFighting)
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
                parryResetTimer += Time.deltaTime;
                if (parryResetTimer >= parryResetTime)
                {
                    isParring = false;
                    parryResetTimer = 0f;
                }
            }

            if (shouldSwing)
            {
                shouldSwing = false;
                StartCoroutine(SwingSword());
            }
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
        StartCoroutine(StartSwings());
        currentHealth = maxHealth;
        enemyCurrentHealth = enemyMaxHealth;
        shouldSwing = false;
        isFighting = true;
    }
    

    private IEnumerator SwingSword()
    {
        swordMovement.AttackRandom();
        yield return new WaitForSeconds(swordMovement.toZoneTime* 2 + 2* swordMovement.toAttackZoneTime);
        shouldSwing = true;
    }
    
    private IEnumerator StartSwings()
    {
        yield return new WaitForSeconds(1f);
        shouldSwing = true;
    }

    public void Outcome(ZoneType zoneType)
    {
        if (isParring && zoneType == shieldZoneType)
        {
            if (shouldParry)
            {
                swordMovement.Blocked(zoneType);
                shieldMovement.PlayParticle();
                Parry();
            }
            else
            {
                GetHit();
            }
            
        }
        else if (zoneType == shieldZoneType)
        {
            
            swordMovement.Blocked(zoneType);
        }
        else
        {
            GetHit();
        }
        
        isParring = false;
        shouldParry = false;
    }
    
    private void GetHit()
    {
        currentHealth--;
        swordMovement.Hit();
        HitAnimationEvent.Raise(new Empty());
        if (currentHealth <= 0)
        {
            StartCoroutine(EndFight(false));
        }
    }
    
    private void Parry()
    {
        enemyCurrentHealth--;
        if (enemyCurrentHealth <= 0)
        {
            StartCoroutine(EndFight(true));
        }
    }

    IEnumerator EndFight(bool win)
    {
        isFighting = false;
        yield return new WaitForSeconds(0.5f);
        virtualCamera.gameObject.SetActive(false);
        fightingObjects.SetActive(false);
        currentFightData.enemyPosition.GetComponent<EnemyAnimation>().SetFight(false);
        playerAnimator.SetBool(fightHash, false);
        swordMovement.OriginalPosition();
        StopAllCoroutines();
        endOfFightEvent.Raise(win);
        
    }
    
  
    
}
