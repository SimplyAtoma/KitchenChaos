using UnityEngine;

public class GameInput : MonoBehaviour
{
    private PlayerInput playerInput;
    private void Awake()
    {
        playerInput = new PlayerInput();
        playerInput.Player.Enable();
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public Vector2 GetMovementVectorNormalized()
    {
        Vector2 inputVector = playerInput.Player.Move.ReadValue<Vector2>();
        
        inputVector = inputVector.normalized;
        return inputVector;
    }
}
