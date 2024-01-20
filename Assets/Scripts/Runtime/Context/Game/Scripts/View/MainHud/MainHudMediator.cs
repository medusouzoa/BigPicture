using Runtime.Context.Game.Scripts.Enum;
using Runtime.Context.Game.Scripts.Models.LayerModel;
using Runtime.Context.Game.Scripts.Models.Panel;
using Runtime.Context.Game.Scripts.Models.PlayerModel;
using strange.extensions.mediation.impl;
using UnityEngine;

namespace Runtime.Context.Game.Scripts.View.MainHud
{
  public enum MainHudEvent
  {
    None,
    Market,
    Craft,
    Inventory,
    Settings,
    Close,
  }

  public class MainHudMediator : EventMediator
  {
    [Inject]
    public MainHudView view { get; set; }

    [Inject]
    public ILayerModel layerModel { get; set; }

    [Inject]
    public IPanelModel panelModel { get; set; }

    [Inject]
    public IPlayerModel playerModel { get; set; }

    public override void OnRegister()
    {
      view.dispatcher.AddListener(MainHudEvent.Market, OnMarketOpen);
      view.dispatcher.AddListener(MainHudEvent.Inventory, OnInventoryOpen);
      view.dispatcher.AddListener(MainHudEvent.Craft, OnCraftOpen);
      view.dispatcher.AddListener(MainHudEvent.Settings, OnSettingsOpen);
      view.dispatcher.AddListener(MainHudEvent.Close, OnClose);
      view.UpdateValue(playerModel.money);
    }

    private void Update()
    {
      view.UpdateValue(playerModel.money);
    }

    private void OnMarketOpen()
    {
      Transform parent = layerModel.GetLayer(Layers.Hud);
      panelModel.LoadPanel(GamePanels.MarketPanel, parent);
    }

    private void OnInventoryOpen()
    {
      Transform parent = layerModel.GetLayer(Layers.Hud);
      panelModel.LoadPanel(GamePanels.InventoryPanel, parent);
    }

    private void OnCraftOpen()
    {
      Transform parent = layerModel.GetLayer(Layers.Hud);
      panelModel.LoadPanel(GamePanels.CraftPanel, parent);
    }

    private void OnSettingsOpen()
    {
      Transform parent = layerModel.GetLayer(Layers.Hud);
      panelModel.LoadPanel(GamePanels.SettingsPanel, parent);
    }

    private void OnClose()
    {
      Destroy(gameObject);
    }
  }
}