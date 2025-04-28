using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Guideline : MonoBehaviour
{
    public Draggable.Type correctTool = Draggable.Type.None;
    public LineRenderer lineRenderer;
    public GameObject triggerPrefab;
    private List<Transform> triggers = new List<Transform>();
    
    private Ingredient ingredient; // parent ingredient
    public bool done = false;

    public float triggerRadius = 0.5f;

    public float timeLimit = 2.0f; // how long the player is given to complete the next guideline trigger before it resets to the beginning
    private float lastCompletedTrigger = 0.0f;
    public int currentTargetTrigger = 0; // current target trigger so that the player does it in order

    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();

        // create triggers at each point of the line

        for (int i = 0; i < lineRenderer.positionCount; i++) {
            Vector3 worldPosition = lineRenderer.GetPosition(i);
            GameObject trigger = Instantiate(triggerPrefab, worldPosition, Quaternion.identity, transform);
            trigger.GetComponent<CircleCollider2D>().radius = triggerRadius;
            triggers.Add(trigger.transform);
            trigger.GetComponent<GuidelineTrigger>().correctTool = this.correctTool;
        }

        var obj = GetComponentInParent<Ingredient>();
        if (obj != null)
            ingredient = obj;
    }

    void Update()
    {
        if (done) return;

        if (lastCompletedTrigger <= 0) { // time limit to complete the current target trigger elapsed so reset to the beginning
            currentTargetTrigger = 0;
        } else {
            lastCompletedTrigger -= Time.deltaTime;
        }
    }

    public void MarkTriggerAsCompleted(Transform trigger) {
        if (done) return;
        if (triggers[currentTargetTrigger] == trigger) {
            if (currentTargetTrigger + 1 >= triggers.Count) {
                done = true;
                ingredient.MarkGuidelineAsCompleted();
                return;
            }
            currentTargetTrigger++;
            lastCompletedTrigger = timeLimit;
        }
    }
}
