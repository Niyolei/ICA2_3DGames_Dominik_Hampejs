using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using UnityEngine;

public class ZoneHolder : MonoBehaviour
{
    [EnumMember]
    public ZoneType zoneType;
}

public enum ZoneType
{
    Top,
    RightTop,
    LeftTop,
    RightBottom,
    LeftBottom
}
