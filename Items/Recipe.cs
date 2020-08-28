using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Recipe", menuName = "Items/Recipe")]
public class Recipe : Item
{
    public Meals Finishedmeal;
    public List<Ingredient> ingredientList;
    public List<int> ingredientAmount;
}
