using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetworkMovement : Photon.MonoBehaviour {

    private GameObject Character;
    private CharacterMovement characterMovement;
    private Vector3 networkPosition, networkVelocity, localVelocity;
    private Quaternion networkRotation;
    private float MinMovementPerUpdate = 1, MaxMovementPerUpdate = 5;
    public int sendRate = 16, serializedSendRate = 16;
    private float lerpTime;
    private double lastTimestamp;

    // Use this for initialization
    void Start ()
    {
        PhotonNetwork.sendRate = sendRate;
        PhotonNetwork.sendRateOnSerialize = serializedSendRate;

        Character = transform.root.gameObject;
        characterMovement = Character.GetComponent<CharacterMovement>();
    }
	
	// Update is called once per frame
	void Update ()
    {
        
        //lerpTime = Time.deltaTime / (1f / sendRate);

        // lerp our movement/rotation from where network player sees us, to where we actually are
        if (!photonView.isMine)
        {
            // predict position based on current position, velocity and total turnaround time of packet
            float ping = (float)PhotonNetwork.GetPing() * 0.001f;
            float lastUpdate = (float)(PhotonNetwork.time - lastTimestamp);
            float totalUpdateTime = ping + lastUpdate;

            Vector3 predictedPosition = networkPosition + networkVelocity * totalUpdateTime;

            // lerp position/rotation based on the photon network send rate
            lerpTime = Time.deltaTime / (1f / sendRate);
            transform.position = Vector3.Lerp(transform.position, predictedPosition, lerpTime);
            transform.rotation = Quaternion.Lerp(transform.rotation, networkRotation, lerpTime);
        }
	}

    void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.isWriting)
        {
            // local component sending transform to the network
            localVelocity = new Vector3(characterMovement.AD, 0f, characterMovement.WS);
            stream.SendNext(transform.position);
            stream.SendNext(transform.rotation);
            stream.SendNext(localVelocity);
        }
        else
        {
            // receiving network players transforms
            networkPosition = (Vector3)stream.ReceiveNext();
            networkRotation = (Quaternion)stream.ReceiveNext();
            networkVelocity = (Vector3)stream.ReceiveNext();
            lastTimestamp = info.timestamp;
        }
    }
}
