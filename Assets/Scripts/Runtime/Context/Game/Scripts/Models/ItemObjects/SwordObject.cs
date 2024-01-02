using UnityEngine;

namespace Runtime.Context.Game.Scripts.Models.ItemObjects
{
  [CreateAssetMenu(fileName = "New Sword Object", menuName = "Inventory System/Items/Sword")]
  public class SwordObject : ItemObject
  {
    public void Awake()
    {
      type = ItemType.Sword;
      itemName = ItemType.Sword.ToString();
    }
  }
}