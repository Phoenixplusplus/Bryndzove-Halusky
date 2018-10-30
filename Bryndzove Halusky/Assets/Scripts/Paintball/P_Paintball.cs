using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class P_Paintball : MonoBehaviour {

    private GameManager gameManager;

    [Header("Attributes")]
    public Vector3 Position;
    public Quaternion Rotation;
    public string Team;
    public float Speed = 1f;
    public float Timeout = 0f;
    public float maxTimeout = 4f;
    public bool isInit = true; // to keep paintballs where they are when manager spawns the pool of them

    [Header("Decal Attributes")]
    public GameObject SplatDecal;
    public Material[] SplatMaterials;

    // Use this for initialization
    void Start ()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }
	
	// Update is called once per frame
	void Update ()
    {
        // main paintball behaviour
        if (!isInit)
        {
            transform.position = transform.position + transform.forward * (Time.deltaTime * Speed);

            Timeout += Time.deltaTime;
            if (Timeout > maxTimeout) ResetPainball();
        }
	}

    void OnTriggerEnter(Collider other)
    {
        // collision with player, this can be handled locally for each player
        if (other.gameObject.tag == "Player")
        {
            C_Character character = other.transform.root.GetComponent<C_Character>();
            if (character.Team != Team)
            {
                character.Health--;
                ResetPainball();
            }
        }

        // collision with scene (things that can be painted in the level)
        if (other.gameObject.tag == "Scene")
        {
            // paint them
            if (Team == "Red")
            {
                // if not already painted, paint it and send message to master to update gamemanager
                // red
                gameManager.SetSplatDecal(transform.position, transform.eulerAngles - new Vector3(90, 0, 0), SplatMaterials[Random.Range(0, 10)]);
                if (other.GetComponent<ApplyPaint>().RedTeam == false)
                {
                    other.GetComponent<ApplyPaint>().RedTeam = true;
                    gameManager.redTeamPaintCount++;
                    if (other.GetComponent<ApplyPaint>().BlueTeam == true)
                    {
                        other.GetComponent<ApplyPaint>().BlueTeam = false;
                        gameManager.blueTeamPaintCount--;
                    }
                }
            }
            else
            {
                // blue
                gameManager.SetSplatDecal(transform.position, transform.eulerAngles - new Vector3(90, 0, 0), SplatMaterials[Random.Range(10, 20)]);
                if (other.GetComponent<ApplyPaint>().BlueTeam == false)
                {
                    other.GetComponent<ApplyPaint>().BlueTeam = true;
                    gameManager.blueTeamPaintCount++;
                    if (other.GetComponent<ApplyPaint>().RedTeam == true)
                    {
                        other.GetComponent<ApplyPaint>().RedTeam = false;
                        gameManager.redTeamPaintCount--;
                    }
                }
            }
            ResetPainball();
        }
    }

    void ResetPainball()
    {
        transform.position = gameManager.paintballsStartPosition;
        Timeout = 0f;
        isInit = true;
    }
}
