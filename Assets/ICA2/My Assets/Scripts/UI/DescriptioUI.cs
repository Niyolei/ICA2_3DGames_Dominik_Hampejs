using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DescriptioUI : MonoBehaviour
{
    public GameObject description;
    private UIInventory uiInventory;
    
    void Start()
    {
        uiInventory = FindObjectOfType<UIInventory>();
    }

    public void ShowDescription(int index)
    {
        description.SetActive(true);
        string text = uiInventory.inventoryItems[index].name +": "+ uiInventory.inventoryItems[index].description;
        description.GetComponent<TextMeshProUGUI>().text = text;
    }
    
    public void HideDescription()
    {
        description.SetActive(false);
    }
}
