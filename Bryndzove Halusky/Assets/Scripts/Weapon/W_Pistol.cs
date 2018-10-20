using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class W_Pistol : W_Weapon {

    W_Pistol()
    {
        clipSize = 12;
        ammoCount = 12;
        shotDelay = 0.01f;
        reloadDelay = 0.8f;
        shotSpeed = 5f;
    }

	// Use this for initialization
	void Start ()
    {
        SetPaintballColour();
    }
	
	// Update is called once per frame
	void Update ()
    {
        
    }

    public override bool Fire()
    {
        return base.Fire();
        // do other pistol gun only related stuff below if needed
    }

    public override bool Reload()
    {
        return base.Reload();
        // do other pistol gun only related stuff below if needed
    }

    public override void CreatePaintball()
    {
        base.Muzzle = transform.Find("Muzzle");
        base.CreatePaintball();
    }
}
