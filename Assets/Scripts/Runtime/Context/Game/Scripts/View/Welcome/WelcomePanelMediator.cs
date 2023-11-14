using Runtime.Context.Game.Scripts.Enum;
using Runtime.Context.Game.Scripts.Models.GameModel;
using Runtime.Context.Game.Scripts.Models.LayerModel;
using Runtime.Context.Game.Scripts.Models.Panel;
using strange.extensions.mediation.impl;
using UnityEngine;

namespace Runtime.Context.Game.Scripts.View.Welcome
{
  public enum WelcomePanelEvent
  {
    Start,
  }

  public class WelcomePanelMediator : EventMediator
  {
    [Inject]
    public WelcomePanelView view { get; set; }

    [Inject]
    public ILayerModel layerModel { get; set; }

    [Inject]
    public IPanelModel panelModel { get; set; }

    [Inject]
    public IGameModel gameModel { get; set; }

    public override void OnRegister()
    {
      view.dispatcher.AddListener(WelcomePanelEvent.Start, OnCheckAllDone);
    }

    private void OnCheckAllDone()
    {
      Transform parent = layerModel.GetLayer(Layers.InGameLayer);
      panelModel.LoadPanel(GamePanels.GamePanel, parent);
      Destroy(gameObject);

      parent = layerModel.GetLayer(Layers.Hud);
      panelModel.LoadPanel(GamePanels.InGameHud, parent)
        .Then(() => { gameModel.StartGame(); });
    }
  }
}

