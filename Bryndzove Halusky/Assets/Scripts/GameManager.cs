using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    [Header("Teams")]
    public int redTeamCount = 0;
    public int blueTeamCount = 0;

    [Header("Paintball")]
    public int paintballID = 0;

	// Use this for initialization
	void Start ()
    {
        
    }
	
	// Update is called once per frame
	void Update ()
    {

    }

    public void LockHideCursor()
    {
        // lock and hide mouse cursor
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
}
