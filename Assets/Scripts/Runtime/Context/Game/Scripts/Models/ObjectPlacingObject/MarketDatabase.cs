using System.Collections.Generic;
using Runtime.Context.Game.Scripts.Vo;
using UnityEngine;

namespace Runtime.Context.Game.Scripts.Models.ObjectPlacingObject
{
  [CreateAssetMenu]
  public class MarketDatabase : ScriptableObject
  {
    public List<MarketData> marketData;
  }
}