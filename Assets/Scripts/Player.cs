using System; 
using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour , IKitchenObjectParent
{
    public event EventHandler<OnSelectedCounterChangedEventArgs> OnSelectCounterChanged;
    public class OnSelectedCounterChangedEventArgs : EventArgs
    {
        public BaseCounter selectedCounter;
    }
    [SerializeField] private float speed = 7.0f;
    [SerializeField] private GameInput gameInput;
    [SerializeField] private LayerMask counterLayerMask;
    [SerializeField] private Transform kitchenObjectHoldPoint;
    private BaseCounter selectedCounter;
    private KitchenObject kitchenObject;

    // Start is called once before the first execution of Update after the MonoBehaviour is created

    // Update is called once per frame
    private bool isWalking = false;
     private static Player instance;
    public static Player Instance {get;private set;}
    private Vector3 lastInteractDir;
    private void Awake()
    {
        if(Instance != null)
        {
            Debug.Log("More than one Player");
        }
        Instance = this;
    }
    private void Start()
    {
        gameInput.OnInteractAction += GameInput_OnInteractAction;
    }
    private void GameInput_OnInteractAction(object sender, System.EventArgs e)
    {
        if(selectedCounter != null)
        {
            selectedCounter.Interact(this);
        }
    }
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

        if ( Physics.Raycast(transform.position, lastInteractDir, out RaycastHit raycastHit, interactDistance, counterLayerMask))
        {
            if (raycastHit.transform.TryGetComponent(out BaseCounter baseCounter))
            {
                //Has ClearCounter
                if(baseCounter != selectedCounter)
                {
                    SetSelectedCounter(baseCounter);
                }
            }
            else
            {
                SetSelectedCounter(null);
            }

        }else
        {
            SetSelectedCounter(null);
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

    private void SetSelectedCounter(BaseCounter baseCounter)
    {
        this.selectedCounter = baseCounter;

        OnSelectCounterChanged?.Invoke(this, new OnSelectedCounterChangedEventArgs
        {
            selectedCounter = baseCounter
        });
    }

    public Transform GetKitchenObjectFollowTransform()
    {
        return kitchenObjectHoldPoint;
    }

    public void SetKitchenObject(KitchenObject kitchenObject)
    {
        this.kitchenObject = kitchenObject;
    }
    public KitchenObject GetKitchenObject()
    {
        return kitchenObject;
    }
    public void ClearKitchenObject()
    {
        kitchenObject = null;
    }

    public bool HasKitchenObject()
    {
        return kitchenObject != null;
    }
}
