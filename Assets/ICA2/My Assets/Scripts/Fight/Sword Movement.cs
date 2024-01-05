using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;

public class SwordMovement : MonoBehaviour
{
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
    
    
    
    private Vector3 _swordOriginalPosition;
    private Vector3 _swordOriginalRotation;
    
    private void Start()
    {
        _swordOriginalPosition = sword.transform.localPosition;
        _swordOriginalRotation = sword.transform.localEulerAngles;
    }
    
    public void AttackAnimation(int zone)
    {
        
    }
    
    [Button]
    public void OriginalPosition()
    {
        sword.transform.DOLocalMove(_swordOriginalPosition, toZoneTime).SetEase(Ease.OutBack);
        sword.transform.DOLocalRotate(_swordOriginalRotation, toZoneTime).SetEase(Ease.OutBack);
    }
    
    [Button]
    private void TopZone()
    {
        sword.transform.DOLocalMove(topZone.transform.localPosition, toZoneTime).SetEase(Ease.OutBack);
        sword.transform.DOLocalRotate(topRotation, toZoneTime).SetEase(Ease.OutBack);
    }
    
    [Button]
    private void TopAttackZone()
    {
        sword.transform.DOLocalMove(topAttackZone.transform.localPosition, toAttackZoneTime).SetEase(Ease.InQuad);
        sword.transform.DOLocalRotate(topAttackRotation, toAttackZoneTime).SetEase(Ease.InQuad);
    }

    [Button]
    private void RightTopZone()
    {
        sword.transform.DOLocalMove(rightTopZone.transform.localPosition, toZoneTime).SetEase(Ease.OutBack);
        sword.transform.DOLocalRotate(rightTopRotation, toZoneTime).SetEase(Ease.OutBack);
    }
    
    [Button]
    private void RightTopAttackZone()
    {
        sword.transform.DOLocalMove(rightTopAttackZone.transform.localPosition, toAttackZoneTime).SetEase(Ease.InQuad);
        sword.transform.DOLocalRotate(rightTopAttackRotation, toAttackZoneTime).SetEase(Ease.InQuad);
    }
    
    [Button]
    private void RightDownZone()
    {
        sword.transform.DOLocalMove(rightDownZone.transform.localPosition, toZoneTime).SetEase(Ease.OutBack);
        sword.transform.DOLocalRotate(rightDownRotation, toZoneTime).SetEase(Ease.OutBack);
    }
    
    [Button]
    private void RightDownAttackZone()
    {
        sword.transform.DOLocalMove(rightDownAttackZone.transform.localPosition, toAttackZoneTime).SetEase(Ease.InQuad);
        sword.transform.DOLocalRotate(rightDownAttackRotation, toAttackZoneTime).SetEase(Ease.InQuad);
    }
    
    [Button]
    private void LeftDownZone()
    {
        sword.transform.DOLocalMove(leftDownZone.transform.localPosition, toZoneTime).SetEase(Ease.OutBack);
        sword.transform.DOLocalRotate(leftDownRotation, toZoneTime).SetEase(Ease.OutBack);
    }
    
    [Button]
    private void LeftDownAttackZone()
    {
        sword.transform.DOLocalMove(leftDownAttackZone.transform.localPosition, toAttackZoneTime).SetEase(Ease.InQuad);
        sword.transform.DOLocalRotate(leftDownAttackRotation, toAttackZoneTime).SetEase(Ease.InQuad);
    }
    
    [Button]
    private void LeftTopZone()
    {
        sword.transform.DOLocalMove(leftTopZone.transform.localPosition, toZoneTime).SetEase(Ease.OutBack);
        sword.transform.DOLocalRotate(leftTopRotation, toZoneTime).SetEase(Ease.OutBack);
    }
    
    [Button]
    private void LeftTopAttackZone()
    {
        sword.transform.DOLocalMove(leftTopAttackZone.transform.localPosition, toAttackZoneTime).SetEase(Ease.InQuad);
        sword.transform.DOLocalRotate(leftTopAttackRotation, toAttackZoneTime).SetEase(Ease.InQuad);
    }
    
}
