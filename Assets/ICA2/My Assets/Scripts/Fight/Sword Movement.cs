using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;

public class SwordMovement : MonoBehaviour
{
    public FightManager fightManager;
    public GameObject sword;
    
    public float toZoneTime = 0.5f;
    public float toAttackZoneTime = 0.2f;
    
    [TabGroup("Sword Zones")]
    public GameObject topZone;
    [TabGroup("Sword Zones")]
    public GameObject rightTopZone;
    [TabGroup("Sword Zones")]
    public GameObject rightDownZone;
    [TabGroup("Sword Zones")]
    public GameObject leftDownZone;
    [TabGroup("Sword Zones")]
    public GameObject leftTopZone;
    
    [TabGroup("Sword Attack Zones")]
    public GameObject topAttackZone;
    [TabGroup("Sword Attack Zones")]
    public GameObject rightTopAttackZone;
    [TabGroup("Sword Attack Zones")]
    public GameObject rightDownAttackZone;
    [TabGroup("Sword Attack Zones")]
    public GameObject leftDownAttackZone;
    [TabGroup("Sword Attack Zones")]
    public GameObject leftTopAttackZone;
    
    
    [TabGroup("Sword Rotation")]
    public Vector3 topRotation = new Vector3(-45, 0, 90);
    [TabGroup("Sword Rotation")]
    public Vector3 rightTopRotation = new Vector3(-20, 45, 25);
    [TabGroup("Sword Rotation")]
    public Vector3 rightDownRotation = new Vector3(20, 45, -25);
    [TabGroup("Sword Rotation")]
    public Vector3 leftDownRotation = new Vector3(20, -45, 25);
    [TabGroup("Sword Rotation")]
    public Vector3 leftTopRotation = new Vector3(-20, -45, -25);
    
    [TabGroup("Sword Attack Rotation")]
    public Vector3 topAttackRotation = new Vector3(-160, 0, 90);
    [TabGroup("Sword Attack Rotation")]
    public Vector3 rightTopAttackRotation = new Vector3(-40, 190, -25);
    [TabGroup("Sword Attack Rotation")]
    public Vector3 rightDownAttackRotation = new Vector3(40, 190, 25);
    [TabGroup("Sword Attack Rotation")]
    public Vector3 leftDownAttackRotation = new Vector3(40, -190, 25);
    [TabGroup("Sword Attack Rotation")]
    public Vector3 leftTopAttackRotation = new Vector3(-40, -190, -25);

    public GameObject hitZone;
    
    private Vector3 _swordOriginalPosition;
    private Vector3 _swordOriginalRotation;
    
    private void Start()
    {
        _swordOriginalPosition = sword.transform.localPosition;
        _swordOriginalRotation = sword.transform.localEulerAngles;
    }

    public void AttackRandom()
    {
        ZoneType type = (ZoneType) Random.Range(0, 5);
        AttackAnimation(type);
    }
    
    private void AttackAnimation(ZoneType type)
    {
        switch (type)
        {
            case ZoneType.Top:
                StartCoroutine(AttackTop(type));
                break;
            case ZoneType.RightTop:
                StartCoroutine(AttackRightTop(type));
                break;
            case ZoneType.LeftTop:
                StartCoroutine(AttackLeftTop(type));
                break;
            case ZoneType.RightBottom:
                StartCoroutine(AttackRightDown(type));
                break;
            case ZoneType.LeftBottom:
                StartCoroutine(AttackLeftDown(type));
                break;
        }
    }

    IEnumerator AttackTop(ZoneType type)
    {
        OriginalPosition();
        yield return new WaitForSeconds(toZoneTime);
        TopZone();
        yield return new WaitForSeconds(toZoneTime);
        TopAttackZone();
        yield return new WaitForSeconds(toAttackZoneTime);
        fightManager.Outcome(type);
    }
    
    IEnumerator AttackRightTop(ZoneType type)
    {
        OriginalPosition();
        yield return new WaitForSeconds(toZoneTime);
        RightTopZone();
        yield return new WaitForSeconds(toZoneTime);
        RightTopAttackZone();
        yield return new WaitForSeconds(toAttackZoneTime);
        fightManager.Outcome(type);
    }
    
    IEnumerator AttackRightDown(ZoneType type)
    {
        OriginalPosition();
        yield return new WaitForSeconds(toZoneTime);
        RightDownZone();
        yield return new WaitForSeconds(toZoneTime);
        RightDownAttackZone();
        yield return new WaitForSeconds(toAttackZoneTime);
        fightManager.Outcome(type);
    }
    
    IEnumerator AttackLeftDown(ZoneType type)
    {
        OriginalPosition();
        yield return new WaitForSeconds(toZoneTime);
        LeftDownZone();
        yield return new WaitForSeconds(toZoneTime);
        LeftDownAttackZone();
        yield return new WaitForSeconds(toAttackZoneTime);
        fightManager.Outcome(type);
    }
    
    IEnumerator AttackLeftTop(ZoneType type)
    {
        OriginalPosition();
        yield return new WaitForSeconds(toZoneTime);
        LeftTopZone();
        yield return new WaitForSeconds(toZoneTime);
        LeftTopAttackZone();
        yield return new WaitForSeconds(toAttackZoneTime);
        fightManager.Outcome(type);
    }
    
