using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class UIRecipeSelectorState : UIState {
    // do list of recipes here here
    // modify here 
    public List<UIRecipeState> recipes = new List<UIRecipeState>();
    public int currentRecipe = 0;
    public void UnlockNextRecipe() {
        if (currentRecipe + 1 >= recipes.Count)
            return;
        currentRecipe++;
        recipes[currentRecipe].locked = false;
        recipes[currentRecipe].MarkModified();
        G.UI.recipeSelector.MarkModified();
    }
}

public class UIRecipeSelector : UIView<UIRecipeSelectorState>
{
    public GameObject recipePrefab;
    public RectTransform parentContainer;

    public override void ApplyNewStateInternal() {
        // TODO: change the display of locked recipes
        // for (recipe in state.recipes)
        // recipe.lock = i < fjsldfs

        ApplyNewStateArray<UIRecipeState, UIRecipe>(parentContainer, recipePrefab, state.recipes);
    }
}
