using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class Draggable : MonoBehaviour
{
    public enum Type { None, Knife, Stirrer, Skin, Ingredient }
    public Type type = Type.None;
    private Plane dragPlane;
    private Vector3 offset;

    void OnMouseDown() {
        dragPlane = new Plane(G.I.mainCamera.transform.forward, transform.position);
        Ray camRay = G.I.mainCamera.ScreenPointToRay(Input.mousePosition);
        float planeDist;
        dragPlane.Raycast(camRay, out planeDist);
        offset = transform.position - camRay.GetPoint(planeDist);
    }
    void OnMouseDrag() {
        Ray camRay = G.I.mainCamera.ScreenPointToRay(Input.mousePosition);
        float planeDist;
        dragPlane.Raycast(camRay, out planeDist);
        transform.position = camRay.GetPoint(planeDist) + offset;
    }
}
