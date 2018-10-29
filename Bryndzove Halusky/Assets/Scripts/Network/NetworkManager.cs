using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetworkManager : Photon.MonoBehaviour {

    protected string roomName = "I'm hungry";
    protected TypedLobby lobbyName = new TypedLobby("NewLobby", LobbyType.Default);
    protected RoomInfo[] roomsList;
    protected GameManager GM;
    protected UserInterfaceManager UI_Manager;
    protected int playersOnline;
    private GameObject lobbyCamera;
    public GameObject Character;

    // Use this for initialization
    void Start()
    {
        PhotonNetwork.ConnectUsingSettings("v4.2");
        GM = GameObject.Find("GameManager").GetComponent<GameManager>();
        lobbyCamera = GameObject.Find("LobbyCamera");
    }

    // Update is called once per frame
    void Update()
    {
        // Update the lists while is inside the lobby or move it into refresh button function or even delete ???????????
        // Update the lists while is inside the lobby or move it into refresh button function or even delete ???????????
        // Update the lists while is inside the lobby or move it into refresh button function or even delete ???????????
        // Update the lists while is inside the lobby or move it into refresh button function or even delete ???????????
        //OnReceivedRoomListUpdate();
    }

    public void JoinRoom(int roomNumber)
    {
      //  if (roomsList[roomNumber].PlayerCount < roomsList[roomNumber].MaxPlayers)
      //  {
      //      Debug.Log("PlayerCount " + roomsList[roomNumber].PlayerCount);
       //     PhotonNetwork.JoinRoom(roomsList[roomNumber].Name);
      //  }
      //  else // Show UI the room is full
       // {

      //  }
    }

    void OnConnectedToMaster()
    {
        PhotonNetwork.JoinLobby(lobbyName);
    }

    void OnReceivedRoomListUpdate()
    {
        roomsList = PhotonNetwork.GetRoomList();
        Debug.Log("Room list length " + roomsList.Length);
    }

    public TypedLobby GetLobbyName()
    {
        return lobbyName;
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
    }
}
