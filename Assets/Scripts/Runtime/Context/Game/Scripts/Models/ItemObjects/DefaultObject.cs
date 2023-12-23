using System;
using Runtime.Context.Game.Scripts.Models.Bundle;
using UnityEngine;

namespace Runtime.Context.Game.Scripts.Models.ItemObjects
{
[CreateAssetMenu(fileName = "New Default Object",menuName = "Inventory System/Items/Default")]
    
  public class DefaultObject: ItemObject
  {
    public void Awake()
    {
      type = ItemType.Default;
      itemName = ItemType.Default.ToString();
    }
  }
}