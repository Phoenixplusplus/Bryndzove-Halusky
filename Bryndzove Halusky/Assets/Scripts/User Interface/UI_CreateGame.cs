using UnityEngine;
using UnityEngine.UI;

public class UI_CreateGame : MonoBehaviour
{
    private UserInterfaceManager UI_manager;
    private int playersCount;
    void Start()
    {
        //Text sets your text to say this message
        UI_manager = GameObject.Find("UserInterfaceManager").GetComponent<UserInterfaceManager>();

        // Initialize players count on 4 = 2vs2
        playersCount = 4;
    }

    public void CreateNewRoom()
    {
      //  roomName = "Server " + roomsList.Length + 1;
       // PhotonNetwork.CreateRoom(roomName, new RoomOptions() { MaxPlayers = 4, IsOpen = true, IsVisible = true }, lobbyName);
    }

    public void SetMaximumPlayerCountTwo()    {        playersCount = 2;    }
    public void SetMaximumPlayerCountFour()    {        playersCount = 4;    }
    public void SetMaximumPlayerCountSix()    {        playersCount = 6;    }
    public void SetMaximumPlayerCountEight()    {        playersCount = 8;    }
}

