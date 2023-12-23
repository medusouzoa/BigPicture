using System;
using Runtime.Context.Game.Scripts.Models.Bundle;
using Runtime.Context.Game.Scripts.Models.Database;
using Runtime.Context.Game.Scripts.Models.ItemObjects;
using UnityEngine;

namespace Runtime.Context.Game.Scripts.Models.InventoryObject
{
  public class Player : MonoBehaviour
  {
    public InventoryObject inventory;

    [Inject]
    public IDatabaseModel databaseModel { get; set; }

    public void OnTriggerEnter(Collider other)
    {
      
      Item item = other.GetComponent<Item>();
      if (item)
      {
        inventory.AddItem(item.item, 1);
        StartCoroutine(web.SaveItem(item.item.name, 1));
        Destroy(other.gameObject);
      }
    }

    private void OnApplicationQuit()
    {
      inventory.container.Clear();
    }
  }
}