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
        // do other machine gun only related stuff below if needed
    }

    public override bool Reload()
    {
        return base.Reload();
        // do other machine gun only related stuff below if needed
    }

    public override void CreatePaintball()
    {
        base.Muzzle = transform.Find("Muzzle");
        base.CreatePaintball();

        Vector3 colour;
        // determine the colour of paintball
        if (Character.Team == "Red") colour = new Vector3(1, 0, 0);
        else colour = new Vector3(0, 0, 1);

        photonView.RPC("NetworkCreatePaintball", PhotonTargets.All, new object[] 
        { Muzzle.transform.position, Muzzle.transform.rotation, colour, shotSpeed});
    }

    [PunRPC]
    public void NetworkCreatePaintball(Vector3 position, Quaternion rotation, Vector3 colour, float speed)
    {
        GameObject pPaintball;
        pPaintball = Instantiate(Paintball, position, rotation);
        pPaintball.GetComponent<Renderer>().material.color = new Color(colour.x, colour.y, colour.z, 1);
        pPaintball.GetComponent<P_Paintball>().speed = speed;
        pPaintball.GetComponent<P_Paintball>().ID = PhotonNetwork.time;
        if (base.photonView.isMine == false)
        {
            GameObject.Find("Character(Clone)").GetComponent<C_Character>().Team = "Pink";
            Debug.Log("hello");
        }
    }
}
