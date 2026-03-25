using System; 
using UnityEngine;
using System.Collections;

public class ClearCounter : BaseCounter
{
    
    [SerializeField] private KitchenObjectSO kitchenObjectSO;
    // Start is called once before the first execution of Update after the MonoBehaviour is created



    // Start is called once before the first execution of Update after the MonoBehaviour is created
   public override void Interact(Player player)
    {
        if (!HasKitchenObject())
        {
            if (player.HasKitchenObject())
            {
                player.GetKitchenObject().SetKitchenObjectParent(this);
            } else
            {
                
            }
        }
        else
        {
           if (player.HasKitchenObject())
            {
            }
            else
            {
                GetKitchenObject().SetKitchenObjectParent(player);
            }
        }
    }


}
