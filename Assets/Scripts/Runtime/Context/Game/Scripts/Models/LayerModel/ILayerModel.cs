using Runtime.Context.Game.Scripts.Enum;
using UnityEngine;

namespace Runtime.Context.Game.Scripts.Models.LayerModel
{
  public interface ILayerModel
  {
    void AddLayer(Layers key, Transform value);
    Transform GetLayer(Layers key);
  }
}