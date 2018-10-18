using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class W_Weapon : MonoBehaviour {

    [Header("Attributes")]
    public int clipSize;
    public int ammoCount;
    public float reloadDelay;
    public float shotDelay;
    public AudioSource shotSound;
    public float shotSpeed;

    private float shotTime = 0f;
    private float reloadTime = 0f;
    private bool isFiring = false;
    private bool isReloading = false;

    // Use this for initialization
    void Start ()
    {

    }
	
	// Update is called once per frame
	void Update ()
    {
        Debug.Log(shotTime);
    }

    public virtual bool Fire()
    {
        if (ammoCount <= 0 || isFiring == true || isReloading == true) return false;

        ammoCount = ammoCount - 1;
        StartCoroutine(RunShotDelay());
        Debug.Log("Shooting.. Delay of " + shotDelay + " seconds");
        return true;
    }

    public virtual bool Reload()
    {
        if (ammoCount == clipSize) return false;

        Debug.Log("Reloading.. Delay of " + reloadDelay + " seconds");
        StartCoroutine(RunReloadDelay());
        return true;
    }

    // weapon coroutines
    IEnumerator RunShotDelay()
    {
        while (shotTime < shotDelay)
        {
            isFiring = true;
            shotTime += Time.deltaTime;
            yield return null;
        }

        shotTime = 0f;
        isFiring = false;
        yield break;      
    }

    IEnumerator RunReloadDelay()
    {
        while (reloadTime < reloadDelay)
        {
            isReloading = true;
            reloadTime += Time.deltaTime;
            yield return null;
        }

        reloadTime = 0f;
        isReloading = false;
        ammoCount = clipSize;
        Debug.Log("Ammo now " + ammoCount);
        yield break;
    }

}
