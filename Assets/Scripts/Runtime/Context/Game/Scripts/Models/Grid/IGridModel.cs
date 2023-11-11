using System.Collections.Generic;
using Runtime.Context.Game.Scripts.Vo;
using UnityEngine;

namespace Runtime.Context.Game.Scripts.Models.Grid
{
  public interface IGridModel
  {
    /// <summary>
    /// It creates grid data based on a given parameter and then triggers an event when it is "ready".
    /// </summary>
    /// <param name="vo">It includes grid settings such as height, width, cell size, etc.</param>
    void Create(GridSettingsVo vo);

    /// <summary>
    /// It returns all grid information as a list.
    /// </summary>
    /// <returns></returns>
    List<NodeVo> GetAllNodes();

    /// <summary>
    /// It finds the grid based on the center point of the grid.
    /// </summary>
    /// <param name="centerPosition">Grid center position</param>
    /// <returns></returns>
    NodeVo GetNodeByCenterPosition(Vector3 centerPosition);

    /// <summary>
    /// It adds a new row to the grid.
    /// </summary>
    void AddHeight();

    /// <summary>
    /// It returns whether there are any empty spaces in the grid table.
    /// </summary>
    /// <returns></returns>
    bool HasEmptyGrid();

    /// <summary>
    /// It returns a randomly empty grid.
    /// </summary>
    /// <returns></returns>
    NodeVo GetRandomEmptyPoint();

    /// <summary>
    /// It returns the height of the grid by current grid settings.
    /// </summary>
    /// <returns></returns>
    int GetHeight();

    int GetWidth();
    NodeVo GetNodeByWorldPosition(Vector3 worldPosition);
    NodeVo GetGridObject(int x, int y);
  }
}