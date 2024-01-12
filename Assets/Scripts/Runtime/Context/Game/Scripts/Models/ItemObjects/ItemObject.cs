using UnityEngine;

namespace Runtime.Context.Game.Scripts.Models.ItemObjects
{
  public enum ItemType
  {
    Default,
    Wrench,
    Stone,
    Stick,
    Axe,
    Hammer,
    Sword
  }

  public abstract class ItemObject : ScriptableObject
  {
    public GameObject prefab;
    public ItemType type;
    public string itemName;
    public int damage;

    [TextArea(15, 20)]
    public string description;

    public bool isWeapon;
    public GameObject weaponButton;
  }
}