using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pin : MonoBehaviour {

    private Rigidbody rb;
    public bool hasFallen;
    public float fallRotationMin; //20
    public float fallRotationMax; //330


    private void Start()
    {
        rb = this.GetComponent<Rigidbody>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Ball")
        {
            rb.AddForce(collision.gameObject.GetComponent<Rigidbody>().velocity);
        }
    }

    public void Update()
    {
        if (gameObject.transform.eulerAngles.x < fallRotationMax && gameObject.transform.eulerAngles.x > fallRotationMin)
        {
            hasFallen = true;
        }
        else if (gameObject.transform.eulerAngles.z < fallRotationMax && gameObject.transform.eulerAngles.z > fallRotationMin)
        {
            hasFallen = true;
        }
    }
}
