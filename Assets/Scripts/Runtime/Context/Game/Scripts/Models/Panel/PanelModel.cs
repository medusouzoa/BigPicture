using RSG;
using Runtime.Context.Game.Scripts.Models.Bundle;
using UnityEngine;

namespace Runtime.Context.Game.Scripts.Models.Panel
{
  public class PanelModel : IPanelModel
  {
    [Inject]
    public BundleFacade bundleFacade { get; set; }

    public IPromise LoadPanel(string key, Transform parent)
    {
      Promise promise = new();
      bundleFacade.Instantiate(key, parent)
        .Then(() => promise.Resolve())
        .Catch(exception => promise.Reject(exception));
      return promise;
    }
    
  }
}