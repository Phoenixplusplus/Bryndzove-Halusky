using UnityEngine;
using UnityEngine.UI;

public class UserInterfaceManager : NetworkManager
{
    private UI_RoomButton [] m_roomButtonsArray;
    private int m_countOfRoomButtons;
    protected Canvas CNVS_Lobby;

    // Use this for initialization
    void Start ()
    {
        InitializeRoomButtons();
        CNVS_Lobby = GameObject.Find("CNV_MainMenu").GetComponent<Canvas>(); ;
    }

    // Update is called once per frame
    void Update ()
    {
        if (PhotonNetwork.connected)
        {
            // DELETEEEEEEEEEEEEEEEEEee
            // DELETEEEEEEEEEEEEEEEEEee
            // DELETEEEEEEEEEEEEEEEEEee


            if (roomsList != null)
            {
                UpdateRooms();


                for (int i = 0; i < roomsList.Length; i++)
                {
                    if (roomsList[i] != null) m_roomButtonsArray[i].gameObject.SetActive(true);
                }
            }
        }
    }

    // Return the count of players connected in master server and rooms together
    public int GetPlayersInMasterServer()
    {
        return PhotonNetwork.countOfPlayersOnMaster + PhotonNetwork.countOfPlayersInRooms;
    }

    public void UpdateRooms()
    {
        for (int i = 0; i < roomsList.Length; i++)
        {
            m_roomButtonsArray[i].SetRoomDetails(i, "Status", roomsList[i].Name, "Map", roomsList[i].PlayerCount, roomsList[i].MaxPlayers);
        }

        if (roomsList.Length < m_countOfRoomButtons)
        {
            for (int i = roomsList.Length; i < m_countOfRoomButtons; i++)
            {
                m_roomButtonsArray[i].ResetButton();
            }
        }
    }

    void JoinRoom(int roomNumber)
    {
        if (roomsList[roomNumber].PlayerCount < roomsList[roomNumber].MaxPlayers)
        {
            CNVS_Lobby.gameObject.SetActive(false);
            PhotonNetwork.JoinRoom(roomsList[roomNumber].Name);
        }
        else // Show UI the room is full
        {
            Debug.Log("Rooms " + roomsList[roomNumber].Name + " is full.");
        }
    }

    // Initialize all room buttons
    private void InitializeRoomButtons()
    {
        // Get all button which containt UI_RoomButton script and put them into array
        m_roomButtonsArray = FindObjectsOfType(typeof(UI_RoomButton)) as UI_RoomButton[];
        m_countOfRoomButtons = m_roomButtonsArray.Length;

        // Swap the buttons positions in array
        // Create temporary UI_RoomButton object
        UI_RoomButton a;
        for (int i = 0; i < m_countOfRoomButtons; i++)
        {
            for (int k = 0; k < m_countOfRoomButtons - 1; k++)
            {
                if (m_roomButtonsArray[k].ID > m_roomButtonsArray[k + 1].ID)
                {
                    a = m_roomButtonsArray[k];
                    m_roomButtonsArray[k] = m_roomButtonsArray[k + 1];
                    m_roomButtonsArray[k + 1] = a;
                }
            }
        }

        // Disable all buttons
        for (int i = 0; i < m_countOfRoomButtons; i++)
        {
            // m_roomButtonsArray[i].gameObject.SetActive(false);
            m_roomButtonsArray[i].ResetButton();
        }
    }
}

// roomsList lengthe does not update during the gameplay ? when you run 2 clients in same time without created any game yet,
// and after you connect twice into game and create game, both games will be made in roomList[0], or only first one will be,
// second game wont initialize
