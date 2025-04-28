using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CircleCollider2D))]
public class GuidelineTrigger : MonoBehaviour
{
    private Guideline guideline;

    void Start() {
        guideline = GetComponentInParent<Guideline>();
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.TryGetComponent<Draggable>(out var draggable)) {
            if (draggable.type == guideline.correctTool) {
                guideline.MarkTriggerAsCompleted(transform);
            }   
        }
    }
}
