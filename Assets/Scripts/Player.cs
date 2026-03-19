using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour
{
    [SerializeField] private float speed = 7.0f;
    [SerializeField] private GameInput gameInput;
    [SerializeField] private LayerMask counterLayerMask;

    // Start is called once before the first execution of Update after the MonoBehaviour is created

    // Update is called once per frame
    private bool isWalking = false;
    private Vector3 lastInteractDir;
    void Update()
    { 
        HandleMovement();
        HandleInteractions();
        
    }   


    public bool IsWalking()
    {
        return isWalking;
    }
    private void HandleInteractions()
    {
        Vector2 inputVector = gameInput.GetMovementVectorNormalized();

        Vector3 movement = new Vector3(inputVector.x, 0, inputVector.y);
        if(movement != Vector3.zero)
        {
            lastInteractDir = movement;
        }
        float interactDistance = 2f;
        bool canInteract = Physics.Raycast(transform.position, lastInteractDir, out RaycastHit raycastHit, interactDistance, counterLayerMask);

        if (canInteract)
        {
            if (raycastHit.transform.TryGetComponent(out ClearCounter clearCounter))
            {
                clearCounter.Interact();
            }

        }

    }
    private void HandleMovement()
    {
        Vector2 inputVector = gameInput.GetMovementVectorNormalized();

        Vector3 movement = new Vector3(inputVector.x, 0, inputVector.y);

        float moveDistance = speed * Time.deltaTime;
        float playerRadius = .7f;
        float playerHeight = 2f;
        bool canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up *playerHeight , playerRadius, movement, moveDistance);

        if (!canMove)
        {
            Vector3 moveDirX = new Vector3(movement.x,0,0).normalized;
            canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up *playerHeight , playerRadius, moveDirX, moveDistance);
            if (canMove)
            {
                movement = moveDirX;
            } else
            {
                Vector3  moveDirZ = new Vector3(0,0,movement.z).normalized;
                canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up *playerHeight , playerRadius, moveDirZ, moveDistance);

                if (canMove)
                {
                    movement = moveDirZ;
                }
            }
        }
        if (canMove)
        {
            transform.position += movement * moveDistance;
        }
        isWalking = movement != Vector3.zero;
        float rotateSpeed = 10f;
        transform.forward = Vector3.Slerp(transform.forward, movement, Time.deltaTime*rotateSpeed);
    }
}
