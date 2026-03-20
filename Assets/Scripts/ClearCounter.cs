using System; 
using UnityEngine;
using System.Collections;

public class ClearCounter : MonoBehaviour
{
    
    [SerializeField] private KitchenObjectSO kitchenObjectSO;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] private Transform counterTopPoint;
    [SerializeField] private ClearCounter secondClearCounter;
    [SerializeField] private bool testing;

    private KitchenObject kitchenObject;

    private void Update()
    {
        if(testing && Input.GetKeyDown(KeyCode.T))
        {
            if (kitchenObject != null)
            {
                kitchenObject.SetClearCounter(secondClearCounter);
            }
        }
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
   public void Interact()
    {
        if ( kitchenObject == null)
        {
            Debug.Log("Interact!");
            Transform kitchenObjectTransform = Instantiate(kitchenObjectSO.prefab, counterTopPoint);
            kitchenObjectTransform.GetComponent<KitchenObject>().SetClearCounter(this);
        } else
        {
            Debug.Log(kitchenObject.GetClearCounter());
        }
        
    }
    public Transform GetKitchenObjectFollowTransform()
    {
        return counterTopPoint;
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
