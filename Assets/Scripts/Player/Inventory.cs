using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Inventory : MonoBehaviour
{
    [SerializeField] private int inventoryCapacity = 5;

    private List<InventoryItem> inventoryItems;
    private bool isFull;
    private bool shouldRot;

    private List<float> rotTimers;

    public bool IsFull => isFull;
    public UnityEvent ItemAdded;
    public UnityEvent<int> ItemRemoved;

    private void Awake()
    {
        inventoryItems = new();
        rotTimers = new();
        isFull = false;
        shouldRot = true;
        StartCoroutine(RotItem());
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
            rotTimers.Add(apple.RotTimer);
            ItemAdded.Invoke();
            
            if (inventoryItems.Count == inventoryCapacity)
                isFull = true;
        }
        else
            Debug.LogError($"Can't add apple! Object is {itemToAdd.name}");
        
    }

    private void RemoveItem(int index)
    {
        ItemRemoved.Invoke(index);
        inventoryItems.RemoveAt(index);
        isFull = false;
    }

    private void OnDisable()
    {
        shouldRot = false;
    }

    private IEnumerator RotItem() // корутина гниени€ €блок в инвентаре
    {
        while (shouldRot)
        {
            if (rotTimers != null && rotTimers.Count > 0)
            {
                for (int i = 0; i < rotTimers.Count; i++)
                {
                    rotTimers[i] -= 1;

                    if (rotTimers[i] == 0)
                    {
                        RemoveItem(i);
                        rotTimers.RemoveAt(i);
                    }
                }
            }
            yield return new WaitForSeconds(1f);
        }

    }
}
