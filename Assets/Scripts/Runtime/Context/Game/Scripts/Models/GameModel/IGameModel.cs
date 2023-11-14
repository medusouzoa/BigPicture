using System.Collections.Generic;
using Runtime.Context.Game.Scripts.Vo;

namespace Runtime.Context.Game.Scripts.Models.GameModel
{
  public interface IGameModel
  {
    List<List<TileGridVo>> grids { get; }
    
    void Setup(int n);

    void StartGame();
    void OnPostConstruct();
  }
}