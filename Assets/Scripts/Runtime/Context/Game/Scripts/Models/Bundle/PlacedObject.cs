﻿using System.Collections.Generic;
using Runtime.Context.Game.Scripts.ObjectPlacingObject;
using Runtime.Context.Game.Scripts.Vo;
using UnityEngine;

namespace Runtime.Context.Game.Scripts.Models.Bundle
{
  public class PlacedObject : MonoBehaviour
  {
    private PlacedObjectType _placedObjectType;
    private Vector2Int _origin;
    private PlacedObjectType.Dir _dir;

    public static PlacedObject Create(Vector3 worldPosition, Vector2Int origin, PlacedObjectType.Dir dir, PlacedObjectType placedObjectType, PlayerData player)
    {
      if (player.money >= placedObjectType.price)
      {
        player.SubtractMoney(placedObjectType.price);
        Debug.Log("purchased the item");
        GameObject placedObjectTransform = Instantiate(placedObjectType.prefab, worldPosition, Quaternion.Euler(0, placedObjectType.GetRotationAngle(dir), 0));
        PlacedObject placedObject = placedObjectTransform.GetComponent<PlacedObject>();

        if (placedObject != null)
        {
          placedObject._placedObjectType = placedObjectType;
          placedObject._origin = origin;
          placedObject._dir = dir;
        }
        else
        {
        }

        return placedObject;
      }
      else
      {
        Debug.LogWarning("Player doesn't have enough money to buy the item.");

        return null;
      }
    }


    public List<Vector2Int> GetGridPositionList()
    {
      return _placedObjectType.GetGridPositionList(_origin, _dir);
    }

    public void DestroySelf()
    {
      Destroy(gameObject);
    }
  }
}