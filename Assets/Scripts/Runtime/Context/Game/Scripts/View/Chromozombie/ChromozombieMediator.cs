using Runtime.Context.Game.Scripts.Models.CameraModel;
using Runtime.Context.Game.Scripts.Vo;
using strange.extensions.mediation.impl;
using UnityEngine;

namespace Runtime.Context.Game.Scripts.View.Chromozombie
{
  public class ChromozombieMediator : EventMediator
  {
    [Inject]
    public ChromozombieView view { get; set; }

    [Inject]
    public ICameraModel cameraModel { get; set; }


    public Camera cam;

    //public GameObject damagePanel;
    public override void OnRegister()
    {
      cam = cameraModel.GetCameraByKey("1");
    }

    void Update()
    {
      if (Input.GetMouseButtonDown(0)) // Left mouse button click
      {
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, Mathf.Infinity, view.zombieLayer))
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
}