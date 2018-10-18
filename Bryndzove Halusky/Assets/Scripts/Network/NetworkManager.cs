using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetworkManager : Photon.MonoBehaviour {

    private string roomName = "I'm hungry";
    private TypedLobby lobbyName = new TypedLobby("NewLobby", LobbyType.Default);
    private RoomInfo[] roomsList;
    private GameManager GM;
    private GameObject lobbyCamera;
    public GameObject Character;

    [Header("Weapons")]
    [HeaderAttribute("Red Team")]
    public GameObject RedPistol;
    public GameObject RedMachineGun;
    public GameObject RedShotgun;
    [HeaderAttribute("Blue Team")]
    public GameObject BluePistol;
    public GameObject BlueMachineGun;
    public GameObject BlueShotgun;

    [Header("Teams")]
    int redTeamCount;
    int blueTeamCount;

    // Use this for initialization
    void Start ()
    {
        PhotonNetwork.ConnectUsingSettings("v4.2");
        GM = GameObject.Find("GameManager").GetComponent<GameManager>();
        lobbyCamera = GameObject.Find("LobbyCamera");
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    void OnGUI()
    {
        if (!PhotonNetwork.connected)
        {
            GUILayout.Label(PhotonNetwork.connectionStateDetailed.ToString());
        }
        else if (PhotonNetwork.room == null)
        {
            // create room
            if (GUI.Button(new Rect(100, 100, 250, 100), "Create Server"))
            {
                roomName = "Server " + roomsList.Length + 1;
                PhotonNetwork.CreateRoom(roomName, new RoomOptions() { MaxPlayers = 4, IsOpen = true, IsVisible = true }, lobbyName);
            }

            // join room
            if (roomsList != null)
            {
                for (int i = 0; i < roomsList.Length; i++)
                {
                    if (GUI.Button(new Rect(100, 250 + (110 * i), 250, 100), roomsList[i].Name + " - Players (" + roomsList[i].PlayerCount + "/" + roomsList[i].MaxPlayers + ")"))
                    {
                        PhotonNetwork.JoinRoom(roomsList[i].Name);
                    }
                }
            }
        }
    }

    void OnConnectedToMaster()
    {
        PhotonNetwork.JoinLobby(lobbyName);
    }

    void OnReceivedRoomListUpdate()
    {
        roomsList = PhotonNetwork.GetRoomList();
    }

    void OnJoinedLobby()
    {
        Debug.Log("Joined lobby");
    }

    void OnJoinedRoom()
    {
        // lock/hide cursor and delete default camera
        GM.LockHideCursor();
        if (lobbyCamera != null) lobbyCamera.SetActive(false);
  
        Debug.Log("Connected to " + "'" + PhotonNetwork.room.Name + "'" + " - Players(" + PhotonNetwork.playerList.Length + ")");

        SetupAndSpawnCharacter();
    }

    void SetupAndSpawnCharacter()
    {
        GameObject localCharacter;

        // note: we are spawning a character from a prefab, which is a 'base', the network character (the one we are controlling)
        // is the localCharacter variable, which needs to have their components enabled
        if (PhotonNetwork.playerList.Length > 1)
        {
            localCharacter = (GameObject)PhotonNetwork.Instantiate(Character.name, new Vector3(-9, 0, -7), Quaternion.identity, 0);
        }
        else
        {
            localCharacter = (GameObject)PhotonNetwork.Instantiate(Character.name, new Vector3(0, 0, 0), Quaternion.identity, 0);
        }

        // -- activate local scripts (disabled for everyone else)
        // activate base scripts
        localCharacter.GetComponent<C_Character>().enabled = true;
        localCharacter.GetComponent<C_CharacterMovement>().enabled = true;
        // activate child components
        localCharacter.transform.Find("CharacterCamera").gameObject.SetActive(true);
        // activate child scripts
        localCharacter.GetComponentInChildren<C_LArmTilt>().enabled = true;
        localCharacter.GetComponentInChildren<C_RArmTilt>().enabled = true;
        localCharacter.GetComponentInChildren<C_BodyTilt>().enabled = true;
        localCharacter.GetComponentInChildren<C_CameraMovement>().enabled = true;
        // -- disable local scripts (enabled for everyone else) if needed

        // team setup
        // find all characters in the server currently as see which team they're on
        GameObject[] playerRefs = GameObject.FindGameObjectsWithTag("Character");

        for (int i = 0; i < playerRefs.Length; i++)
        {
            if (playerRefs[i].GetComponent<C_Character>().Team == "Red") redTeamCount++;
            if (playerRefs[i].GetComponent<C_Character>().Team == "Blue") blueTeamCount++;
        }

        // pick a team based on teamcount
        if (redTeamCount > blueTeamCount) localCharacter.GetComponent<C_Character>().Team = "Blue";
        else if (blueTeamCount > redTeamCount) localCharacter.GetComponent<C_Character>().Team = "Red";
        else
        {
            int rand = Random.Range(1, 3);
            if (rand == 1)
            {
                localCharacter.GetComponent<C_Character>().Team = "Blue";
                blueTeamCount++;
            }
            else
            {
                localCharacter.GetComponent<C_Character>().Team = "Red";
                redTeamCount++;
            }
        }

        // move to spawn point
        GameObject[] spawnPointRefs = GameObject.FindGameObjectsWithTag("SpawnPoint");

        for (int i = 0; i < spawnPointRefs.Length; i++)
        {
            if (localCharacter.GetComponent<C_Character>().Team == spawnPointRefs[i].GetComponent<SpawnPoint>().Team && spawnPointRefs[i].GetComponent<SpawnPoint>().Occupied == false)
            {
                localCharacter.transform.position = spawnPointRefs[i].transform.position;
                localCharacter.transform.rotation = spawnPointRefs[i].transform.rotation;
            }
        }

        // spawn weapon
        GameObject localL_Gun;
        Transform L_gunSlot = localCharacter.transform.Find("CharacterBody/CharacterLArm/LGunSlot");

        if (localCharacter.GetComponent<C_Character>().Team == "Red")
        {
            localL_Gun = (GameObject)PhotonNetwork.Instantiate(RedMachineGun.name, L_gunSlot.transform.position + new Vector3(0, 0.1f, 0), localCharacter.transform.rotation, 0);
            localL_Gun.transform.parent = L_gunSlot;
        }
        else
        {
            localL_Gun = (GameObject)PhotonNetwork.Instantiate(BlueMachineGun.name, L_gunSlot.transform.position + new Vector3(0, 0.1f, 0), localCharacter.transform.rotation, 0);
            localL_Gun.transform.parent = L_gunSlot;
        }
        // give a reference to it to the localcharacter
        localCharacter.GetComponent<C_Character>().lw = localL_Gun;
        localCharacter.GetComponent<C_Character>().NetworkStart();


    }
}
