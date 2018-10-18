using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class C_Character : Photon.MonoBehaviour {

    [Header("Network Attributes")]
    public string loginName;
    public string Team;

    [Header("Attributes")]
    public float Health = 10f;
    public float healthRecoverySpeed = 0.1f;
    public float movementSpeed;
    public Texture headTex, bodyTex;

	// Use this for initialization
	void Start ()
    {

	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}
}
