using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class C_BodyTilt : Photon.MonoBehaviour {

    private CharacterMovement characterMovement;
    private Vector3 WSADTilt;
    public float rotateRate = 100f;

    void Awake()
    {

    }

	// Use this for initialization
	void Start ()
    {
        characterMovement = transform.parent.GetComponent<CharacterMovement>();
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (photonView.isMine)
        {
            Tilt();
        }
    }

    // set x,z rotation based on movement from charactermovement script
    void Tilt()
    {
        WSADTilt = new Vector3(characterMovement.WS * rotateRate, 0, -characterMovement.AD * rotateRate);

        transform.localEulerAngles = WSADTilt;
    }
}