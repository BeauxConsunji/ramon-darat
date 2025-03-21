using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using Unity.VisualScripting;
using UnityEngine;

public enum UIType {
    HUD,
    MainMenu,
    Cutscene,
    RecipeSelector,
    TimingMinigame,
}

public class UIState
{
    [System.NonSerialized]
    public bool modified = true;
    public virtual void MarkModified() {
        modified = true;
    }
}

public class UIView<S> : MonoBehaviour where S: UIState, new() {
    public S state;

    public virtual void ApplyNewStateInternal() {}
    public virtual void UpdateChildren() {}

    public static void ApplyNewStateArray<T, C> (RectTransform parent, GameObject childPrefab, IEnumerable<T> states) where T : UIState, new() where C : UIView<T> {
        int i = 0;
        foreach (var currentState in states) {
            GameObject obj;
            if (i < parent.childCount) {
                obj = parent.GetChild(i).gameObject;
                obj.SetActive(true);
            } else {
                obj = Instantiate(childPrefab, parent, false);
            }
            var childComponent = obj.GetComponent<C>();
            if (currentState != childComponent.state) {
                childComponent.state = currentState;
                childComponent.state.modified = true;
            }
            childComponent.ApplyNewState();
            i++;
        }
        for (; i < parent.childCount; i++) {
            parent.GetChild(i).gameObject.SetActive(false);
        }
    }

    public void ApplyNewState() {
        if (state == null) {
            state = new S();
        }
        if (state.modified) {
            ApplyNewStateInternal();
        }
        UpdateChildren();
        state.modified = false;
    }

    public static void DestroyAllChildren(Transform t) {
        for (int i = t.childCount - 1; i >= 0; i--) {
            GameObject.DestroyImmediate(t.GetChild(i).gameObject);
        }
    }
}

[System.Serializable]
public class UIMainState : UIState {
    // TODO: Create UIState subclasses for each UI type e.g. UIHUDState, UIMenuState

    public UIType uiType;
    public UIRecipeSelectorState recipeSelector;
    public UITimingMinigameState timingMinigame;
}

public class UIMain : UIView<UIMainState> {
    // TODO: Create UIView<UIState> subclasses for each UI type in a separate file e.g. UIMainMenu.cs
    // e.g. public UIMainMenu menu;
    public UIRecipeSelector recipeSelector;
    public UIMainMenu mainMenu;
    public UITimingMinigame timingMinigame;

    public override void ApplyNewStateInternal()
    {
        // TODO: Set active the gameObject depending on the current UI type
        // e.g. menu.gameObject.SetActive(state.uiType == UIType.Menu);
        recipeSelector.gameObject.SetActive(state.uiType == UIType.RecipeSelector);
        mainMenu.gameObject.SetActive(state.uiType == UIType.MainMenu);
        timingMinigame.gameObject.SetActive(state.uiType == UIType.TimingMinigame);
    }
    public override void UpdateChildren() {
        // TODO: set the state of each UI type
        // Example:
        // menu.state = state.menu;
        // menu.ApplyNewState();

        recipeSelector.state = state.recipeSelector;
        recipeSelector.ApplyNewState();

        timingMinigame.state = state.timingMinigame;
        timingMinigame.ApplyNewState();
    }
    public static UIMainState DefaultState() {
        return new UIMainState {
            uiType = UIType.MainMenu,
            recipeSelector = new UIRecipeSelectorState() {
                recipes = new List<UIRecipeState>() {
                    new UIRecipeState() { name = "Rice", description = "staple"},
                    new UIRecipeState() { name = "Adobo", description = "Manok"},
                    new UIRecipeState() { name = "Pinakbet", description = "Gulay"},
                    new UIRecipeState() { name = "Sinigang", description = "Maasim"},
                    new UIRecipeState() { name = "Kare-Kare", description = "Peanut Butter"},
                    new UIRecipeState() { name = "Halo-Halo", description = "Matamis"},

                    // new UIRecipeState { name = "Adobo"}
                } 
                
            },
            timingMinigame = new UITimingMinigameState() {
                instructions = new List<UITimingInstructionState>()
            }
            
            // { recipes = new List<UIRecipestate> { new UIRecipeState { name = "Adobo"}}}
            // TODO: initialize each UI type
            // Example:
            // menu = new UIMenuState() {
            //     menuType = MenuType.TitleMenu
            // }
        };
    }
    void Start() {
        state = DefaultState();

    }

    void LateUpdate() {
        ApplyNewState();
    }
}
