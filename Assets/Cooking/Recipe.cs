using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(GameObjectID))]
public class Recipe : MonoBehaviour
{
    public List<Minigame> minigames = new List<Minigame>();
    // Start is called before the first frame update
    void Start()
    {
        foreach (var minigame in GetComponentsInChildren<Minigame>()) {
            minigames.Add(minigame);
            minigame.gameObject.SetActive(false);
        }
        InitializeUI();
    }
    void InitializeUI() {
        G.UI.recipe.minigames.Clear();

        foreach (var minigame in minigames) {
            G.UI.recipe.AddMinigame(new UIMinigameState() {
                type = minigame.type,
                loadingScreenText = minigame.loadingScreenText,
                loadingScreenImage = minigame.loadingScreenImage,
                gameObjectId = minigame.gameObjectId.ids[0]
            });
        }
        G.UI.MarkModified();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
