using UnityEngine;

namespace Runtime.Context.Game.Scripts.Models.Bundle
{
  public class InventoryTest: MonoBehaviour
  {
    private Inventory _inventory;

    [SerializeField]
    private UI_Inventory uiInventory;

    private void Awake()
    {
      _inventory = new Inventory();
      uiInventory.SetInventory(_inventory);
      
    }
  }
}