using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Inventory : MonoBehaviour
{
    [SerializeField] private int inventoryCapacity = 5;

    private List<InventoryItem> inventoryItems;
    private bool isFull;

    public bool IsFull => isFull;
    public UnityEvent ItemAdded;
    public UnityEvent<int> ItemRemoved;

    private void Awake()
    {
        inventoryItems = new();
        isFull = false;
    }

    public void AddItem(GameObject itemToAdd)
    {
        if (inventoryItems.Count == inventoryCapacity)
            return;

        // в рамках “« у нас лишь €блоки, так что так, но лучше через интерфейс реализовать подобный функционал (например, ICollectable)
        if (itemToAdd.TryGetComponent<Apple>(out Apple apple))
        {
            InventoryItem item = new InventoryItem();
            item.SetItemLifetime(apple.RotTimer);
            inventoryItems.Add(item);
            ItemAdded.Invoke();
            
            if (inventoryItems.Count == inventoryCapacity)
                isFull = true;

            StartCoroutine(RotItem(item));
        }
        else
            Debug.LogError($"Can't add apple! Object is {itemToAdd.name}");
        
    }

    private void RemoveItem(InventoryItem itemToRemove)
    {
        ItemRemoved.Invoke(inventoryItems.IndexOf(itemToRemove));
        inventoryItems.Remove(itemToRemove);
        isFull = false;
    }

    private IEnumerator RotItem(InventoryItem item)
    {
        yield return new WaitForSeconds(item.Lifetime);
        RemoveItem(item);
    }
}
