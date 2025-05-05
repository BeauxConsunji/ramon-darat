using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum HeatLevel {None, Low, Medium, High};

[RequireComponent(typeof(BoxCollider2D))]
public class Draggable : MonoBehaviour
{
    public enum Type { None, Knife, Stirrer, Skin, Ingredient, Knob, RiceCup, PotCover, Pork, Sauce, Oil, Garlic, Onion, Water, Spatula }
    public Type type = Type.None;
    public float knobRange = 2.0f;
    private Plane dragPlane;
    private Vector3 originalPosition;
    private Vector3 offset;
    public Sprite defaultSprite, draggedSprite;
    public BoxCollider2D boxCollider;
    public SpriteRenderer spriteRenderer;
    private int settingsCount;
    public TimingMinigame timingMinigame;
    public bool isDragged = false;

    void Start() {
        Debug.Log("Draggable start");
        settingsCount = HeatLevel.GetNames(typeof(HeatLevel)).Length;
        timingMinigame = GetComponentInParent<TimingMinigame>();
        
        if (boxCollider == null)
            boxCollider = GetComponent<BoxCollider2D>();
        if (spriteRenderer == null)
            spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = defaultSprite;
        SetColliderFromSprite();
    }

    void OnMouseDown() {
        if (type != Type.RiceCup) {
            spriteRenderer.sprite = draggedSprite;
            SetColliderFromSprite();
        }
        isDragged = true;
        dragPlane = new Plane(G.I.mainCamera.transform.forward, transform.position);
        Ray camRay = G.I.mainCamera.ScreenPointToRay(Input.mousePosition);
        float planeDist;
        dragPlane.Raycast(camRay, out planeDist);
        offset = transform.position - camRay.GetPoint(planeDist);
        originalPosition = transform.position;
    }
    void OnMouseDrag() {
        Ray camRay = G.I.mainCamera.ScreenPointToRay(Input.mousePosition);
        float planeDist;
        dragPlane.Raycast(camRay, out planeDist);
        var dragPoint = camRay.GetPoint(planeDist);

        if (type == Type.Knob) {
            var dragAmount = (dragPoint + offset - originalPosition).x;
            var dragNormalized = Mathf.Clamp(dragAmount / (knobRange/2), -1, 1);
            var angle = 360 * (1f - (dragNormalized + 1f) / 2f);

            transform.rotation = Quaternion.Euler(0.0f, 0.0f, angle);
            // Debug.Log(transform.rotation.eulerAngles.z);
            
        } else {
            transform.position = dragPoint + offset;
        }
    }
    void OnMouseUp() {
        if (type != Type.RiceCup) {
            spriteRenderer.sprite = defaultSprite;
            SetColliderFromSprite();
        }
        isDragged = false;
        if (type == Type.Knob) {
            float angle = transform.rotation.eulerAngles.z;
            if (angle == 0.0f)
                angle = 360.0f;
            
            int settingIndex;
            if (angle > 240f) {
                settingIndex = 3;
            } 
            else if (angle > 120f) {
                settingIndex = 2;
            } 
            else {
                settingIndex = 1;
            }
            
            if (timingMinigame != null)
                timingMinigame.ChangeHeatLevel((HeatLevel)(settingIndex));
        }
    }

    private void SetColliderFromSprite() {
        Debug.Log("Set Collider From Sprite");
        boxCollider.size = new Vector2(spriteRenderer.sprite.bounds.size.x * spriteRenderer.transform.lossyScale.x, spriteRenderer.sprite.bounds.size.y * spriteRenderer.transform.lossyScale.y);
    }
}
