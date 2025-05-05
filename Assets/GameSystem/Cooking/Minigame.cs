using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

[RequireComponent(typeof(GameObjectID))]
public class Minigame : MonoBehaviour
{
    public enum Type { Guideline, Measurable, Timing };
    public Type type;
    public string loadingScreenText;
    public Sprite loadingScreenImage; 
    public GameObjectID gameObjectId;
    public bool done = false;
    public Recipe recipe;
    public List<GameObject> ingredientList = new List<GameObject>();


    // Start is called before the first frame update
    void Start()
    {
        gameObjectId = GetComponent<GameObjectID>();

        ingredientList = GetComponentsInChildren<Ingredient>(true)
            .Select(script => script.gameObject)
            .ToList();

    }

    public virtual void IngredientDone(GameObject i) {
        // i.SetActive(false);
        if (ingredientList.Contains(i)) {
            ingredientList.Remove(i);
            MarkCompleted();
        }
    }

    // public virtual void MarkCompleted() {
    //     if (done) return;
    //     if (recipe != null) {
    //         done = true;
    //         recipe.NextMinigame();
    //     }
    // }

        public virtual void MarkCompleted() {
        if (done) return;
        if (recipe != null) {
            if (ingredientList.Count == 0) {
                StartCoroutine(FinishMinigame());
            }
        }
    }

    IEnumerator FinishMinigame()
    {
        yield return new WaitForSeconds(2);
        done = true;
        recipe.NextMinigame();
    }
}
