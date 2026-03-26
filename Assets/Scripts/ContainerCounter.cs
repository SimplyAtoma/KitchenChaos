using System;
using UnityEngine;

public class ContainerCounter : BaseCounter 
{
    public event EventHandler OnPlayerGrabbedObject;
    [SerializeField] private KitchenObjectSO kitchenObjectSO;
    // Start is called once before the first execution of Update after the MonoBehaviour is created


    public override void Interact(Player player)
    {
        if(!player.HasKitchenObject()){
        KitchenObject.SpawnKitchenObject(kitchenObjectSO,player);
        OnPlayerGrabbedObject?.Invoke(this, EventArgs.Empty);
        }
    }

}
