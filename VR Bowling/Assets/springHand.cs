using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

public class springHand : MonoBehaviour {

    public Hand hand;

    public void OnPickUp()
    {
        hand.transform.gameObject.GetComponent<SpringJoint>().connectedBody = this.gameObject.GetComponent<Rigidbody>();
        this.gameObject.GetComponent<Rigidbody>().isKinematic = false;
        print("yass");
        //this.gameObject.transform.parent = null;

    }
}
