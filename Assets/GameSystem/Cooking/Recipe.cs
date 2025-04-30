using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(GameObjectID))]
public class Recipe : MonoBehaviour
{
    public List<Minigame> minigames = new List<Minigame>();
    public int score = 0;
    // Start is called before the first frame update
    void Start()
    {
        foreach (var minigame in GetComponentsInChildren<Minigame>()) {
            minigames.Add(minigame);
            minigame.recipe = this;
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

    public void NextMinigame() {
        Debug.Log("NextMinigame");
        if (G.UI.recipe.done) return;
        if (G.UI.recipe.currentMinigame + 1 >= G.UI.recipe.minigames.Count) {
            G.UI.recipe.done = true;
            G.UI.uiType = UIType.Scoreboard;
            G.UI.scoreboard.score = score;
            G.UI.scoreboard.MarkModified();
            G.UI.MarkModified();
            G.UI.recipe = null;
            gameObject.SetActive(false);
            return;
        }
        minigames[G.UI.recipe.currentMinigame].gameObject.SetActive(false);
        G.UI.recipe.currentMinigame++;
        G.UI.recipe.MarkModified();
        G.UI.uiType = UIType.LoadingScreen;
        G.UI.MarkModified();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
