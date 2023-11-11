using Runtime.Context.Game.Scripts.Models.Bundle;
using strange.extensions.mediation.impl;
using UnityEngine;

namespace Runtime.Context.Game.Scripts.View.Inventory
{
  public class InventoryMediator: EventMediator
  {
    [Inject] 
    public InventoryView view { get; set; }

    public override void OnRegister()
    {
      view.uiInventory = new UI_Inventory();
      
    }
    
  }
}