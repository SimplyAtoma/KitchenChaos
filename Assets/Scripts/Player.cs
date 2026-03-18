using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour
{
    [SerializeField] private float speed = 7.0f;
    [SerializeField] private GameInput gameInput;
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    // Update is called once per frame
    private bool isWalking = false;
    void Update()
    { 
        Vector2 inputVector = gameInput.GetMovementVectorNormalized();
        Vector3 movement = new Vector3(inputVector.x, 0, inputVector.y);
        transform.position += movement * speed * Time.deltaTime;
        isWalking = movement != Vector3.zero;
        float rotateSpeed = 10f;
        transform.forward = Vector3.Slerp(transform.forward, movement, Time.deltaTime*rotateSpeed);
    }   


    public bool IsWalking()
    {
        return isWalking;
    }
}
