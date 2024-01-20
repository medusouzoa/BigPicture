using System.Collections.Generic;
using Runtime.Context.Game.Scripts.Enum;
using UnityEngine;

namespace Runtime.Context.Game.Scripts.Models.LayerModel
{
  public class LayerModel : ILayerModel
  {
    private Dictionary<Layers, Transform> _layers;

    [PostConstruct]
    public void OnPostConstruct()
    {
      _layers = new Dictionary<Layers, Transform>();
    }

    public void AddLayer(Layers key, Transform value)
    {
      _layers[key] = value;
    }

    public Transform GetLayer(Layers key)
    {
      if (!_layers.ContainsKey(key))
      {
        return null;
      }

      Transform transformCont = _layers[key];

      return transformCont;
    }
  }
}