    public void OriginalPosition()
    {
        sword.transform.DOLocalMove(_swordOriginalPosition, toZoneTime).SetEase(Ease.OutBack);
        sword.transform.DOLocalRotate(_swordOriginalRotation, toZoneTime).SetEase(Ease.OutBack);
    }
    
    private void TopZone()
    {
        sword.transform.DOLocalMove(topZone.transform.localPosition, toZoneTime).SetEase(Ease.OutBack);
        sword.transform.DOLocalRotate(topRotation, toZoneTime).SetEase(Ease.OutBack);
    }
    
    private void TopAttackZone()
    {
        sword.transform.DOLocalMove(topAttackZone.transform.localPosition, toAttackZoneTime).SetEase(Ease.InQuad);
        sword.transform.DOLocalRotate(topAttackRotation, toAttackZoneTime).SetEase(Ease.InQuad);
    }


    private void RightTopZone()
    {
        sword.transform.DOLocalMove(rightTopZone.transform.localPosition, toZoneTime).SetEase(Ease.OutBack);
        sword.transform.DOLocalRotate(rightTopRotation, toZoneTime).SetEase(Ease.OutBack);
    }
    

    private void RightTopAttackZone()
    {
        sword.transform.DOLocalMove(rightTopAttackZone.transform.localPosition, toAttackZoneTime).SetEase(Ease.InQuad);
        sword.transform.DOLocalRotate(rightTopAttackRotation, toAttackZoneTime).SetEase(Ease.InQuad);
    }
    

    private void RightDownZone()
    {
        sword.transform.DOLocalMove(rightDownZone.transform.localPosition, toZoneTime).SetEase(Ease.OutBack);
        sword.transform.DOLocalRotate(rightDownRotation, toZoneTime).SetEase(Ease.OutBack);
    }
    

    private void RightDownAttackZone()
    {
        sword.transform.DOLocalMove(rightDownAttackZone.transform.localPosition, toAttackZoneTime).SetEase(Ease.InQuad);
        sword.transform.DOLocalRotate(rightDownAttackRotation, toAttackZoneTime).SetEase(Ease.InQuad);
    }
    

    private void LeftDownZone()
    {
        sword.transform.DOLocalMove(leftDownZone.transform.localPosition, toZoneTime).SetEase(Ease.OutBack);
        sword.transform.DOLocalRotate(leftDownRotation, toZoneTime).SetEase(Ease.OutBack);
    }
    

    private void LeftDownAttackZone()
    {
        sword.transform.DOLocalMove(leftDownAttackZone.transform.localPosition, toAttackZoneTime).SetEase(Ease.InQuad);
        sword.transform.DOLocalRotate(leftDownAttackRotation, toAttackZoneTime).SetEase(Ease.InQuad);
    }
    

    private void LeftTopZone()
    {
        sword.transform.DOLocalMove(leftTopZone.transform.localPosition, toZoneTime).SetEase(Ease.OutBack);
        sword.transform.DOLocalRotate(leftTopRotation, toZoneTime).SetEase(Ease.OutBack);
    }

    private void LeftTopAttackZone()
    {
        sword.transform.DOLocalMove(leftTopAttackZone.transform.localPosition, toAttackZoneTime).SetEase(Ease.InQuad);
        sword.transform.DOLocalRotate(leftTopAttackRotation, toAttackZoneTime).SetEase(Ease.InQuad);
    }
    
    public void Hit()
    {
        sword.transform.DOLocalMove(hitZone.transform.localPosition, toAttackZoneTime).SetEase(Ease.OutBack);
    }
    
    public void Blocked(ZoneType type)
    {
        switch (type)
        {
            case ZoneType.Top:
                BlockedTop();
                break;
            case ZoneType.RightTop:
                BlockedRightTop();
                break;
            case ZoneType.LeftTop:
                BlockedLeftTop();
                break;
            case ZoneType.RightBottom:
                BlockedRightDown();
                break;
            case ZoneType.LeftBottom:
                BlockedLeftDown();
                break;
            
        }
    }
    
    private void BlockedTop()
    {
        sword.transform.DOLocalMove(topZone.transform.localPosition, toZoneTime).SetEase(Ease.OutBack);
    }
    
    private void BlockedRightTop()
    {
        sword.transform.DOLocalMove(rightTopZone.transform.localPosition, toZoneTime).SetEase(Ease.OutBack);
    }
    
    private void BlockedLeftTop()
    {
        sword.transform.DOLocalMove(leftTopZone.transform.localPosition, toZoneTime).SetEase(Ease.OutBack);
    }
    
    private void BlockedRightDown()
    {
        sword.transform.DOLocalMove(rightDownZone.transform.localPosition, toZoneTime).SetEase(Ease.OutBack);
    }
    
    private void BlockedLeftDown()
    {
        sword.transform.DOLocalMove(leftDownZone.transform.localPosition, toZoneTime).SetEase(Ease.OutBack);
    }
    
    
}
