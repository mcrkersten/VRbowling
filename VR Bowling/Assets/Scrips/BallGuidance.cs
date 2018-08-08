using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallGuidance : MonoBehaviour {

    private void OnTriggerStay(Collider ball)
    {
        if(ball.gameObject.tag == "Ball")
        {
            if(ball.gameObject.transform.position.x > this.gameObject.transform.position.x)
            {
                ball.GetComponent<Rigidbody>().AddForce(new Vector3(-.3f, 0, 0));
            }else if(ball.gameObject.transform.position.x < this.gameObject.transform.position.x)
            {
                ball.GetComponent<Rigidbody>().AddForce(new Vector3(.3f, 0, 0));
            }
        }
    }
}