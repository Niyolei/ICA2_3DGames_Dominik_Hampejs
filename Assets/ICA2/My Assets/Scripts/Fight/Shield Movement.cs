using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;

public class ShieldMovement : MonoBehaviour
{
    public GameObject shield;
    
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
    
    [Button]
    public void ToTopZone()
    {
        shield.transform.DOLocalMove(topZone.transform.localPosition, toZoneTime);
        shield.transform.DOLocalRotate(topRotation, toZoneTime);
    }
    
    [Button]
    public void ToRightTopZone()
    {
        shield.transform.DOLocalMove(rightTopZone.transform.localPosition, toZoneTime);
        shield.transform.DOLocalRotate(rightTopRotation, toZoneTime);
    }
    
    [Button]
    public void ToLeftTopZone()
    {
        shield.transform.DOLocalMove(leftTopZone.transform.localPosition, toZoneTime);
        shield.transform.DOLocalRotate(leftTopRotation, toZoneTime);
    }
    
    [Button]
    public void ToRightDownZone()
    {
        shield.transform.DOLocalMove(rightDownZone.transform.localPosition, toZoneTime);
        shield.transform.DOLocalRotate(rightDownRotation, toZoneTime);
    }
    
    [Button]
    public void ToLeftDownZone()
    {
        shield.transform.DOLocalMove(leftDownZone.transform.localPosition, toZoneTime);
        shield.transform.DOLocalRotate(leftDownRotation, toZoneTime);
    }
    
}
