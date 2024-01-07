using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class UIInventory : MonoBehaviour
{
    public List<GameObject> inventorySlots;
    public List<Obtainable> inventoryItems;
    
    public float initialSlotSize = 30f;
    public float slotSize = 20f;
    
    private Vector3 originalPosition;
    
    [Header("Progression")]
    public ProgressUI progressionUI;
    
    public void Start()
    {
        originalPosition = transform.localPosition;
    }
    
    
    public void AddItem(Obtainable item)
    {
        inventoryItems.Add(item);
        progressionUI.SetProgress(item);
        inventorySlots[inventoryItems.Count - 1].GetComponent<UnityEngine.UI.Image>().sprite = item.sprite;
    }
    
    public void OpenInventory()
    {
        Vector3 newPosition = originalPosition;
        newPosition.y += inventoryItems.Count * slotSize + initialSlotSize;
        gameObject.transform.DOLocalMove(newPosition, 0.5f);
    }
    
    public void CloseInventory()
    {
        gameObject.transform.DOLocalMove(originalPosition, 0.5f);
    }
}
