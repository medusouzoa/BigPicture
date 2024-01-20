using UnityEngine;

public class GameManager : MonoBehaviour
{
  public GameObject gameOverPanel;
  public GameObject hudLayer;

  public void OnPlayerDeath()
  {
    if (gameOverPanel != null)
    {
      gameOverPanel.SetActive(true);
      hudLayer.SetActive(false);
    }
  }
}