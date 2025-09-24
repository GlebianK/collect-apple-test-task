using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [SerializeField] private int inventoryCapacity = 5;

    private List<GameObject> inventoryItems;

    public void AddItem()
    {
        if (inventoryItems.Count == inventoryCapacity)
            return;


    }
}
