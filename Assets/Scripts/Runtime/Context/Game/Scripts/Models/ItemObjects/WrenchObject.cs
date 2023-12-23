using UnityEngine;

namespace Runtime.Context.Game.Scripts.Models.ItemObjects
{
  [CreateAssetMenu(fileName = "New Wrench Object", menuName = "Inventory System/Items/Wrench")]
  public class WrenchObject : ItemObject
  {
    public void Awake()
    {
      type = ItemType.Wrench;
      itemName = ItemType.Wrench.ToString();
    }
  }
}