using UnityEngine;

namespace Runtime.Context.Game.Scripts.Models.ItemObjects
{
  public enum ItemType
  {
    Default,
    Wrench,
    Stone,
    Stick
  }

  public abstract class ItemObject : ScriptableObject
  {
    public GameObject prefab;
    public ItemType type;
    public string itemName;
    [TextArea(15,20)]
    public string description;
  }
}