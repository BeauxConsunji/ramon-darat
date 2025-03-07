using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PitcherScript : MonoBehaviour
{
    public float rotateSpeed = 30f;
    private float z;
    private bool isPressed;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
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

    public float getZ()
    {
        return z;
    }
    public bool isPouring()
    {
        return isPressed;
    }

}
