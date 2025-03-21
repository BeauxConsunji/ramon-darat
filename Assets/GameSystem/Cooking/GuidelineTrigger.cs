using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(CircleCollider2D))]
public class GuidelineTrigger : MonoBehaviour
{
    private Guideline guideline;
    public Draggable.Type correctTool;
    private Collider2D collider;

    void Start() {
        guideline = GetComponentInParent<Guideline>();
        collider = GetComponent<Collider2D>();
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (correctTool != Draggable.Type.None) {
            if (other.TryGetComponent<Draggable>(out var draggable)) {
                if (draggable.type == guideline.correctTool) {
                    guideline.MarkTriggerAsCompleted(transform);
                }   
            }
        }

    }

    public void Update() {

        if (Input.GetMouseButton(0) && IsTouchingMouse()) {
            guideline.MarkTriggerAsCompleted(transform);
        }
    }
    // private void OnMouseDown(){
    //     if (correctTool == Draggable.Type.None) {
    //         guideline.MarkTriggerAsCompleted(transform);
    //         Debug.Log("Dragging like a dragqueen");  
    //     }           
    // }

    public bool IsTouchingMouse()
    {
        Vector2 point = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        return collider.OverlapPoint(point);
    }
}
    

