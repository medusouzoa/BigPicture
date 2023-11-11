using RSG;
using UnityEngine;

namespace Runtime.Context.Game.Scripts.Models.Panel
{
  public interface IPanelModel
  {
    IPromise LoadPanel(string key, Transform parent);
  }
}