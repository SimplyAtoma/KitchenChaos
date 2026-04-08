using UnityEngine;
using System;
public class GameInput : MonoBehaviour
{
    private PlayerInput playerInput;
    public event EventHandler OnInteractAction;
    public event EventHandler OnInteractAlternateAction;
    private void Awake()
    {
        playerInput = new PlayerInput();
        playerInput.Player.Enable();

        playerInput.Player.Interact.performed += Interact_performed;
        playerInput.Player.InteractAlternate.performed += InteractAlternate_performed;
    }

    private void Interact_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        OnInteractAction?.Invoke(this,EventArgs.Empty);
    }
    private void InteractAlternate_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        OnInteractAlternateAction?.Invoke(this,EventArgs.Empty);
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public Vector2 GetMovementVectorNormalized()
    {
        Vector2 inputVector = playerInput.Player.Move.ReadValue<Vector2>();
        
        inputVector = inputVector.normalized;
        return inputVector;
    }
}
