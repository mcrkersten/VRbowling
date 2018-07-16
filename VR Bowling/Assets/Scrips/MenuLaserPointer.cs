using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuLaserPointer : MonoBehaviour {

    public RaycastHit laser;
    public LineRenderer lineRenderer;
	// Use this for initialization
	void Start () {
        lineRenderer.positionCount = 2;
    }
	
	// Update is called once per frame
	void Update () {


        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out laser, Mathf.Infinity))
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * laser.distance, Color.yellow);
            Debug.Log("Did Hit");
            lineRenderer.SetPosition(0, this.gameObject.transform.position);
            lineRenderer.SetPosition(1, Vector3.forward);
        }
        else
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * 1000, Color.white);
            Debug.Log("Did not Hit");
            lineRenderer.SetPosition(0, this.gameObject.transform.position);
            lineRenderer.SetPosition(1, Vector3.forward);

        }
    }
}
