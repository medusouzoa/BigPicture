using UnityEngine;

namespace Runtime.Context.Game.Scripts.Models.ItemObjects
{
  [CreateAssetMenu(fileName = "New Stick Object", menuName = "Inventory System/Items/Stick")]
  public class StickObject : ItemObject
  {
    public void Awake()
    {
      type = ItemType.Stick;
      itemName = ItemType.Stick.ToString();
    }
  }
}