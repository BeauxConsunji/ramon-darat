using System;
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
        double percentage = Math.Round(state.score*100.0f/state.total);
        if (percentage >= 93)
            scoreText.text = "A";
        else if (percentage >= 87)
            scoreText.text = "B+";
        else if (percentage >= 81)
            scoreText.text = "B";
        else if (percentage >= 75)
            scoreText.text = "C+";
        else if (percentage >= 69)
            scoreText.text = "C";
        else if (percentage >= 60)
            scoreText.text = "D";
        else
            scoreText.text = "F";
            
        scoreText.text += $" ({percentage}%)";
        
        Debug.Log("scorussy " + state.score.ToString());
        Debug.Log("totully " + state.total.ToString());
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
