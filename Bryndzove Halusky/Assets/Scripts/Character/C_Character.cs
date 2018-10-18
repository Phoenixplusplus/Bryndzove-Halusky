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

    // do not assign these values in editor
    public GameObject lw, rw;
    public W_Weapon leftWeapon, rightWeapon;

    private bool autoFire;

    // called by network manager to give reference to new gameobjects
    public void NetworkStart()
    {
        leftWeapon = lw.GetComponent<W_Weapon>();
        if (leftWeapon.name.Contains("Machine")) autoFire = true;
        else autoFire = false;
    }

    // Use this for initialization
    void Start ()
    {

	}
	
	// Update is called once per frame
	void Update ()
    {
        // keyboard input
        if (autoFire == true)
        {
            if (Input.GetMouseButton(0))
            {
                leftWeapon.Fire();
            }
        }
        else
        {
            if (Input.GetMouseButtonDown(0))
            {
                leftWeapon.Fire();
            }
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            leftWeapon.Reload();
        }
    } 
}
