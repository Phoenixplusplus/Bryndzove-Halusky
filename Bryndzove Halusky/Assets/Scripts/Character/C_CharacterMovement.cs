using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class C_CharacterMovement : Photon.MonoBehaviour
{
    // local
    public float mouseSensitivity = 3f, movementSpeed = 10f;
    public float WS, AD;
    public Vector3 localVelocity;

    // networking
    public Vector3 networkPosition, networkVelocity, predictedPosition;
    public Quaternion networkRotation;
    public double lastTimestamp;

    void Awake()
    {

    }

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (photonView.isMine)
        {
            Move();
            RotateWithMouseX();
        }
        //else
        //{
        //    // update position
        //    // calculate roundtrip time for packet
        //    float ping = (float)PhotonNetwork.GetPing() * 0.001f;
        //    float lastUpdate = (float)(PhotonNetwork.time - lastTimestamp);
        //    float totalUpdateTime = ping + lastUpdate;

        //    //update position
        //    predictedPosition = networkPosition + networkVelocity * movementSpeed * totalUpdateTime;
        //    transform.position = Vector3.MoveTowards(transform.position, predictedPosition, movementSpeed * Time.deltaTime); // - 0.2 seems to be a good enough value

        //    // update rotation
        //    transform.rotation = Quaternion.RotateTowards(transform.rotation, networkRotation, movementSpeed * Time.deltaTime);
        //}
    }

    // local
    void Move()
    {
        // move on keyboard input
        AD = Input.GetAxis("Horizontal") * Time.deltaTime;
        WS = Input.GetAxis("Vertical") * Time.deltaTime;

        localVelocity = new Vector3(AD, 0, WS);
        transform.Translate(AD * movementSpeed, 0, WS * movementSpeed);
    }

    void RotateWithMouseX()
    {
        // rotate on mouse X
        float rawMouseRotation = Input.GetAxis("Mouse X");
        Vector3 mouseRotation = new Vector3(0, rawMouseRotation, 0);

        transform.Rotate(mouseRotation * mouseSensitivity);
    }

    // networking
    void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        //if (stream.isWriting)
        //{
        //    // local component sending info to the network
        //    stream.SendNext(transform.position);
        //    stream.SendNext(transform.rotation);
        //    stream.SendNext(localVelocity);
        //    stream.SendNext(movementSpeed);
        //}
        //else
        //{
        //    // if we are not sending info, then recieve the same info from others
        //    networkPosition = (Vector3)stream.ReceiveNext();
        //    networkRotation = (Quaternion)stream.ReceiveNext();
        //    networkVelocity = (Vector3)stream.ReceiveNext();
        //    movementSpeed = (float)stream.ReceiveNext();
        //    lastTimestamp = info.timestamp;
        //}
    }
}
