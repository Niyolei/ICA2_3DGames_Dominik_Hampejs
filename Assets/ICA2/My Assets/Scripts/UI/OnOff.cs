using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnOff : MonoBehaviour
{
    public void SetOnOff(bool on)
    {
        gameObject.SetActive(on);
    }
    
    public void SetOnOffInverse(bool on)
    {
        gameObject.SetActive(!on);
    }
}
