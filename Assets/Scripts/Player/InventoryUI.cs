using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    [SerializeField] InventoryItemUI[] uiItems;

    private void Awake()
    {
        Inventory temp = FindAnyObjectByType<Inventory>();
        if (temp != null)
        {
            temp.ItemAdded.AddListener(OnItemAdded);
            temp.ItemRemoved.AddListener(OnItemRemoved);
        }      
    }

    public void OnItemAdded()
    {
        foreach (InventoryItemUI item in uiItems)
        {
            if (item.IsEmpty)
            {
                item.SetSlot();
                break;
            }
        }
    }

    public void OnItemRemoved(int removedIndex)
    {
        uiItems[removedIndex].ClearSlot();
    }

    private void OnDisable()
    {
        Inventory temp = FindAnyObjectByType<Inventory>();
        if (temp != null)
        {
            temp.ItemAdded.RemoveListener(OnItemAdded);
            temp.ItemRemoved.RemoveListener(OnItemRemoved);
        }
    }
}
