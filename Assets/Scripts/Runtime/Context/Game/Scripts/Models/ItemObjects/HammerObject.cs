using UnityEngine;

namespace Runtime.Context.Game.Scripts.Models.ItemObjects
{
  [CreateAssetMenu(fileName = "New Hammer Object", menuName = "Inventory System/Items/Hammer")]
  public class HammerObject : ItemObject
  {
    public void Awake()
    {
      type = ItemType.Hammer;
      itemName = ItemType.Hammer.ToString();
    }
  }
}