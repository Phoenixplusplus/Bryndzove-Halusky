using UnityEngine;
using UnityEngine.UI;

public class UI_RoomButton : MonoBehaviour
{
    public int ID;

    private Text roomNumber;
    private Text roomStatus;
    private Text roomName;
    private Text mapName;
    private Text playersCount;

    void Start()
    {
        roomNumber = this.gameObject.transform.GetChild(0).GetComponent<Text>();
        roomStatus = this.gameObject.transform.GetChild(1).GetComponent<Text>();
        roomName = this.gameObject.transform.GetChild(2).GetComponent<Text>();
        mapName = this.gameObject.transform.GetChild(3).GetComponent<Text>();
        playersCount = this.gameObject.transform.GetChild(4).GetComponent<Text>();
    }

}
