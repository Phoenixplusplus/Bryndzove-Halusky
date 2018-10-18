using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint : Photon.MonoBehaviour {

    [Header("Attributes")]
    public string Team;
    public bool Occupied = false;

	// Use this for initialization
	void Start ()
    {
        
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            photonView.RPC("networkEnter", PhotonTargets.All, 1);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            photonView.RPC("networkExit", PhotonTargets.All, 1);
        }
    }

    [PunRPC]
    void networkEnter(int i)
    {
        Occupied = true;
        Debug.Log(Occupied);
    }

    [PunRPC]
    void networkExit(int i)
    {
        Occupied = false;
        Debug.Log(Occupied);
    }
}
