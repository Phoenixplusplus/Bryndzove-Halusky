using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class C_ArmTilt : MonoBehaviour {

    private Vector3 initialRotation;

	// Use this for initialization
	void Start ()
    {
        initialRotation = transform.eulerAngles;
	}
	
	// Update is called once per frame
	void Update ()
    {
		transform.eulerAngles = new Vector3 (initialRotation.x, transform.parent.eulerAngles.y, initialRotation.z);
    }
}
