using strange.extensions.mediation.impl;
using TMPro;
using UnityEngine;

namespace Runtime.Context.Game.Scripts.View.Craft
{
  public enum CraftEvent
  {
    None,
    Close
  }

  public class CraftMediator : EventMediator
  {
    [Inject]
    public CraftView view { get; set; }

    public override void OnRegister()
    {
      view.dispatcher.AddListener(CraftEvent.Close, OnClose);
      CreateDisplay();
    }


    public void CreateDisplay()
    {
      GameObject parent = GameObject.Find("Elements");

      if (parent == null)
      {
        Debug.LogError("Parent GameObject 'Elements' not found.");
        return;
      }

      // InstantiateCraft1(parent);
      for (int i = 0; i < view.craftBook.recipes.Count; i++)
      {
        GameObject obj = Instantiate(view.craftBook.recipes[i].prefab, parent.transform);

        for (int j = 0; j < view.craftBook.recipes[i].ingredients.Count; j++)
        {
          string itemName = "Item" + (j + 1);
          Transform itemTransform = obj.transform.Find(itemName);

          if (itemTransform != null)
          {
            GameObject newItemPrefab = view.craftBook.recipes[i].ingredients[j].item.prefab;
            newItemPrefab.GetComponentInChildren<TextMeshProUGUI>().text = view.craftBook.recipes[i].ingredients[j].amount.ToString("n0");

            Instantiate(newItemPrefab, itemTransform.position, itemTransform.rotation, itemTransform);
          }
          else
          {
            Debug.LogError($"The '{itemName}' GameObject was not found in the instantiated prefab hierarchy.");
          }
        }

        string craftedName = "CraftedItem";
        Transform craftedTransform = obj.transform.Find(craftedName);
        if (craftedTransform != null)
        {
          GameObject newItemPrefab = view.craftBook.recipes[i].result.prefab;
          Instantiate(newItemPrefab, craftedTransform.position, craftedTransform.rotation, craftedTransform);
        }
        else
        {
          Debug.LogError($"The '{craftedName}' GameObject was not found in the instantiated prefab hierarchy.");
        }
      }
    }

    /* public void InstantiateCraft1(GameObject parent)
     {
       GameObject obj = Instantiate(view.craftBook.recipes[0].prefab, parent.transform);
       for (int j = 0; j < view.craftBook.recipes[0].ingredients.Count; j++)
       {
         string itemName = "Item" + (j + 1);
         Transform itemTransform = obj.transform.Find(itemName);

         if (itemTransform != null)
         {
           GameObject newItemPrefab = view.craftBook.recipes[0].ingredients[j].item.prefab;
           newItemPrefab.GetComponentInChildren<TextMeshProUGUI>().text = view.craftBook.recipes[0].ingredients[j].amount.ToString("n0");

           Instantiate(newItemPrefab, itemTransform.position, itemTransform.rotation, itemTransform);
         }
         else
         {
           Debug.LogError($"The '{itemName}' GameObject was not found in the instantiated prefab hierarchy.");
         }
       }
     }*/

    private void OnClose()
    {
      Destroy(gameObject);
      Debug.Log("Close action called");
    }

    public override void OnRemove()
    {
      view.dispatcher.RemoveListener(CraftEvent.Close, OnClose);
    }
  }
}