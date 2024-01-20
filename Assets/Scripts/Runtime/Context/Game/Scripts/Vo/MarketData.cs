using System;
using Runtime.Context.Game.Scripts.Models.ObjectPlacingObject;
using UnityEngine;

namespace Runtime.Context.Game.Scripts.Vo
{
  [Serializable]
  public class MarketData
  {
    [field: SerializeField]
    public string Name { get; private set; }

    [field: SerializeField]
    public Vector2Int Size { get; private set; } = Vector2Int.one;

    [field: SerializeField]
    public GameObject Prefab { get; private set; }

    [field: SerializeField]
    public PlacedObjectType placedObjectType;

    [field: SerializeField]
    public GameObject itemImage { get; private set; }
  }
}