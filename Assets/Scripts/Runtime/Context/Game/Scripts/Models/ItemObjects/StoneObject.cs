using UnityEngine;

namespace Runtime.Context.Game.Scripts.Models.ItemObjects
{
  [CreateAssetMenu(fileName = "New Stone Object", menuName = "Inventory System/Items/Stone")]
  public class StoneObject : ItemObject
  {
    public void Awake()
    {
      type = ItemType.Stone;
      itemName = ItemType.Stone.ToString();

    }
  }
}