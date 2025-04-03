using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuideScript : MonoBehaviour
{
    public GameObject passMessage; // temporary and for logic testing
    public GameObject failMessage;

    // Start is called before the first frame update
    void Start()
    {
        passMessage.SetActive(false);
        failMessage.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        // Layer 4 = Water
        if (collision.gameObject.layer == 4)
        {
            passMessage.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        // Layer 4 = Water
        if (collision.gameObject.layer == 4)
        {
            passMessage.SetActive(false);
            failMessage.SetActive(true);
        }
    }
}
