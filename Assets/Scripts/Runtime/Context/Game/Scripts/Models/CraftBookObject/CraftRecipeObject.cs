using System.Collections.Generic;
using Runtime.Context.Game.Scripts.Models.ItemObjects;
using UnityEngine;

namespace Runtime.Context.Game.Scripts.Models.CraftBookObject
{
  [CreateAssetMenu(fileName = "CraftRecipe", menuName = "Craft System/CraftRecipe")]
  public class CraftRecipeObject : ScriptableObject
  {
    public List<IngredientSlot> ingredients = new List<IngredientSlot>();
    public ItemObject result;
    public GameObject prefab;
    public string recipeName;
  }

  [System.Serializable]
  public class IngredientSlot
  {
    public ItemObject item;
    public int amount;

    public IngredientSlot(ItemObject item, int amount)
    {
      this.item = item;
      this.amount = amount;
    }
  }
}