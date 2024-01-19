using System.Collections;
using System.Collections.Generic;
using Runtime.Context.Game.Scripts.Vo;
using UnityEngine;

public class GameManager : MonoBehaviour
{
  public GameObject gameOverPanel;

  public void OnPlayerDeath()
  {
    if (gameOverPanel != null)
    {
      gameOverPanel.SetActive(true);
    }
  }
}