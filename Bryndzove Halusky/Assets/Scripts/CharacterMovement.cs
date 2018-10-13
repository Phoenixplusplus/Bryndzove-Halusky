using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour {

    public float mouseSensitivity = 3f, movementSpeed = 10f;
    public float WS, AD;

    // Use this for initialization
    void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        Move();
        RotateWithMouseX();
    }

    void Move()
    {
        // move on keyboard input
        AD = Input.GetAxis("Horizontal") * Time.deltaTime * movementSpeed;
        WS = Input.GetAxis("Vertical") * Time.deltaTime * movementSpeed;

        transform.Translate(AD, 0, WS);
    }

    void RotateWithMouseX()
    {
        // rotate on mouse X
        float rawMouseRotation = Input.GetAxis("Mouse X");
        Vector3 mouseRotation = new Vector3(0, rawMouseRotation, 0);

        transform.Rotate(mouseRotation * mouseSensitivity);
    }
}
