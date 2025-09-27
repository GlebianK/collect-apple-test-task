using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private float playerSpeed;
    [SerializeField] private Inventory playerInventory;

    private Vector2 direction;
    private List<GameObject> collectablesToCollect; // список доступных для подбора предметов (яблок)
    private bool canCollect;

    private void Awake()
    {
        direction = Vector2.zero;
        collectablesToCollect = new();

        if (rb == null)
            throw new System.ArgumentNullException("No CharacterController found!");

        if (playerInventory == null)
            throw new System.ArgumentNullException("No inventory found!");
    }

    private void FixedUpdate()
    {
        MovePlayer();
    }

    private void MovePlayer()
    {
        rb.velocityX = direction.x * (playerSpeed * Time.fixedDeltaTime);
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Collectable"))
        {
            collectablesToCollect.Add(col.gameObject);
            canCollect = true;
        }
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Collectable"))
        {
            if (collectablesToCollect.Contains(col.gameObject))
                collectablesToCollect.Remove(col.gameObject);

            if (collectablesToCollect.Count == 0)
                canCollect = false;
        }
    }

    public void OnMove(InputAction.CallbackContext context) // callback для события движения в Input System (A/D или стрелки влево/вправо)
    {
        if (context.performed)
        {
            direction = context.ReadValue<Vector2>();
        }
        else if (context.canceled)
        {
            direction = Vector2.zero;
        }
    }

    public void OnInteract(InputAction.CallbackContext context) // callback для события взаимодействия в Input System (клавиша E)
    {
        if (context.performed)
        {
            if (canCollect && !playerInventory.IsFull)
            {
                playerInventory.AddItem(collectablesToCollect[0]);
                Destroy(collectablesToCollect[0]);
            }
        }
    }
}
