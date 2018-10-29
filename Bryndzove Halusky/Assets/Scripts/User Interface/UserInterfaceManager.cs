using UnityEngine;
using UnityEngine.UI;

public class UserInterfaceManager : NetworkManager
{
    private UI_RoomButton [] m_roomButtonsArray;

    public Button test;
    public Button test2;

    // Use this for initialization
    void Start ()
    {
        m_roomButtonsArray = FindObjectsOfType(typeof(UI_RoomButton)) as UI_RoomButton[];

        foreach (UI_RoomButton ptrButton in m_roomButtonsArray)
        {
            ptrButton.gameObject.SetActive(false);
        }
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

    public int GetPlayersInMasterServer()
    {
        return PhotonNetwork.countOfPlayersOnMaster + PhotonNetwork.countOfPlayersInRooms;
    }
}

// roomsList lengthe does not update during the gameplay ? when you run 2 clients in same time without created any game yet,
// and after you connect twice into game and create game, both games will be made in roomList[0], or only first one will be,
// second game wont initialize
