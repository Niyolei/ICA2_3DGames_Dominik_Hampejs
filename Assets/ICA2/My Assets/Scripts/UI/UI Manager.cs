using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public AudioPlayer audioPlayer;

    public void ItemObtained(Obtainable item)
    {
        audioPlayer.PlayAudio(1);
        Debug.Log("Item Obtained");
    }
}
