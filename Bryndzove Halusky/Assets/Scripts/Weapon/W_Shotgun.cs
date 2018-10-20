using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class W_Shotgun : W_Weapon {

    W_Shotgun()
    {
        clipSize = 8;
        ammoCount = 8;
        shotDelay = 1.0f;
        reloadDelay = 3f;
        shotSpeed = 10f;
    }

    // Use this for initialization
    void Start()
    {
        SetPaintballColour();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override bool Fire()
    {
        return base.Fire();
        // do other shotgun only related stuff below if needed
    }

    public override bool Reload()
    {
        return base.Reload();
        // do other shotgun only related stuff below if needed
    }

    public override void CreatePaintball()
    {
        base.Muzzle = transform.Find("Muzzle");
        base.CreatePaintball();
    }
}
