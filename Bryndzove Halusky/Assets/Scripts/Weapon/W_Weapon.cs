using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class W_Weapon : MonoBehaviour {

    [Header("Attributes")]
    public int clipSize;
    public float shootDelay;
    public float reloadTime;
    public AudioSource shotSound;
    public float shotSpeed;

    public Vector3 shotPosition;
    public Quaternion shotRotation;

    private Transform Muzzle;

    // Use this for initialization
    void Start ()
    {

    }
	
	// Update is called once per frame
	void Update ()
    {

    }

    public void Fire()
    {

    }

    public void Reload()
    {

    }
}
