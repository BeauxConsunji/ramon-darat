using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(CircleCollider2D))]
public class GuidelineTrigger : MonoBehaviour
{
    private Guideline guideline;
    public Draggable.Type correctTool;
    public Draggable currentDraggable;
    private Collider2D collider;

    void Start() {
        guideline = GetComponentInParent<Guideline>();
        collider = GetComponent<Collider2D>();
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.TryGetComponent<Draggable>(out var draggable)) {
            if (correctTool != Draggable.Type.None && draggable.type == guideline.correctTool) {
                guideline.MarkTriggerAsCompleted(transform);
            }   
            currentDraggable = draggable;
        }

    }
    public void Update() {

        if (correctTool == Draggable.Type.None && !currentDraggable.isDragged && Input.GetMouseButton(0) && IsTouchingMouse()) {
            guideline.MarkTriggerAsCompleted(transform);
        }
    }

    public bool IsTouchingMouse()
    {
        Vector2 point = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        return collider.OverlapPoint(point);
    }
}
    

