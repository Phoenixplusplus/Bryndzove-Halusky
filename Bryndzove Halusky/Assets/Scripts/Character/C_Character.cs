using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class C_Character : Photon.MonoBehaviour {

    [Header("Network Attributes")]
    public string loginName;
    public string Team;

    [Header("Attributes")]
    public float Health = 10f;
    public float healthRecoverySpeed = 0.1f;
    public float movementSpeed;
    public Texture headTex, bodyTex;

    // do not assign these values in editor
    public W_Weapon leftWeapon, rightWeapon;

    private bool autoFire;

    // Use this for initialization
    void Start ()
    {
        // on spawn from network manager
        if (photonView.isMine)
        {
            PickTeam(Random.Range(0,3));
            MoveToSpawnPoint();
            AttachWeapon();
        }
        else
        {

        }
	}
	
	// Update is called once per frame
	void Update ()
    {
        // keyboard input
        if (photonView.isMine)
        {
            if (autoFire == true)
            {
                if (Input.GetMouseButton(0))
                {
                    leftWeapon.Fire();
                }
            }
            else
            {
                if (Input.GetMouseButtonDown(0))
                {
                    leftWeapon.Fire();
                }
            }
            if (Input.GetKeyDown(KeyCode.R))
            {
                leftWeapon.Reload();
            }
            if (Input.GetKeyDown(KeyCode.V))
            {
                GameObject[] go = GameObject.FindGameObjectsWithTag("Character");

                for (int i = 0; i < go.Length; i++)
                {
                    Debug.Log(go[i].GetComponent<C_Character>().Team);
                }
            }
        }
    }
    
    [PunRPC] void PickTeam(int rand)
    {
        int redTeamCount = 0;
        int blueTeamCount = 0;

        // find all characters in the server currently as see which team they're on
        GameObject[] playerRefs = GameObject.FindGameObjectsWithTag("Character");

        for (int i = 0; i < playerRefs.Length; i++)
        {
            if (playerRefs[i].GetComponent<C_Character>().Team == "Red") redTeamCount++;
            if (playerRefs[i].GetComponent<C_Character>().Team == "Blue") blueTeamCount++;
        }

        // pick a team based on teamcount
        if (redTeamCount > blueTeamCount) Team = "Blue";
        if (blueTeamCount > redTeamCount) Team = "Red";
        if (blueTeamCount == redTeamCount)
        {
            if (rand == 1)
            {
                Team = "Blue";
                blueTeamCount++;
            }
            else
            {
                Team = "Red";
                redTeamCount++;
            }
        }

        // send to server
        if (photonView.isMine)
        {
            photonView.RPC("PickTeam", PhotonTargets.OthersBuffered, rand);
        }
    }

    void MoveToSpawnPoint()
    {
        // move to spawn point
        GameObject[] spawnPointRefs = GameObject.FindGameObjectsWithTag("SpawnPoint");

        for (int i = 0; i < spawnPointRefs.Length; i++)
        {
            if (Team == spawnPointRefs[i].GetComponent<SpawnPoint>().Team && spawnPointRefs[i].GetComponent<SpawnPoint>().Occupied == false)
            {
                transform.position = spawnPointRefs[i].transform.position;
                transform.rotation = spawnPointRefs[i].transform.rotation;
                break;
            }
        }
    }

    void AttachWeapon()
    {
        // spawn weapon
        GameObject localL_Gun;
        Transform L_gunSlot = transform.Find("CharacterBody/CharacterLArm/LGunSlot");

        if (Team == "Red")
        {
            localL_Gun = (GameObject)PhotonNetwork.Instantiate("RedMachineGun", L_gunSlot.transform.position + new Vector3(0, 0.1f, 0), transform.rotation, 0);
            localL_Gun.transform.parent = L_gunSlot;
        }
        else
        {
            localL_Gun = (GameObject)PhotonNetwork.Instantiate("BlueMachineGun", L_gunSlot.transform.position + new Vector3(0, 0.1f, 0), transform.rotation, 0);
            localL_Gun.transform.parent = L_gunSlot;
        }

        leftWeapon = localL_Gun.GetComponent<W_Weapon>();
        leftWeapon.enabled = true;
        if (leftWeapon.name.Contains("Machine")) autoFire = true;
        else autoFire = false;
    }
}
