using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RiceCupScript : MonoBehaviour
{
    public LayerMask pouringWater;
    public GameObject tooMuchMessage; // temporary too

    private Animator anim;
    public Animator riceCookerInterior;
    private bool isEmpty = true;
    private bool canGetRice;
    private bool riceCookerGetRice;
    private string state;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();    
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("current animation state NO CUP: " + riceCookerInterior.GetCurrentAnimatorStateInfo(0).IsName("NoCup"));
        //Debug.Log(transform.position);
        anim.SetBool("isEmpty", isEmpty);

        if (Input.GetKeyDown(KeyCode.C))
        {            
            if (canGetRice)
            {               
                anim.SetTrigger("GetRice");
                isEmpty = false;
            }
            else
            {
                if(isEmpty)
                {
                    anim.SetTrigger("PourEmpty");
                }
                else
                {
                    if (riceCookerGetRice == true)
                    {
                        Debug.Log("YOU SHOULD BE GETTING RICE");
                        if (riceCookerInterior.GetCurrentAnimatorStateInfo(0).IsName("NoCup"))
                        {
                            anim.SetTrigger("PourRice");
                            state = "1Cup";
                        }
                        else if (riceCookerInterior.GetCurrentAnimatorStateInfo(0).IsName("1Cup"))
                        {
                            anim.SetTrigger("PourRice");
                            state = "2Cups";
                        }
                        else
                        {
                            tooMuchMessage.SetActive(true);
                        }
                        StartCoroutine(AddRiceAnimation(state));
                    }
                    else
                    {
                        anim.SetTrigger("PourRice");
                    }

                    isEmpty = true;
                }
            }
        }
    }

    IEnumerator AddRiceAnimation(string state)
    {
        Debug.Log("ABRACADABRA");
        yield return new WaitForSeconds(1.5f);

        riceCookerInterior.Play(state);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Bigas")
        {
            canGetRice = true;
            Debug.Log("KUHA KA NA NG KANIN");
        }
        else if (collision.gameObject.name == "PouringLimitMask")
        {
            riceCookerGetRice = true;
            Debug.Log("PWEDE NANG MAGKABIGAS");
        }

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Bigas")
        {
            canGetRice = false;
        }
        else if (collision.gameObject.name == "PouringLimitMask")
        {
            riceCookerGetRice = false;
        }
    }
}
