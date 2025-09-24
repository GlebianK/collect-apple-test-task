using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class InventoryItem
{
    private float itemLifeTime;
    private Sprite itemSprite;

    public float Lifetime => itemLifeTime;

    public void SetItemLifetime(float newLifeTime)
    {
        itemLifeTime = newLifeTime;
    }
}
