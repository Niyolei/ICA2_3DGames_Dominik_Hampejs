using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventorySystem : MonoBehaviour
{
    public List<Obtainable> inventory = new List<Obtainable>();
    
    public void AddItem(Obtainable item)
    {
        inventory.Add(item);
    }
    
    public HashSet<Obtainable> GetInventory()
    {
        return new HashSet<Obtainable>(inventory);
    }
}
