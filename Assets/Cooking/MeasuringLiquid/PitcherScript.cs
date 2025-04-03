using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PitcherScript : MonoBehaviour
{
    public PouringAreaScript pouringArea;

    // for tilting controls
    public float rotateSpeed = 30f;
    private float z;
    private bool isPressed;

    // for mouse controls
    private bool dragging = false;
    private Vector3 offset;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Debug.Log("IS PRESSED: " + isPressed);
        // pouring
        Debug.Log(pouringArea.CanPourChecker());
        if (pouringArea.CanPourChecker())
        {
            if (Input.GetKey(KeyCode.Space) && z < 90)
            {
                z += Time.deltaTime * rotateSpeed;
                isPressed = true;
            }
            else if (!Input.GetKey(KeyCode.Space) && z > 0)
            {
                z -= Time.deltaTime * rotateSpeed;
                isPressed = false;
            }

            transform.localRotation = Quaternion.Euler(0f, 0f, z);
        }

        // mouse dragging
        if (dragging) { 
            transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition) + offset;            
        }

    }

    // set pouring controls
    public float GetZ()
    {
        return z;
    }
    public bool IsPouring()
    {
        return isPressed;
    }

    // set mouse dragging controls
    private void OnMouseDown()
    {
        Debug.Log("You are clicking me baby");
        offset = transform.position - Camera.main.ScreenToWorldPoint(Input.mousePosition);
        dragging = true;
    }

    private void OnMouseUp() 
    { 
        dragging = false;
    }
    
}
