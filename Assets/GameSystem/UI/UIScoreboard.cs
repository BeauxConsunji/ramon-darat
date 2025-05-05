using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class UIScoreboardState : UIState
{
    public int score;
}
public class UIScoreboard : UIView<UIScoreboardState>
{
    public TMPro.TextMeshProUGUI scoreText;
    // Start is called before the first frame update
    public override void ApplyNewStateInternal()
    {
        scoreText.text = state.score.ToString();
    }
    public void Done() {
        G.UI.recipeSelector.UnlockNextRecipe();
        G.UI.uiType = UIType.RecipeSelector;
        G.UI.MarkModified();
        G.UI.recipe = null;
    }
}
