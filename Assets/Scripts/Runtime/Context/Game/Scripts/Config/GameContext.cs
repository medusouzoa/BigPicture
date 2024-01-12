using Runtime.Context.Game.Scripts.Command;
using Runtime.Context.Game.Scripts.Models.Bundle;
using Runtime.Context.Game.Scripts.Models.CameraModel;
using Runtime.Context.Game.Scripts.Models.DamageModel;
using Runtime.Context.Game.Scripts.Models.Database;
using Runtime.Context.Game.Scripts.Models.GameModel;
using Runtime.Context.Game.Scripts.Models.Grid;
using Runtime.Context.Game.Scripts.Models.InventoryModel;
using Runtime.Context.Game.Scripts.Models.LayerModel;
using Runtime.Context.Game.Scripts.Models.Panel;
using Runtime.Context.Game.Scripts.Models.Pathfinding;
using Runtime.Context.Game.Scripts.View.Chromozombie;
using Runtime.Context.Game.Scripts.View.Craft;
using Runtime.Context.Game.Scripts.View.CraftRow;
using Runtime.Context.Game.Scripts.View.GridManager;
using Runtime.Context.Game.Scripts.View.GridTest;
using Runtime.Context.Game.Scripts.View.Inventory;
using Runtime.Context.Game.Scripts.View.Layer;
using Runtime.Context.Game.Scripts.View.MainHud;
using Runtime.Context.Game.Scripts.View.Market;
using Runtime.Context.Game.Scripts.View.Player;
using Runtime.Context.Game.Scripts.View.SceneCameras;
using Runtime.Context.Game.Scripts.View.WeaponPanel;
using Runtime.Context.Game.Scripts.View.Welcome;
using strange.extensions.context.api;
using strange.extensions.context.impl;
using UnityEngine;

namespace Runtime.Context.Game.Scripts.Config
{
  public class GameContext : MVCSContext
  {
    public GameContext(MonoBehaviour view) : base(view)
    {
    }

    protected override void mapBindings()
    {
      base.mapBindings();

      injectionBinder.Bind<IGameModel>().To<GameModel>().ToSingleton();
      injectionBinder.Bind<IPathfindingService>().To<PathfindingService>().ToSingleton();
      injectionBinder.Bind<IGridModel>().To<GridModel>().ToSingleton();
      injectionBinder.Bind<ILayerModel>().To<LayerModel>().ToSingleton();
      injectionBinder.Bind<IPanelModel>().To<PanelModel>().ToSingleton();
      injectionBinder.Bind<IDatabaseModel>().To<DatabaseModel>().ToSingleton();
      injectionBinder.Bind<IInventoryModel>().To<InventoryModel>().ToSingleton();
      injectionBinder.Bind<ICameraModel>().To<CameraModel>().ToSingleton();
      injectionBinder.Bind<IDamageModel>().To<DamageModel>().ToSingleton();

      injectionBinder.Bind<BundleFacade>().To<BundleFacade>().ToSingleton();

      mediationBinder.Bind<GridManagerView>().To<GridManagerMediator>();
      mediationBinder.Bind<GridTestView>().To<GridTestMediator>();
      mediationBinder.Bind<InventoryView>().To<InventoryMediator>();
      mediationBinder.Bind<LayerView>().To<LayerMediator>();
      mediationBinder.Bind<WelcomePanelView>().To<WelcomePanelMediator>();
      mediationBinder.Bind<MainHudView>().To<MainHudMediator>();
      mediationBinder.Bind<CraftView>().To<CraftMediator>();
      mediationBinder.Bind<CraftRowView>().To<CraftRowMediator>();
      mediationBinder.Bind<PlayerView>().To<PlayerMediator>();
      mediationBinder.Bind<MarketView>().To<MarketMediator>();
      mediationBinder.Bind<CameraView>().To<CameraMediator>();
      mediationBinder.Bind<ChromozombieView>().To<ChromozombieMediator>();
      mediationBinder.Bind<WeaponPanelView>().To<WeaponPanelMediator>();

      commandBinder.Bind(ContextEvent.START).To<InitGameCommand>();
    }
  }
}