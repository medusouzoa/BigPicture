using Runtime.Context.Game.Scripts.Models.LayerModel;
using strange.extensions.mediation.impl;

namespace Runtime.Context.Game.Scripts.View.Layer
{
  public class LayerMediator : EventMediator
  {
    [Inject]
    public ILayerModel layerModel { get; set; }

    [Inject]
    public LayerView view { get; set; }

    public override void OnRegister()
    {
      layerModel.AddLayer(view.type, view.container);
    }

    public override void OnRemove()
    {
    }
  }
}