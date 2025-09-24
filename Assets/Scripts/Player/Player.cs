using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    [SerializeField] private CharacterController cc;
    [SerializeField] private float playerSpeed;
    [SerializeField] private Inventory playerInventory;

    private Vector2 direction;
    private List<GameObject> collectablesToCollect; // список доступных дл€ подбора предметов (€блок)
    private bool canCollect;

    private void Awake()
    {
        direction = Vector2.zero;
        collectablesToCollect = new();

        if (cc == null)
            throw new System.ArgumentNullException("No CharacterController found!");

        if (playerInventory == null)
            throw new System.ArgumentNullException("No inventory found!");
    }

    private void Update()
    {
        MovePlayer();
    }

    private void MovePlayer()
    {
        Vector3 movementVector = new Vector3(direction.x, direction.y, 0);
        cc.Move(movementVector * (playerSpeed * Time.deltaTime));
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Collectable"))
        {
            // TODO: подбор коллектабла
            canCollect = true;
        }
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Collectable"))
        {
            if (collectablesToCollect.Count == 0)
                canCollect = false;
        }
    }

    public void OnMove(InputAction.CallbackContext context) // callback дл€ событи€ движени€ в Input System (A/D или стрелки влево/вправо)
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

    public void OnInteract(InputAction.CallbackContext context) // callback дл€ событи€ взаимодействи€ в Input System (клавиша E)
    {
        if (context.performed)
        {
            if (canCollect)
            {
                // TODO: добавление предметов в инвентарь

            }
        }
    }
}
