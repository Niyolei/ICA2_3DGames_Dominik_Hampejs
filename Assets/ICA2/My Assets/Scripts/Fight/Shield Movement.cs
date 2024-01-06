using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;

public class ShieldMovement : MonoBehaviour
{
    public GameObject shield;

    public float parryWindow = 0.2f;
    public float toZoneTime = 0.5f;

    [TabGroup("Shield Zones")] public GameObject topZone;
    [TabGroup("Shield Zones")] public GameObject rightTopZone;
    [TabGroup("Shield Zones")] public GameObject leftTopZone;
    [TabGroup("Shield Zones")] public GameObject rightDownZone;
    [TabGroup("Shield Zones")] public GameObject leftDownZone;

    [TabGroup("Shield Rotation")] public Vector3 topRotation = new Vector3(-45, 0, 90);
    [TabGroup("Shield Rotation")] public Vector3 rightTopRotation = new Vector3(-20, 45, 25);
    [TabGroup("Shield Rotation")] public Vector3 leftTopRotation = new Vector3(-20, -45, -25);
    [TabGroup("Shield Rotation")] public Vector3 rightDownRotation = new Vector3(20, 45, -25);
    [TabGroup("Shield Rotation")] public Vector3 leftDownRotation = new Vector3(20, -45, 25);
    
    public Vector3 parryOffset = new Vector3(0, 0, 0.3f);
    public float parryRotation = 40f;


    public void MoveToZone(ZoneType type)
    {
        switch (type)
        {
            case ZoneType.Top:
                ToTopZone();
                break;
            case ZoneType.RightTop:
                ToRightTopZone();
                break;
            case ZoneType.LeftTop:
                ToLeftTopZone();
                break;
            case ZoneType.RightBottom:
                ToRightDownZone();
                break;
            case ZoneType.LeftBottom:
                ToLeftDownZone();
                break;
        }
    }


    public void ToTopZone()
    {
        shield.transform.DOLocalMove(topZone.transform.localPosition, toZoneTime);
        shield.transform.DOLocalRotate(topRotation, toZoneTime);
    }


    public void ToRightTopZone()
    {
        shield.transform.DOLocalMove(rightTopZone.transform.localPosition, toZoneTime);
        shield.transform.DOLocalRotate(rightTopRotation, toZoneTime);
    }


    public void ToLeftTopZone()
    {
        shield.transform.DOLocalMove(leftTopZone.transform.localPosition, toZoneTime);
        shield.transform.DOLocalRotate(leftTopRotation, toZoneTime);
    }


    public void ToRightDownZone()
    {
        shield.transform.DOLocalMove(rightDownZone.transform.localPosition, toZoneTime);
        shield.transform.DOLocalRotate(rightDownRotation, toZoneTime);
    }

    public void ToLeftDownZone()
    {
        shield.transform.DOLocalMove(leftDownZone.transform.localPosition, toZoneTime);
        shield.transform.DOLocalRotate(leftDownRotation, toZoneTime);
    }

    public void Parry(ZoneType type)
    {
        switch (type)
        {
            case ZoneType.Top:
                ParryTop();
                break;
            case ZoneType.RightTop:
                ParryRightTop();
                break;
            case ZoneType.LeftTop:
                ParryLeftTop();
                break;
            case ZoneType.RightBottom:
                ParryRightDown();
                break;
            case ZoneType.LeftBottom:
                ParryLeftDown();
                break;
        }
    }

    private void ParryTop()
    {
        shield.transform.localPosition = topZone.transform.localPosition;
        shield.transform.localRotation = Quaternion.Euler(topRotation);
        Vector3 rotation = shield.transform.localRotation.eulerAngles;
        rotation.z -= parryRotation;
        shield.transform.DORotate(rotation, parryWindow).SetEase(Ease.OutBack);
        shield.transform.DOLocalMove(shield.transform.localPosition + parryOffset, parryWindow).SetEase(Ease.OutBack);
    }
    
    private void ParryRightTop()
    {
        shield.transform.localPosition = rightTopZone.transform.localPosition;
        shield.transform.localRotation = Quaternion.Euler(rightTopRotation);
        Vector3 rotation = shield.transform.localRotation.eulerAngles;
        rotation.z -= parryRotation;
        shield.transform.DORotate(rotation, parryWindow).SetEase(Ease.OutBack);
        shield.transform.DOLocalMove(shield.transform.localPosition + parryOffset, parryWindow).SetEase(Ease.OutBack);
    }
    
    private void ParryLeftTop()
    {
        shield.transform.localPosition = leftTopZone.transform.localPosition;
        shield.transform.localRotation = Quaternion.Euler(leftTopRotation);
        Vector3 rotation = shield.transform.localRotation.eulerAngles;
        rotation.z += parryRotation;
        shield.transform.DORotate(rotation, parryWindow).SetEase(Ease.OutBack);
        shield.transform.DOLocalMove(shield.transform.localPosition - parryOffset, parryWindow).SetEase(Ease.OutBack);
    }
    
    private void ParryRightDown()
    {
        shield.transform.localPosition = rightDownZone.transform.localPosition;
        shield.transform.localRotation = Quaternion.Euler(rightDownRotation);
        Vector3 rotation = shield.transform.localRotation.eulerAngles;
        rotation.z -= parryRotation;
        shield.transform.DORotate(rotation, parryWindow).SetEase(Ease.OutBack);
        shield.transform.DOLocalMove(shield.transform.localPosition + parryOffset, parryWindow).SetEase(Ease.OutBack);
    }
    
    private void ParryLeftDown()
    {
        shield.transform.localPosition = leftDownZone.transform.localPosition;
        shield.transform.localRotation = Quaternion.Euler(leftDownRotation);
        Vector3 rotation = shield.transform.localRotation.eulerAngles;
        rotation.z += parryRotation;
        shield.transform.DORotate(rotation, parryWindow).SetEase(Ease.OutBack);
        shield.transform.DOLocalMove(shield.transform.localPosition - parryOffset, parryWindow).SetEase(Ease.OutBack);
    }
    
    public void PlayParticle()
    {
        shield.GetComponentInChildren<ParticleSystem>().Play();
    }
    
}


