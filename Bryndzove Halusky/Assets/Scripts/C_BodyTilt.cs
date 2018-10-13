using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class C_BodyTilt : MonoBehaviour {

    private CharacterMovement characterMovement;
    private Vector3 WSADTilt;
    public float rotateRate = 100f;

	// Use this for initialization
	void Start ()
    {
        characterMovement = transform.parent.GetComponent<CharacterMovement>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        Tilt();

        Debug.Log(WSADTilt);
    }

    // set x,z rotation based on movement from charactermovement script
    void Tilt()
    {
        WSADTilt = new Vector3(characterMovement.WS * rotateRate, 0, -characterMovement.AD * rotateRate);

        transform.localEulerAngles = WSADTilt;
    }
}