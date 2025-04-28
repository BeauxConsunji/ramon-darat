using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class UIRecipeState : UIState {
    // info abt 
    public bool locked = true;
    public string name;
    public string description;
    public Sprite thumbnail;
    public string gameObjectId;
    public List<UIMinigameState> minigames = new List<UIMinigameState>();
    public int currentMinigame = 0;

    public void AddMinigame(UIMinigameState minigame) {
        minigames.Add(minigame);
        MarkModified();
    }
}
public class UIRecipe : UIView<UIRecipeState>
{
    public TMPro.TextMeshProUGUI name;
    public TMPro.TextMeshProUGUI description;
    public Image img;

    // TODO: sprite for locked
    public override void ApplyNewStateInternal() {
        name.text = state.name;
        description.text = state.description;
        img.sprite = state.thumbnail;

        // TODO: lockedSprite.SetActive(state.locked);
    }

    public void SelectRecipe() {
        Debug.Log("Clicked");
        G.UI.uiType = UIType.LoadingScreen;
        G.UI.recipe = state;
        G.UI.recipe.MarkModified();
        G.UI.MarkModified();

        if (G.I.TryGetGameObjectById(state.gameObjectId, out var recipe)) {
            recipe.SetActive(true);
        }
    }
}
