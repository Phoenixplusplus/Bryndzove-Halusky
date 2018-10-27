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

    [Header("Decal Attributes")]
    public GameObject SplatDecal;
    public Material[] SplatMaterials;

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
                GameObject splatDecal = Instantiate(SplatDecal, other.transform.position, other.transform.rotation);
                splatDecal.transform.Rotate(Vector3.up, Random.Range(0, 360), Space.Self);
                splatDecal.GetComponent<Decal>().m_Material = SplatMaterials[Random.Range(0, 10)];

                if (other.GetComponent<ApplyPaint>().RedTeam == false)
                {
                    other.GetComponent<ApplyPaint>().RedTeam = true;
                    GameObject.Find("GameManager").GetComponent<GameManager>().redTeamPaintCount++;
                    if (other.GetComponent<ApplyPaint>().BlueTeam == true)
                    {
                        other.GetComponent<ApplyPaint>().BlueTeam = false;
                        GameObject.Find("GameManager").GetComponent<GameManager>().blueTeamPaintCount--;
                    }
                }
            }
            else
            {
                // blue
                GameObject splatDecal = Instantiate(SplatDecal, other.transform.position, other.transform.rotation);
                splatDecal.transform.Rotate(Vector3.up, Random.Range(0, 360), Space.Self);
                splatDecal.GetComponent<Decal>().m_Material = SplatMaterials[Random.Range(10, 20)];

                if (other.GetComponent<ApplyPaint>().BlueTeam == false)
                {
                    other.GetComponent<ApplyPaint>().BlueTeam = true;
                    GameObject.Find("GameManager").GetComponent<GameManager>().blueTeamPaintCount++;
                    if (other.GetComponent<ApplyPaint>().RedTeam == true)
                    {
                        other.GetComponent<ApplyPaint>().RedTeam = false;
                        GameObject.Find("GameManager").GetComponent<GameManager>().redTeamPaintCount--;
                    }
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
