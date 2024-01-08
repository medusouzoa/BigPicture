using System;
using Runtime.Context.Game.Scripts.Models.Bundle;
using Runtime.Context.Game.Scripts.Models.CameraModel;
using Runtime.Context.Game.Scripts.Vo;
using UnityEngine;

public class ClickHandler : MonoBehaviour
{
  private ICameraModel _cameraModel;
  public LayerMask zombieLayer;

  private Camera cam;

  //public GameObject damagePanel;
  private void Start()
  {
    cam = _cameraModel.GetCameraByKey("1");
  }

  void Update()
  {
    if (Input.GetMouseButtonDown(0)) // Left mouse button click
    {
      Ray ray = cam.ScreenPointToRay(Input.mousePosition);
      RaycastHit hit;

      if (Physics.Raycast(ray, out hit, Mathf.Infinity, zombieLayer))
      {
        ZombieStats zombie = hit.collider.GetComponent<ZombieStats>();

        if (zombie != null)
        {
          // Open UI panel to choose melee weapon
          // damagePanel.SetActive(true);
          // Pass the selected zombie to the UI panel for further processing
          zombie.TakeDamage(100);
          Debug.Log(zombie.health);
          Debug.Log("zombie clicked");
        }
      }
    }
  }
}