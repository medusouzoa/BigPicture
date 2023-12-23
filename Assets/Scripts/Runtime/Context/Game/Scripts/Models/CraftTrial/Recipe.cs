// using System;
//
// namespace Runtime.Context.Game.Scripts.Models.CraftTrial
// {
//   [Serializable]
//   public class Recipe
//   {
//     private string name;
//     private int weight;
//     private int id;
//     private RecipeType recipeType;
//     private Ingredients ingredients;
//     private Loot output;
//
//     // Change these to whatever suits your game
//     public enum RecipeType
//     {
//       equipment,
//       consumable,
//       upgrade
//     };
//
//     public Recipe()
//     {
//     }
//
//     public Recipe(string name, int id, RecipeType recipeType, Ingredients ingredients, Loot output)
//     {
//       this.name = name;
//       this.id = id;
//       this.recipeType = recipeType;
//       this.ingredients = ingredients;
//       this.output = output;
//     }
//
//     public string Name
//     {
//       get { return this.name; }
//       set { this.name = value; }
//     }
//
//     public int Id
//     {
//       get { return this.id; }
//       set { this.id = value; }
//     }
//
//     public RecipeType Type
//     {
//       get { return this.recipeType; }
//       set { this.recipeType = value; }
//     }
//
//     public Ingredients Ingredients
//     {
//       get { return this.ingredients; }
//       set { this.ingredients = value; }
//     }
//
//    /* public Loot Output
//     {
//       get { return this.output; }
//       set { this.output = value; }
//     }*/
//
//     public override bool Equals(object obj)
//     {
//       if (GetHashCode() == obj.GetHashCode())
//         return true;
//       return false;
//     }
//
//     public override int GetHashCode()
//     {
//       unchecked
//       {
//         int hash = 47;
//
//         hash = hash * 227 + this.id.GetHashCode();
//
//         return hash;
//       }
//     }
//   }
// }