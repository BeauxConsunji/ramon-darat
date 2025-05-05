using System.Collections;
using System.Collections.Generic;
using System.Xml.Schema;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class UIScoreboardState : UIState
{
    public int score;
    public int total = 1;
    // public Image final_food;
}
public class UIScoreboard : UIView<UIScoreboardState>
{
    public TMPro.TextMeshProUGUI scoreText;
    public Image final_food;
    // Start is called before the first frame update
    public override void ApplyNewStateInternal()
    {
        double percentage = state.score * 100.0f /state.total;
        scoreText.text = $"{percentage}%";
        Debug.Log("scorussy" + state.score.ToString());
        Debug.Log("totully" + state.total.ToString());
        // scoreText.text = state.score.ToString() + "/" + state.total.ToString();
        Debug.Log("Testes");
        if (G.UI.recipe != null && final_food != null)
        {
            Debug.Log("Spriting like a sprite queen");
            final_food.sprite = G.UI.recipe.finalImage;
        }
    }
    public void Done() {
        G.UI.recipeSelector.UnlockNextRecipe();
        G.UI.uiType = UIType.RecipeSelector;
        G.UI.MarkModified();
        G.UI.recipe = null;
    }
}
