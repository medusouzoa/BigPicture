using UnityEngine;

namespace Runtime.Context.Game.Scripts.Vo
{
  public class ItemAssetsVo
  {
    public static ItemAssetsVo Instance { get; private set; }

    private void Awake()
    {
      Instance = this;
    }

    public Transform pfItemWorld;
    public Sprite wrench;
    public Sprite stone;
    public Sprite stick;
  }
}