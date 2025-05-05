using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class UIRecipeState : UIState {
    // info abt 
    public bool locked = true;
    public bool done = false;
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
    public GameObject lockedSprite;

    // TODO: sprite for locked
    public override void ApplyNewStateInternal() {
        name.text = state.locked ? "??????" : state.name;
        description.text = state.locked ? "??????" : state.description;
        img.sprite = state.thumbnail;

        lockedSprite.SetActive(state.locked);
    }

    public void SelectRecipe() {
        Debug.Log("Clicked");
        if (state.locked) return;
        G.UI.uiType = UIType.LoadingScreen;
        G.UI.recipe = state;
        G.UI.recipe.MarkModified();
        G.UI.MarkModified();

        if (G.I.TryGetGameObjectById(state.gameObjectId, out var recipe)) {
            recipe.SetActive(true);
        }
    }
}
