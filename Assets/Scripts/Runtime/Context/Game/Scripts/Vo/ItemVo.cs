using UnityEngine;

namespace Runtime.Context.Game.Scripts.Vo
{
  public class ItemVo
  {
    public enum ItemType
    {
      None,
      Wrench,
      Stone,
      Stick
    }

    public ItemType itemType;
    public int amount;

    public Sprite GetSprite()
    {
      switch (itemType)
      {
        default:
        case ItemType.Stick: return ItemAssetsVo.Instance.stick;
        case ItemType.Wrench: return ItemAssetsVo.Instance.wrench;
        case ItemType.Stone: return ItemAssetsVo.Instance.stone;
      }
    }
  }
}