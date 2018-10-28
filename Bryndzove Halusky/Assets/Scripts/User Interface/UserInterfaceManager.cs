using UnityEngine;
using UnityEngine.UI;

public class UserInterfaceManager : MonoBehaviour
{

    private UI_RoomButton [] m_roomButtons;

    private NetworkManager m_networkManager;

    public Button test;
    public Button test2;

    // Use this for initialization
    void Start ()
    {
        m_networkManager = GameObject.Find("NetworkManager").GetComponent<NetworkManager>();

    }

    // Update is called once per frame
    void Update ()
    {
        if (m_networkManager.GetRoomInfo(0) != null) test.gameObject.SetActive(true);
        else test.gameObject.SetActive(false);

        if (m_networkManager.GetRoomInfo(1) != null) test2.gameObject.SetActive(true);
        else test2.gameObject.SetActive(false);
    }
}
