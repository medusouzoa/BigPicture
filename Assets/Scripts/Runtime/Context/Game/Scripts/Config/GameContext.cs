using Runtime.Context.Game.Scripts.Command;
using Runtime.Context.Game.Scripts.Models.Bundle;
using Runtime.Context.Game.Scripts.Models.GameModel;
using Runtime.Context.Game.Scripts.Models.Grid;
using Runtime.Context.Game.Scripts.Models.LayerModel;
using Runtime.Context.Game.Scripts.Models.Panel;
using Runtime.Context.Game.Scripts.Models.Pathfinding;
using Runtime.Context.Game.Scripts.View.Game;
using Runtime.Context.Game.Scripts.View.GridManager;
using Runtime.Context.Game.Scripts.View.GridTest;
using Runtime.Context.Game.Scripts.View.Inventory;
using Runtime.Context.Game.Scripts.View.Layer;
using Runtime.Context.Game.Scripts.View.Tile;
using Runtime.Context.Game.Scripts.View.TileManager;
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

      injectionBinder.Bind<BundleFacade>().To<BundleFacade>().ToSingleton();

      mediationBinder.Bind<GridManagerView>().To<GridManagerMediator>();
      mediationBinder.Bind<GridTestView>().To<GridTestMediator>();
      mediationBinder.Bind<InventoryView>().To<InventoryMediator>();
      mediationBinder.Bind<LayerView>().To<LayerMediator>();
      mediationBinder.Bind<TileManagerView>().To<TileManagerMediator>();
      mediationBinder.Bind<TileView>().To<TileMediator>();
      mediationBinder.Bind<WelcomePanelView>().To<WelcomePanelMediator>();
      mediationBinder.Bind<GamePanelView>().To<GamePanelMediator>();

      commandBinder.Bind(ContextEvent.START).To<InitGameCommand>();
    }
  }
}