using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour
{
    [SerializeField] private float speed = 5.0f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    // Update is called once per frame
    private bool isWalking = false;
    void Update()
    { 
        Vector2 inputVector = new Vector2(0, 0);
        if (Input.GetKey(KeyCode.W))
        {
            inputVector.y += 1;
        }
        if (Input.GetKey(KeyCode.S))
        {
            inputVector.y -= 1;
        }
        if (Input.GetKey(KeyCode.A))
        {
            inputVector.x -= 1;
        }
        if (Input.GetKey(KeyCode.D))
        {
            inputVector.x += 1;
        }
        inputVector = inputVector.normalized;

        Vector3 movement = new Vector3(inputVector.x, 0, inputVector.y);
        transform.position += movement * speed * Time.deltaTime;
        isWalking = movement != Vector3.zero;
        float rotateSpeed = 10f;
        transform.forward = Vector3.Slerp(transform.forward, movement, Time.deltaTime*rotateSpeed);
    }   

    public void Interact()
    {
        Debug.Log("Interacting");
    }

    public bool IsWalking()
    {
        return isWalking;
    }
}
