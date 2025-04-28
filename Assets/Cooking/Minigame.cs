using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(GameObjectID))]
public class Minigame : MonoBehaviour
{
    public enum Type { Guideline, Measurable, Timing };
    public Type type;
    public string loadingScreenText;
    public Sprite loadingScreenImage;
    public GameObjectID gameObjectId;
    public bool done = false;
    
    // Start is called before the first frame update
    void Start()
    {
        gameObjectId = GetComponent<GameObjectID>();
    }

    public virtual void MarkCompleted() {
        if (done) return;
        // TODO: Go to next minigame
    }
}
