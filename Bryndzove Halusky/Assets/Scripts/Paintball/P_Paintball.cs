using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class P_Paintball : MonoBehaviour {

    [Header("Attributes")]
    public Vector3 Position;
    public Quaternion Rotation;
    public string Team;
    public float Speed = 1f;
    public float Timeout = 0f;
    public float maxTimeout = 4f;

    // Use this for initialization
    void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        transform.position = transform.position + transform.forward * (Time.deltaTime * Speed);

        Timeout += Time.deltaTime;
        if (Timeout > maxTimeout) DestroyPaintball();
	}

    void OnTriggerEnter(Collider other)
    {
        // collision with player, this can be handled locally for each player
        if (other.gameObject.tag == "Player")
        {
            C_Character character = other.transform.root.GetComponent<C_Character>();
            if (character.Team != Team)
            {
                other.transform.root.GetComponent<C_Character>().Health--;
                DestroyPaintball();
            }
        }

        // collision with scene (things that can be painted in the level)
        if (other.gameObject.tag == "Scene")
        {
            //Debug.Log("Hit scene object" + other.name);
            // paint them
            if (Team == "Red")
            {
                // if not already painted, paint it and send message to master to update gamemanager
                // red
                if (other.gameObject.GetComponent<Renderer>().material.color != new Color(1, 0, 0, 1))
                {
                    other.gameObject.GetComponent<Renderer>().material.color = new Color(1, 0, 0, 1);
                    if (PhotonNetwork.isMasterClient) GameObject.Find("GameManager").GetComponent<GameManager>().redTeamPaintCount++;
                }
            }
            else
            {
                // blue
                if (other.gameObject.GetComponent<Renderer>().material.color != new Color(0, 0, 1, 1))
                {
                    other.gameObject.GetComponent<Renderer>().material.color = new Color(0, 0, 1, 1);
                    if (PhotonNetwork.isMasterClient) GameObject.Find("GameManager").GetComponent<GameManager>().blueTeamPaintCount++;
                }
            }
            DestroyPaintball();
        }
    }

    void DestroyPaintball()
    {
        Destroy(gameObject);
    }
}
