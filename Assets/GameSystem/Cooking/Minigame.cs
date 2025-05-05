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
    public float delayAfterDone = 2.0f;


    // Start is called before the first frame update
    void Start()
    {
        gameObjectId = GetComponent<GameObjectID>();

        ingredientList = GetComponentsInChildren<Ingredient>(true)
            .Select(script => script.gameObject)
            .ToList();

    }

    public virtual void MarkCompleted(GameObject i=null) {
        if (done) return;
        if (i != null) { // Guideline Minigame
            if (ingredientList.Contains(i)) {
                ingredientList.Remove(i);
                if (recipe != null) {
                    if (ingredientList.Count == 0) {
                        StartCoroutine(FinishMinigame());
                    }
                }
            }
        } else { // Measurable Minigame
            if (recipe != null) {
                done = true;
                recipe.NextMinigame();
            }
        }
    }

    IEnumerator FinishMinigame()
    {
        Debug.Log("Finished Minigame");
        yield return new WaitForSeconds(delayAfterDone);
        done = true;
        recipe.NextMinigame();
    }
}
