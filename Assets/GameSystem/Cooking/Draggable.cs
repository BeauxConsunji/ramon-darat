using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum HeatLevel {None, Low, Medium, High};

[RequireComponent(typeof(BoxCollider2D))]
public class Draggable : MonoBehaviour
{
    public enum Type { None, Knife, Stirrer, Skin, Ingredient, Knob }
    public Type type = Type.None;
    public float knobRange = 2.0f;
    private Plane dragPlane;
    private Vector3 originalPosition;
    private Vector3 offset;
    private int settingsCount;
    private TimingMinigame timingMinigame;

    void Start() {
        settingsCount = HeatLevel.GetNames(typeof(HeatLevel)).Length;
        timingMinigame = GetComponentInParent<TimingMinigame>();
    }

    void OnMouseDown() {
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
            transform.rotation = Quaternion.Euler(0.0f, 0.0f, Mathf.Clamp((dragPoint + offset - originalPosition).x, -2, 2) * -180.0f / knobRange);
            
        } else {
            transform.position = dragPoint + offset;
        }
    }
    void OnMouseUp() {
        if (type == Type.Knob) {
            var settingIndex = settingsCount - 1 - Mathf.Floor((transform.rotation.z + 1) * (settingsCount-2) / 2.0f);
            if (timingMinigame != null)
                timingMinigame.ChangeHeatLevel((HeatLevel)(settingIndex));
        }
    }
}
