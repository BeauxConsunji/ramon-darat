using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PitcherScript : MonoBehaviour
{
    // for tilting controls
    public float rotateSpeed = 30f;
    private float z;
    private bool isPressed;
    private bool canPour;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Debug.Log("IS PRESSED: " + isPressed);
        // pouring
        if (canPour)
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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "PouringAreaTrigger")
        {
            canPour = true;
            Debug.Log("u can pour now");
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.name == "PouringAreaTrigger")
        {
            canPour = false;
        }
    }

}
