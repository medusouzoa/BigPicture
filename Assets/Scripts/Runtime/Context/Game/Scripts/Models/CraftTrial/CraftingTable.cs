// using UnityEngine;
// using System;
// using System.Collections.Generic;
//
// namespace Runtime.Context.Game.Scripts.Models.CraftTrial
// {
//   [Serializable]
//   public static class CraftingTable
//   {
//     /* [Craftable] - Returns true or false if Inventory (or Inventories) contains enough materials to craft a given Recipe */
//     public static bool Craftable(Recipe recipe, InventoryObject.InventoryObject inventory)
//     {
//       // Set ingredient variables
//       Dictionary<int, int> ing = recipe.Ingredients.Contents;
//       List<int> ingList = new List<int>(ing.Keys);
//       int c = ingList.Count;
//
//       // Iterate through all ingredients in the recipe
//       for (int i = 0; i < c; i++)
//       {
//         // If the inventory contains the current Loot item, continue ...
//         if (inventory.container.Contains(ingList[i]))
//         {
//           // If the inventory's quantity of the current Loot item 
//           // is less then the Recipe's requirements, return false
//           if (inventory.Quantity(ingList[i]) < ing[ingList[i]]) return false;
//         }
//         // .. otherwise return false 
//         else
//         {
//           return false;
//         }
//       }
//
//       // Return true if all previous checks passed
//       return true;
//     }
//
//     public static bool Craftable(Recipe recipe, InventoryObject.InventoryObject[] inventories)
//     {
//       Dictionary<int, int> ing = recipe.Ingredients.Contents;
//       List<int> ingList = new List<int>(ing.Keys);
//       int c = ingList.Count;
//       int d = inventories.Length;
//       int lootCount = 0;
//
//       // countList will hold a total count of available Loot for each Recipe Ingredient
//       Dictionary<int, int> countList = new Dictionary<int, int>(c);
//
//       // Iterate through all Ingredients in the Recipe
//       for (int i = 0; i < c; i++)
//       {
//         // Reset lootCount to 0 for this Ingredient
//         lootCount = 0;
//
//         // Iterate through all of the Inventories
//         for (int j = 0; j < d; j++)
//         {
//           // If the current inventory contains this Ingredient, continue
//           if (inventories[j].Contains(ingList[i]))
//           {
//             // Current Inventory's quantity of the current Recipe Ingredient
//             int q = inventories[j].Quantity(ingList[i]);
//             // Current Recipe Ingredient's quantity requirement
//             int g = ing[ingList[i]];
//
//             // If the current Inventory contains the Loot item but does not have enough, 
//             // add its quantity to the lootCount
//             if (q < g)
//             {
//               lootCount += q;
//             }
//
//             // If the current Inventory contains the Loot item and has sufficient quantities,
//             // add the required Ingredient quantity to lootCount
//             else if (q >= g)
//             {
//               lootCount += g;
//             }
//           }
//         }
//
//         // After iterating through all of the Inventories, set the total accumulated
//         // lootCount to a list with the Recipe's index as its index
//         countList[i] = lootCount;
//       }
//
//
//       // After iterating through all of our Ingredients and all of our Inventories, 
//       // check each value in countList and compare it to its Ingredient's required quantity.
//       // If even one value is less than required, we don't have enough Ingredients and return false.
//       for (int r = 0; r < c; r++)
//       {
//         if (countList[r] < ing[ingList[r]])
//           return false;
//       }
//
//       // If the previous checks passed, return true
//       return true;
//     }
//
//     // [Craft]
//     public static Loot Craft(Recipe recipe, Inventory inventory)
//     {
//       // Set ingredient variables
//       Dictionary<int, int> ing = recipe.Ingredients.Contents;
//       List<int> ingList = new List<int>(ing.Keys);
//       int c = ingList.Count;
//
//       // For each Ingredient in the Recipe, remove the required amount from this Inventory
//       for (int i = 0; i < c; i++)
//       {
//         inventory.Remove(ingList[i], ing[ingList[i]]);
//       }
//
//       return recipe.Output;
//     }
//
//     public static Loot Craft(Recipe recipe, Inventory[] inventories)
//     {
//       Dictionary<int, int> ing = recipe.Ingredients.Contents;
//       List<int> ingList = new List<int>(ing.Keys);
//       int c = ingList.Count;
//
//       for (int i = 0; i < c; i++)
//       {
//         // For each Ingredient in the Recipe, set the required amount and reset the number removed
//         int req = ing[ingList[i]];
//         int numRemoved = 0;
//
//         for (int j = 0; j < inventories.Length; j++)
//         {
//           int q = inventories[j].Quantity(ingList[i]);
//           Debug.Log(q);
//
//           // If the current Inventory contains all of the required Ingredients,
//           // remove them all and break out of this loop.
//           if (q >= ing[ingList[i]])
//           {
//             inventories[j].Remove(ingList[i], req);
//             numRemoved = req;
//             break;
//           }
//           // If the current Inventory contains only some of the required Ingredients,
//           // remove however many it contains, increment numRemoved, and check numRemoved
//           // to see if we've removed the required amount. If so, break out of this loop.
//           else
//           {
//             inventories[j].Remove(ingList[i], q);
//             numRemoved += q;
//
//             if (numRemoved >= req)
//               break;
//           }
//         }
//       }
//
//       return recipe.Output;
//     }
//   }
// }
//
// }