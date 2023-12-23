// using System;
// using System.Collections.Generic;
// using Runtime.Context.Game.Scripts.ObjectPlacingObject;
// using UnityEngine;
//
// namespace Runtime.Context.Game.Scripts.Vo
// {
//   public class PlacementSystem : MonoBehaviour
//   {
//     [SerializeField]
//     private GameObject mouseIndicator, cellIndicator;
//
//     [SerializeField]
//     private InputManager inputManager;
//
//     [SerializeField]
//     private Grid grid;
//
//     [SerializeField]
//     private ObjectsDatabase database;
//
//     private int selectedObjectIndex = -1;
//
//     [SerializeField]
//     private GameObject gridVisualization;
//
//     [SerializeField]
//     private AudioClip correctPlacementClip, wrongPlacementClip;
//
//     [SerializeField]
//     private AudioSource source;
//
//     private GridData _floorData, _furnitureData;
//     private Renderer _previewRenderer;
//     public List<GameObject> placedGameObj = new();
//
// /*
//
//
//     [SerializeField]
//     private PreviewSystem preview;
//
//     private Vector3Int lastDetectedPosition = Vector3Int.zero;
//
//     [SerializeField]
//     private ObjectPlacer objectPlacer;
//
//     IBuildingState buildingState;
//
//     [SerializeField]
//     private SoundFeedback soundFeedback;
// */
//     private void Start()
//     {
//       StopPlacement();
//       gridVisualization.SetActive(false);
//       _floorData = new();
//       _furnitureData = new();
//       _previewRenderer = cellIndicator.GetComponentInChildren<Renderer>();
//     }
//
//     public void StartPlacement(int ID)
//     {
//       StopPlacement();
//       selectedObjectIndex = database.objectsData.FindIndex(data => data.ID == ID);
//       if (selectedObjectIndex < 0)
//       {
//         Debug.LogError($"No ID found {ID}");
//         return;
//       }
//
//       gridVisualization.SetActive(true);
//       cellIndicator.SetActive(true);
//       /*buildingState = new PlacementState(ID,
//         grid,
//         preview,
//         database,
//         floorData,
//         furnitureData,
//         objectPlacer,
//         soundFeedback);*/
//       inputManager.OnClicked += PlaceStructure;
//       inputManager.OnExit += StopPlacement;
//     }
//
//     public void StartRemoving()
//     {
//       StopPlacement();
//       gridVisualization.SetActive(true);
//       // buildingState = new RemovingState(grid, preview, floorData, furnitureData, objectPlacer, soundFeedback);
//       inputManager.OnClicked += PlaceStructure;
//       inputManager.OnExit += StopPlacement;
//     }
//
//     private void PlaceStructure()
//     {
//       if (inputManager.IsPointerOverUI())
//       {
//         return;
//       }
//
//       Vector3 mousePosition = inputManager.GetSelectedMapPosition();
//       Vector3Int gridPosition = grid.WorldToCell(mousePosition);
//       bool placementValidity = CheckPlacementValidity(gridPosition, selectedObjectIndex);
//       if (placementValidity == false)
//       {
//         return;
//       }
//
//       GameObject newObj = Instantiate(database.objectsData[selectedObjectIndex].Prefab);
//
//       newObj.transform.position = grid.CellToWorld(gridPosition);
//       placedGameObj.Add(newObj);
//       GridData selectedData = database.objectsData[selectedObjectIndex].ID == 0 ? _floorData : _furnitureData;
//       selectedData.AddObjectAt(gridPosition, database.objectsData[selectedObjectIndex].Size,
//         database.objectsData[selectedObjectIndex].ID,
//         placedGameObj.Count - 1);
//       // buildingState.OnAction(gridPosition);
//     }
//
//     private bool CheckPlacementValidity(Vector3Int gridPosition, int selectedObjectIndex)
//     {
//       GridData selectedData = database.objectsData[selectedObjectIndex].ID == 0 ? _floorData : _furnitureData;
//
//       return selectedData.CanPlaceObjectAt(gridPosition, database.objectsData[selectedObjectIndex].Size);
//     }
//
//     private void StopPlacement()
//     {
//       selectedObjectIndex = -1;
//       // soundFeedback.PlaySound(SoundType.Click);
//       // if (buildingState == null)return;
//       gridVisualization.SetActive(false);
//       cellIndicator.SetActive(false);
//       // buildingState.EndState();
//       inputManager.OnClicked -= PlaceStructure;
//       inputManager.OnExit -= StopPlacement;
//       // lastDetectedPosition = Vector3Int.zero;
//       // buildingState = null;
//     }
//
//     private void Update()
//     {
//       if (selectedObjectIndex < 0) return;
//       // if (buildingState == null)return;
//       Vector3 mousePosition = inputManager.GetSelectedMapPosition();
//       Vector3Int gridPosition = grid.WorldToCell(mousePosition);
//
//       bool placementValidity = CheckPlacementValidity(gridPosition, selectedObjectIndex);
//       _previewRenderer.material.color = placementValidity ? Color.blue : Color.red;
//
//       mouseIndicator.transform.position = mousePosition;
//       cellIndicator.transform.position = grid.CellToWorld(gridPosition);
//       /*if (lastDetectedPosition != gridPosition)
//       {
//         buildingState.UpdateState(gridPosition);
//         lastDetectedPosition = gridPosition;
//       }*/
//     }
//   }
// }