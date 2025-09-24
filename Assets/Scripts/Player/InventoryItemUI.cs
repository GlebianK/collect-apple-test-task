using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryItemUI : MonoBehaviour
{
    [SerializeField] private Image selfImage;
    [SerializeField] private Sprite[] sprites; // заглушка для состояния слота инвентаря "свободно/занято"

    private bool isEmpty;

    public bool IsEmpty => isEmpty;

    private void Awake()
    {
        if (sprites == null || sprites.Length < 2)
            throw new System.Exception("Smth wrong with sprites!");

        selfImage.sprite = sprites[0];
    }

    public void SetSlot()
    {
        isEmpty = false;
        selfImage.sprite = sprites[1];
        selfImage.color = Color.red;
    }

    public void ClearSlot()
    {
        selfImage.sprite = sprites[0];
        isEmpty = true;
        selfImage.color = Color.white;
    }
}
