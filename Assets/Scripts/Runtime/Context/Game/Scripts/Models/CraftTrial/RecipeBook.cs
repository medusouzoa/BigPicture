// using System.Collections.Generic;
// using System;
//
// namespace Runtime.Context.Game.Scripts.Models.CraftTrial
// {
//   namespace LootSystem
//   {
//     [Serializable]
//     public class RecipeBook
//     {
//       private List<Recipe> list;
//
//       public RecipeBook()
//       {
//         this.list = new List<Recipe>();
//       }
//
//       // [Clear] - Clear the inventory
//       public void Clear()
//       {
//         this.list.Clear();
//       }
//
//       // [Add] - Add a Recipe item to the RecipeBook. Returns this RecipeBook.
//       public RecipeBook Add(Recipe recipe)
//       {
//         this.list.Add(recipe);
//
//         return this;
//       }
//
//       // [Remove] - Remove a Recipe from the RecipeBook
//       public RecipeBook Remove(Recipe recipe)
//       {
//         if (this.list.Contains(recipe))
//         {
//           this.list.Remove(recipe);
//         }
//
//         return this;
//       }
//
//       /* [Total] - returns the total number of Recipe in the RecipeBook */
//       public int Total()
//       {
//         return this.list.Count;
//       }
//
//       /* [Contains] - return whether or not a Recipe exists in the RecipeBook */
//       public bool Contains(Recipe recipe)
//       {
//         return this.list.Contains(recipe);
//       }
//
//       public bool Contains(string name)
//       {
//         int c = this.list.Count;
//
//         for (int i = 0; i < c; i++)
//         {
//           if (list[i].Name == name) return true;
//         }
//
//         return false;
//       }
//
//       public bool Contains(int id)
//       {
//         int c = this.list.Count;
//
//         for (int i = 0; i < c; i++)
//         {
//           if (list[i].Id == id) return true;
//         }
//
//         return false;
//       }
//
//       // [Contents] - get list of Recipes in RecipeBook
//       public List<Recipe> Contents
//       {
//         get { return this.list; }
//       }
//     }
//   }
// }