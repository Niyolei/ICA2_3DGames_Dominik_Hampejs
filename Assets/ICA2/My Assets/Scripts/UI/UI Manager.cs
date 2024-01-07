using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public AudioPlayer audioPlayer;
    public UIInventory uiInventory;

    public void ItemObtained(Obtainable item)
    {
        audioPlayer.PlayAudio(1);
        uiInventory.AddItem(item);
        Debug.Log("Item Obtained");
    }
}
