using UnityEngine;
using UnityEngine.UI;

public class UserInterfaceManager : NetworkManager
{
    private UI_RoomButton [] m_roomButtonsArray;
    private int m_countOfRoomButtons;

    // Use this for initialization
    void Start ()
    {
        InitializeRoomButtons();
    }

    // Update is called once per frame
    void Update ()
    {
        if (PhotonNetwork.connected)
        {
            if (roomsList != null)
            {
                for (int i = 0; i < roomsList.Length; i++)
                {
                    if (roomsList[i] != null) m_roomButtonsArray[i].gameObject.SetActive(true);
                    else m_roomButtonsArray[i].gameObject.SetActive(false);
                }
            }
        }
    }

    // Return the count of players connected in master server and rooms together
    public int GetPlayersInMasterServer()
    {
        return PhotonNetwork.countOfPlayersOnMaster + PhotonNetwork.countOfPlayersInRooms;
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
        for (int i = 0; i < m_countOfRoomButtons; i++) m_roomButtonsArray[i].gameObject.SetActive(false);
    }
}

// roomsList lengthe does not update during the gameplay ? when you run 2 clients in same time without created any game yet,
// and after you connect twice into game and create game, both games will be made in roomList[0], or only first one will be,
// second game wont initialize
