﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour {

    public CharacterMovement characterMovement;
    public float cameraSensitivity, cameraSensitivityDamp = 3f;

	// Use this for initialization
	void Start ()
    {
        characterMovement = GameObject.Find("Character").GetComponent<CharacterMovement>();
        cameraSensitivity = characterMovement.mouseSensitivity / cameraSensitivityDamp;
    }
	
	// Update is called once per frame
	void Update ()
    {
        RotateWithMouseY(355f, 20f);
    }

    // rotate camera only on x axis
    void RotateWithMouseY(float upperClamp, float lowerClamp)
    {
        // clamp rotation to parameter local eulers
        if (transform.localEulerAngles.x >= upperClamp || transform.localEulerAngles.x <= lowerClamp)
        {
            float rawMouseRotation = Input.GetAxis("Mouse Y");
            Vector3 mouseRotation = new Vector3(-rawMouseRotation, 0, 0);

            transform.Rotate(mouseRotation * cameraSensitivity, Space.Self);
        }

        // reset if goes outside parameter values (with 20 degree offset in case of frame delay)
        if (transform.localEulerAngles.x < upperClamp && transform.localEulerAngles.x > upperClamp - 20f)
        {
            Vector3 fixAngle = new Vector3(upperClamp + 0.01f, transform.localEulerAngles.y, transform.localEulerAngles.z);
            transform.localEulerAngles = fixAngle;
        }

        if (transform.localEulerAngles.x > lowerClamp && transform.localEulerAngles.x < lowerClamp + 20f)
        {
            Vector3 fixAngle = new Vector3(lowerClamp - 0.01f, transform.localEulerAngles.y, transform.localEulerAngles.z);
            transform.localEulerAngles = fixAngle;
        }
    }
}
