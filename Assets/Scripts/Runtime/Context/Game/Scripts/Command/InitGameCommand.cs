using Runtime.Context.Game.Scripts.Enum;
using Runtime.Context.Game.Scripts.Models.InventoryModel;
using Runtime.Context.Game.Scripts.Models.LayerModel;
using Runtime.Context.Game.Scripts.Models.Panel;
using strange.extensions.command.impl;
using UnityEngine;

namespace Runtime.Context.Game.Scripts.Command
{
  public class InitGameCommand : EventCommand
  {
    [Inject]
    public ILayerModel layerModel { get; set; }

    [Inject]
    public IInventoryModel inventoryModel { get; set; }

    [Inject]
    public IPanelModel panelModel { get; set; }


    public override void Execute()
    {
      Transform parent = layerModel.GetLayer(Layers.Hud);
      panelModel.LoadPanel("WelcomePanel", parent);
      inventoryModel.InitializeDatabase();
 
    }
  }
}