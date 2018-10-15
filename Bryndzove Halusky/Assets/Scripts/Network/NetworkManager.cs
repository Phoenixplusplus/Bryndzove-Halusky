using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetworkManager : Photon.MonoBehaviour {

    private const string roomName = "RoomName";
    private TypedLobby lobbyName = new TypedLobby("NewLobby", LobbyType.Default);
    private RoomInfo[] roomsList;
    private GameManager GM;
    private GameObject defaultCamera;
    public GameObject Character;

    // Use this for initialization
    void Start ()
    {
        PhotonNetwork.ConnectUsingSettings("v4.2");
        GM = GameObject.Find("GameManager").GetComponent<GameManager>();
        defaultCamera = GameObject.Find("DefaultCamera");
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
            if (GUI.Button(new Rect(100, 100, 250, 100), "Start Server"))
            {
                PhotonNetwork.CreateRoom(roomName, new RoomOptions() { MaxPlayers = 4, IsOpen = true, IsVisible = true }, lobbyName);
            }

            // join room
            if (roomsList != null)
            {
                for (int i = 0; i < roomsList.Length; i++)
                {
                    if (GUI.Button(new Rect(100, 250 + (110 * i), 250, 100), "Join" + roomsList[i].Name))
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
        Debug.Log("Room was created");
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
        if (defaultCamera != null) defaultCamera.SetActive(false);

        Debug.Log("Connected to room");
        print(PhotonNetwork.playerList.Length);

        // spawn in
        Spawn();

    }

    void Spawn()
    {
        GameObject localCharacter;

        // note: we are spawning a character from a prefab, which is a 'base', the network character (the one the player is controlling)
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
        localCharacter.GetComponent<C_CharacterMovement>().enabled = true;
        // activate child components
        localCharacter.transform.Find("CharacterCamera").gameObject.SetActive(true);
        // activate child scripts
        localCharacter.GetComponentInChildren<C_LArmTilt>().enabled = true;
        localCharacter.GetComponentInChildren<C_RArmTilt>().enabled = true;
        localCharacter.GetComponentInChildren<C_BodyTilt>().enabled = true;
        localCharacter.GetComponentInChildren<C_CameraMovement>().enabled = true;

        // -- disable local scripts (enabled for everyone else)
    }
}
