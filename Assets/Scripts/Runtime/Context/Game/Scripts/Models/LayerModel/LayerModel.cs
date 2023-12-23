using System.Collections.Generic;
using Runtime.Context.Game.Scripts.Enum;
using UnityEngine;

namespace Runtime.Context.Game.Scripts.Models.LayerModel
{
  public class LayerModel : ILayerModel
  {
    private Dictionary<Layers, Transform> _layers;

    [PostConstruct] //bunu nereye yazarsak altındaki ilk çalışır.
    public void OnPostConstruct()
    {
      _layers = new Dictionary<Layers, Transform>();
    }

    public void AddLayer(Layers key, Transform value)
    {
      Debug.Log("LayerModel>AddLayer> key: " + key + " value: " + value);
      _layers[key] = value;
    }

    public Transform GetLayer(Layers key)
    {
      if (!_layers.ContainsKey(key)) //önce olumsuz koşulları yaz
      {
      //  Debug.LogError(" Player not found in layer map" + key);
        return null;
      }

      Transform transformCont = _layers[key];
      Debug.Log("LayerModel>GetLayer> key: " + key + " value: " + transformCont);

      return transformCont;
    }
  }
}