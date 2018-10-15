using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetworkMovement : Photon.MonoBehaviour {

    private GameObject Character;
    private C_CharacterMovement characterMovement;
    private Vector3 networkPosition, networkVelocity, localVelocity, predictedPosition;
    private Quaternion networkRotation;
    public int sendRate = 30, serializedSendRate = 30;
    private float lerpTime;
    private double lastTimestamp;
    private float movementSpeed;

    // Use this for initialization
    void Start ()
    {
        PhotonNetwork.sendRate = sendRate;
        PhotonNetwork.sendRateOnSerialize = serializedSendRate;

        Character = transform.root.gameObject;
        characterMovement = Character.GetComponent<C_CharacterMovement>();
    }
	
	// Update is called once per frame
	void Update ()
    {


        //predictedPosition = networkPosition + localVelocity * totalUpdateTime;

        //// lerp position/rotation based on the send rate
        //lerpTime = Time.deltaTime * sendRate;
        //Debug.Log(lastUpdate);

        if (photonView.isMine)
        {
            // do local stuff
        }
        else
        {
            // update position
            // calculate roundtrip time for packet
            float ping = (float)PhotonNetwork.GetPing() * 0.001f;
            float lastUpdate = (float)(PhotonNetwork.time - lastTimestamp);
            float totalUpdateTime = ping + lastUpdate;

            //update position
            predictedPosition = networkPosition + networkVelocity * movementSpeed * totalUpdateTime;
            transform.position = Vector3.MoveTowards(transform.position, predictedPosition, Vector3.Distance(transform.position, predictedPosition) * sendRate * Time.deltaTime);

            // update rotation
            transform.rotation = Quaternion.Lerp(transform.rotation, networkRotation, 180f * sendRate * Time.deltaTime);

            //transform.position = Vector3.MoveTowards(transform.position, predictedPosition, 10f * Time.deltaTime);
            //transform.rotation = Quaternion.Lerp(transform.rotation, networkRotation, 10f * Time.deltaTime);
        }
    }

    void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.isWriting)
        {
            // local component sending transform to the network
            stream.SendNext(transform.position);
            stream.SendNext(transform.rotation);
            stream.SendNext(characterMovement.localVelocity);
            stream.SendNext(characterMovement.movementSpeed);
        }
        else
        {
            // receiving network players transforms
            networkPosition = (Vector3)stream.ReceiveNext();
            networkRotation = (Quaternion)stream.ReceiveNext();
            networkVelocity = (Vector3)stream.ReceiveNext();
            movementSpeed = (float)stream.ReceiveNext();
            lastTimestamp = info.timestamp;
        }
    }
}
