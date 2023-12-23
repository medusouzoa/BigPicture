using System.Collections.Generic;
using Runtime.Context.Game.Scripts.Enum;
using Runtime.Context.Game.Scripts.Vo;
using strange.extensions.context.api;
using strange.extensions.dispatcher.eventdispatcher.api;

namespace Runtime.Context.Game.Scripts.Models.GameModel
{
  public class GameModel : IGameModel
  {
    [Inject(ContextKeys.CONTEXT_DISPATCHER)]
    public IEventDispatcher dispatcher { get; set; }



    public List<List<TileGridVo>> grids { get; private set; }
    public int col { get; set; }

    [PostConstruct]
    public void OnPostConstruct()
    {
      grids = new List<List<TileGridVo>>();
      //   _uiInventory.SetInventory(inventory);
      /* ItemWorld.SpawnItemWorld(new Vector3(20, 20), new Item {itemType = Item.ItemType.Stone, amount = 1});
       ItemWorld.SpawnItemWorld(new Vector3(-20, 20), new Item {itemType = Item.ItemType.Stick, amount = 1});
       ItemWorld.SpawnItemWorld(new Vector3(0, -20), new Item {itemType = Item.ItemType.Wrench, amount = 1});
     */
    }

    

    public void Setup(int n)
    {
      col = n;
      dispatcher.Dispatch(GameEvent.CreateGrid);
      FillGridData();
    }

    private void FillGridData()
    {
      for (int i = 0; i < col; i++)
      {
        grids.Add(new List<TileGridVo>());
        for (int j = 0; j < col; j++)
        {
          List<TileGridVo> gridVos = grids[i];
          gridVos.Add(new TileGridVo
          {
            x = i,
            y = j
          });
        }
      }
    }

    public void StartGame()
    {
      dispatcher.Dispatch(GameEvent.GameReady);
    }
  }
}