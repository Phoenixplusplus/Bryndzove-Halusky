using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class W_MachineGun : W_Weapon {

    W_MachineGun()
    {
        clipSize = 30;
        ammoCount = 30;
        shotDelay = 0.2f;
        reloadDelay = 1.5f;
        shotSpeed = 1f;
    }

    // Use this for initialization
    void Start ()
    {
          
    }
	
	// Update is called once per frame
	void Update ()
    {

    }

    public override bool Fire()
    {
        return base.Fire();
        // do other machine gun only related stuff below if needed
    }

    public override bool Reload()
    {
        return base.Reload();
        // do other machine gun only related stuff below if needed
    }
}
