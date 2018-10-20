using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class P_Paintball : MonoBehaviour {

    [Header("Attributes")]
    public Material redColour;
    public Material blueColour;

    [Header("Networking")]
    public double creationTime;
    public Vector3 Position;
    public Quaternion Rotation;
    public int ID;
    public float speed = 1f;

    // Use this for initialization
    void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        transform.position = transform.position + transform.forward * (Time.deltaTime * speed);
	}
}
