using UnityEngine;

namespace Runtime.Context.Game.Scripts.Models.ItemObjects
{
  [CreateAssetMenu(fileName = "New Axe Object", menuName = "Inventory System/Items/Axe")]
  public class AxeObject : ItemObject
  {
    public void Awake()
    {
      type = ItemType.Axe;
      itemName = ItemType.Axe.ToString();
    }
  }
}