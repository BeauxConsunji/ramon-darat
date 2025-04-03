using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuideScript : MonoBehaviour
{
    public GameObject message; // temporary and for logic testing
    private bool rightAmount;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (rightAmount) {
            message.SetActive(true);
        }
        else {
            message.SetActive(false);
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        // Layer 4 = Water
        if (collision.gameObject.layer == 4)
        {
            rightAmount = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        // Layer 4 = Water
        if (collision.gameObject.layer == 4)
        {
            rightAmount = false;
        }
    }
}
