using System.Collections.Generic;
using UnityEngine;

namespace Runtime.Context.Game.Scripts.Models.CraftBookObject
{
  [CreateAssetMenu(fileName = "CraftBook", menuName = "Craft System/CraftBook")]
  public class CraftBookObject : ScriptableObject
  {
    public List<CraftRecipeObject> recipes = new List<CraftRecipeObject>();
    public GameObject prefab;

    public bool ContainsRecipe(List<IngredientSlot> ingredients)
    {
      foreach (CraftRecipeObject recipe in recipes)
      {
        if (CheckIngredients(recipe, ingredients))
        {
          return true;
        }
      }

      return false;
    }

    private bool CheckIngredients(CraftRecipeObject recipe, List<IngredientSlot> ingredients)
    {
      foreach (IngredientSlot ingredient in recipe.ingredients)
      {
        if (!ingredients.Contains(ingredient))
        {
          return false;
        }
      }

      return true;
    }
  }
}